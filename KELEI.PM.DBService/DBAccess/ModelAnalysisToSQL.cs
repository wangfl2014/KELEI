using KELEI.Commons.Helper;
using KELEI.PM.DBService.Commons;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KELEI.PM.DBService.DBAccess
{
    /// <summary>
    /// 根据实体类T，返回该实体的select，update，insert，delete语句
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ModelAnalysisToSQL<T> where T : System.Type
    {
        //获取实体类T的特性及属性
        public static void GetModelProperty(T model)
        {
            var list = DataPropertyHandle.GetTypeProperties(model, BindingFlagType.All, null);
            if (list == null) return;
            //返回sql字段
            var sqlColumn = AnalysisSql(list.Item2, list.Item4);

            BusinessModel bm = new BusinessModel()
            {
                modelCode = list.Item1,
                tableName = list.Item2,
                column = sqlColumn.Item4,
                dbConnection = list.Item3?? ConstSet.DefaultDBConnection,
                modelObj = model
            };
            
            ModelDictionaries.UpdateDicModel(list.Item1, bm);

            //生成sql语句
            //查询
            string relationSql = string.Empty;
            var relationList = sqlColumn.Item1.Where(p => !string.IsNullOrEmpty(p.relationTxt));
            if (relationList == null || relationList.Count() <= 0)
            {
                relationSql = "";
            }
            else
            {
                relationSql = string.Join(" ", relationList.Select(p => p.relationTxt).Distinct().ToArray());
            }

            var selectSql = string.Format("select {0} from {1} {2}", string.Join(",", sqlColumn.Item1.Select(p => p.columnTxt).ToArray()), list.Item2, relationSql);

            //删除数据sql
            var deleteSql = string.Format("delete from {0} ", list.Item2);

            //新增数据sql
            var insertList = sqlColumn.Item3.Select(p => p.columnCode).ToArray();
            List<string> valueList = new List<string>();
            foreach (var item in sqlColumn.Item3)
            {
                valueList.Add("@" + item.columnCode);
            }
            var insertSql = string.Format("INSERT INTO {0}({1}) VALUES({2})", list.Item2, string.Join(",", insertList), string.Join(",", valueList));

            //修改sql
            List<string> updateList = new List<string>();
            var updateColumn = sqlColumn.Item2.Select(p => p.columnCode).ToArray();
            foreach (var item in updateColumn)
            {
                updateList.Add(item + "=@" + item);
            }
            var updateSql = string.Format("UPDATE {0} SET {1}", list.Item2, string.Join(",", updateList));

            AnalysisModel anaModel = new AnalysisModel()
            {
                id = list.Item2,
                selectSql = selectSql,
                insertSql = insertSql,
                updateSql = updateSql,
                deleteSql = deleteSql,
                selectColumnList = sqlColumn.Item1,
                updateColumnList = sqlColumn.Item2,
                insertColumnList = sqlColumn.Item3
            };
            ModelDictionaries.UpdateDicModelAnalysis(list.Item1, anaModel);
        }

        /// <summary>
        /// 根据实体的属性，特性拼接sql语句
        /// </summary>
        /// <param name="colnumlist">表明，字段列表</param>
        /// <returns>select列，update列，insert列</returns>
        private static Tuple<List<SelectColumn>, List<UpdateColumn>, List<UpdateColumn>,List<Column>> AnalysisSql(string tableName, List<DataPropertyAttribute> colnumlist)
        {
            List<SelectColumn> selectColumnList = new List<SelectColumn>();//select列
            List<UpdateColumn> updateColumnList = new List<UpdateColumn>();//更新列
            List<UpdateColumn> insertColumnList = new List<UpdateColumn>();//插入列
            List<Column> columnInfo = new List<Column>();//列信息
            foreach (DataPropertyAttribute item in colnumlist)
            {
                columnInfo.Add(new Column() {
                    cloumnid=item.ColumCode,
                    cloumnType= item.CloumnType,
                    dataCloumn=item.Field,
                    isPrimaryKey=item.IsKey,
                    mappingKeys=item.MappingKeys,
                    defaultValue=item.DefaultExpression,
                    sourceModel=item.SourceModel
                });

                switch (item.CloumnType)
                {
                    case BindingcloumnType.Data: //Data是指本表字段
                        if (!item.BindingFlag.ToString().Contains("Select") && !item.BindingFlag.ToString().Contains("Where")) //where和select类型的不需要更新与新增
                        {
                            insertColumnList.Add(new UpdateColumn() { columnCode = item.Field,  isPrimaryKey = item.IsKey, columnTxt=item.DefaultExpression });//需要新增或修改的列，做主键标识，主键新增但不进行修改
                            updateColumnList.Add(new UpdateColumn() { columnCode = item.Field, isPrimaryKey = item.IsKey });
                        }
                        selectColumnList.Add(new SelectColumn() { columnCode = item.Field, columnTxt = tableName + "." + item.Field + " AS " + item.ColumCode,  relationTxt = "" });//select语句中应当出现的列，因为是本表字段，没有关联关系
                        break;
                    case BindingcloumnType.Extended: //Extended--DefaultExpression
                        //这里扩展类型仅处理，数据库计算字段，和表关联字段
                        //数据库计算列，计算列表现形式为this.字段1+this.字段2,常量或数据库函数，按照数据库要求填写
                        if (!string.IsNullOrEmpty(item.DefaultExpression) )//@表示数据库中进行sql语句计算，例如金额=this.数量*this.单价,所有本表数据引用都使用this.开头
                        {
                            string columnName = item.DefaultExpression;
                            selectColumnList.Add(new SelectColumn { columnCode = item.Field, columnTxt = columnName.Replace("this.", tableName + ".") + " AS " + item.ColumCode, relationTxt = "" });//计算列例如金额=this.数量*this.单价,使用数据库函数dbfun(this.数量)
                        }
                        break;
                    case BindingcloumnType.ExteData://ExteData--MappingKeys
                        //关联表的关联字段,this.本表字段=关联表.关联表字段，
                        if (!string.IsNullOrEmpty(item.MappingKeys))
                        {
                            //查找关联model,返回其表名
                            var relationColumn = item.MappingKeys.Replace("this.", tableName+".");
                            var relationTable = item.MappingKeys.Split('=')[1].Split('.')[0];

                            selectColumnList.Add(new SelectColumn() { columnCode = item.Field, columnTxt = relationTable + "." + item.Field + " AS " + item.ColumCode,  relationTxt = " Left join " + relationTable +" ON "+ relationColumn, relationTableName = relationTable });//表关联进行的查询
                        }
                        break;
                    default:
                        break;
                }
            }
            return new Tuple<List<SelectColumn>, List<UpdateColumn>, List<UpdateColumn>, List<Column>>(selectColumnList, updateColumnList, insertColumnList, columnInfo);
        }
    }

    public enum DBOrderbyType
    {
        asc,
        desc,
    }
}
