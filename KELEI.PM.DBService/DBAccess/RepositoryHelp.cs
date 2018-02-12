using System;
using Dapper;
using System.Collections.Generic;
using System.Linq;
using KELEI.PM.DBService.Commons;
using KELEI.Commons.Helper;

namespace KELEI.PM.DBService.DBAccess
{
    public class RepositoryHelp<T>
    {
        public static Tuple<BusinessModel, AnalysisModel> GetDic()
        {
            var t1 = typeof(T);
            string modelName = t1.Name;
            var ModelDic = ModelDictionaries.GetDic(modelName);
            return ModelDic;
        }

        public static Dictionary<string, dynamic> GetDicByobj(object obj)
        {
            //解析查询对象
            Dictionary<string, dynamic> dic = new Dictionary<string, dynamic>();
            var keys = obj.GetType();
            var keyPiArry = keys.GetProperties();
            foreach(var item in keyPiArry)
            {
                if(!string.IsNullOrEmpty(item.Name))
                {
                    dic.Add(item.Name, item.GetValue(obj, null));
                }
            }
            return dic;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="dic"></param>
        /// <param name="bm"></param>
        /// <returns></returns>
        public static Tuple<string, DynamicParameters> GetDicWhere(string sql, Dictionary<string, dynamic> dic, BusinessModel bm)
        {
            if (dic == null || dic.Count() <= 0)
            {
                return new Tuple<string, DynamicParameters>(sql, null);
            }
            List<string> dicKey = new List<string>();
            List<dynamic> dicValue = new List<dynamic>();
            DynamicParameters pars = new DynamicParameters();
            foreach (var item in dic)
            {
                //根据key返回所对应的表和字段
                var where = GetListWhere(item.Key, bm);
                dicKey.Add(where);
                pars.Add(item.Key, item.Value);
            }
            string selectSql = sql + " WHERE " + string.Join(" and ", dicKey.ToArray());
            return new Tuple<string, DynamicParameters>(selectSql, pars);
        }

        public static string GetOrderby(string sql, Dictionary<string, Enumerator.DBOrderbyType> dicOrder, BusinessModel bm)
        {
            if (dicOrder == null || dicOrder.Count <= 0)
            {
                return sql;
            }
            string selectSql = sql + " Order by ";
            List<string> orderSql = new List<string>();
            foreach (var item in dicOrder)
            {
                var table = GetTableAndColum(item.Key, bm);
                orderSql.Add(table.Item1 + "." + table.Item2 + " " + item.Value.ToString());
            }
            selectSql += string.Join(",", orderSql.ToArray());
            return selectSql;
        }

        public static string GetWhere(string sql, List<string> whereColumn)
        {
            List<string> where = new List<string>();
            foreach (var item in whereColumn)
            {
                string columnPrar = string.Empty;
                if (item.IndexOf('.') > 0)
                {
                    columnPrar = item.Split('.')[1];
                }
                else
                {
                    columnPrar = item;
                }
                where.Add(item + "=@" + columnPrar);
            }
            sql += " WHERE " + string.Join(" and ", where.ToArray());
            return sql;
        }

        public static Tuple<string, DynamicParameters> GetEntitileWhere(string sql, List<WhereEntitile> whereList, BusinessModel bm)
        {
            if (whereList == null || whereList.Count() <= 0)
            {
                return new Tuple<string, DynamicParameters>(sql, null);
            }
            List<string> dicKey = new List<string>();
            List<dynamic> dicValue = new List<dynamic>();
            DynamicParameters pars = new DynamicParameters();
            int i = 1;
            foreach (var item in whereList)
            {
                //根据key返回所对应的表和字段
                var where = GetEntitileWhere(item, bm, i.ToString());
                pars.Add(item.filedName + i.ToString(), item.oValue);
                i++;
                var orcorrel = item.orCorrelation;
                while (true)
                {
                    if (orcorrel == null)
                    {
                        break;
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(orcorrel.filedName)) break;
                        where += " or " + GetEntitileWhere(orcorrel, bm, i.ToString());
                        pars.Add(orcorrel.filedName + i.ToString(), orcorrel.oValue);
                        i++;
                        orcorrel = orcorrel.orCorrelation;
                    }
                }
                dicKey.Add("(" + where + ")");

            }
            string selectSql = sql + " WHERE " + string.Join(" and ", dicKey.ToArray());
            return new Tuple<string, DynamicParameters>(selectSql, pars);
        }

        /// <summary>
        /// 设置扩展字段（关联实体的）
        /// </summary>
        /// <param name="odata"></param>
        /// <param name="columList"></param>
        /// <returns></returns>
        public static T SetExteModel(T odata, List<Column> columList)
        {
            if(columList==null || columList.Count<=0)
            {
                return default(T);
            }
            foreach (var item in columList)
            {
                //获取相关实体
                var exteModel = ModelDictionaries.GetDic(item.sourceModel);
                string exSql = exteModel.Item2.selectSql; //返回关联实体sql
                                                          //拼接where条件
                string rColumnCode = item.cloumnid;
                string rMk = item.mappingKeys.Split('=')[1];
                exSql += " WHERE " + rMk + "=@" + rMk.Split('.')[1];
                //反射获取数据值
                dynamic value = DataPropertyHandle.GetObjectValue(odata, item.mappingKeys.Split('=')[0].Replace("this.", ""));
                if (value != null)
                {
                    DynamicParameters pars = new DynamicParameters();
                    pars.Add(rMk.Split('.')[1], value);
                    var extResult = DapperDBContext.Query(exteModel.Item1.modelObj, exSql, pars, databaseOption: exteModel.Item1.dbConnection);
                    //设置实体到
                    if (extResult != null)
                    {
                        if (item.cloumnType == BindingcloumnType.ExteModel)
                        {
                            DataPropertyHandle.SetObjectValue(odata, rColumnCode, extResult.FirstOrDefault());
                        }
                        else if (item.cloumnType == BindingcloumnType.ExteModelList)
                        {
                            DataPropertyHandle.SetObjectListValue(odata, rColumnCode, extResult.ToList(), exteModel.Item1.modelObj);
                        }
                    }
                }
            }
            return odata;
        }

        public static Tuple<long, List<T>> GetMysqlList(string selectSql, string whereSql, DynamicParameters para, string databaseOption, BusinessModel bm, long startLine, long endLine)
        {
            List<T> result = new List<T>();
            long count = 0;
            if (startLine == 0 && endLine == 0)
            {
                result = DapperDBContext.Query<T>(selectSql, para, databaseOption: databaseOption).ToList();
                count = result.Count();
            }
            else
            {
                selectSql += $" LIMIT {startLine}, {endLine};";
                var keyConlum = string.Empty;
                var key = bm.column.Where(p => p.isPrimaryKey).Select(p => p.dataCloumn);
                if (key == null || key.Count() <= 0)
                {
                    keyConlum = "*";
                }
                else
                {
                    keyConlum = string.Join(",", key.ToArray());
                }
                var selectCount = $"Select count({keyConlum}) countNum from {bm.tableName} {whereSql}";
                var multi = DapperDBContext.QueryList<T>(selectSql + selectCount, para, databaseOption: databaseOption);
                result = multi.Item2;
                count = multi.Item1;
            }
            return new Tuple<long, List<T>>(count, result.ToList());
        }

        public static Tuple<long, List<T>> GetSqlList(string selectSql, string whereSql, DynamicParameters para, string databaseOption, BusinessModel bm, long startLine, long endLine)
        {
            List<T> result = new List<T>();
            long count = 0;
            if (startLine == 0 && endLine == 0)
            {
                result = DapperDBContext.Query<T>(selectSql, para, databaseOption: databaseOption).ToList();
                count = result.Count();
            }
            else
            {
                if(selectSql.IndexOf("Order by")>0)
                {
                    selectSql += $" OFFSET {startLine} row FETCH NEXT {endLine} rows only;";
                }
                else
                {
                    var addFile = " ,(row_number() over(order by @@servername))  as LineAddFildnum ";
                    selectSql= selectSql.Insert(selectSql.IndexOf(" from"), addFile)+ $" Order by LineAddFildnum OFFSET {startLine} row FETCH NEXT {endLine} rows only;";
                }
                
                var keyConlum = string.Empty;
                var key = bm.column.Where(p => p.isPrimaryKey).Select(p => p.dataCloumn);
                if (key == null || key.Count() <= 0)
                {
                    keyConlum = "*";
                }
                else
                {
                    keyConlum = string.Join(",", key.ToArray());
                }
                var selectCount = string.Format("Select count({0}) countNum from {1} {2}", keyConlum, bm.tableName, whereSql);
                var multi = DapperDBContext.QueryList<T>(selectSql + selectCount, para, databaseOption: databaseOption);
                result = multi.Item2;
                count = multi.Item1;
            }
            return new Tuple<long, List<T>>(count, result.ToList());
        }

        public static List<Tuple<string, object>> GetInsert(T odata, List<Column> columList)
        {
            if (columList == null || columList.Count() <= 0)
            {
                return null;
            }
            List<Tuple<string, object>> insertOjb = new List<Tuple<string, object>>();
            foreach (var item in columList)
            {
                //1、获取该列值，2、获取该列对应实体，3、获取insert语句
                //反射获取数据值
                dynamic value = DataPropertyHandle.GetObjectValue(odata, item.cloumnid);
                if (value == null)
                {
                    continue;
                }
                //返回对应的实体的insert语言
                //获取相关实体
                var exteModel = ModelDictionaries.GetDic(item.sourceModel);
                string exSql = exteModel.Item2.insertSql; //返回关联实体sql
                insertOjb.Add(new Tuple<string, object>(exSql, value));
            }
            return insertOjb;
        }

        public static Tuple<string, DynamicParameters> GetUpdateEntitileSql(string sql, List<WhereEntitile> whereList, dynamic t, BusinessModel bm)
        {
            DynamicParameters param = new DynamicParameters();
            List<string> where = new List<string>();
            if (whereList != null && whereList.Count() > 0)
            {
                //查询条件
                var item = GetEntitileWhere(sql, whereList, bm);
                param = item.Item2;
                sql = item.Item1;
            }
            //实体参数
            if (t != null)
            {
                //反射属性字段名称及值
                var dic = DataPropertyHandle.GetObjectKeyValue(t, true);
                foreach (var item in dic)
                {
                    param.Add(item.Key, item.Value);
                }
            }
            return new Tuple<string, DynamicParameters>(sql, param);
        }

        public static List<Tuple<string, object>> GetUpdate(T odata, List<Column> columList)
        {
            if (columList == null || columList.Count() <= 0)
            {
                return null;
            }
            List<Tuple<string, object>> updateOjb = new List<Tuple<string, object>>();
            foreach (var item in columList)
            {
                //反射获取数据值
                dynamic value = DataPropertyHandle.GetObjectValue(odata, item.cloumnid);
                if (value == null)
                {
                    continue;
                }
                //获取与主表关联关系，删除已有数据，重新插入
                var exteModel = ModelDictionaries.GetDic(item.sourceModel); //获取实体信息
                string exSql = exteModel.Item2.deleteSql;
                //删除条件设置
                string whereSql = string.Empty;
                string rMk = item.mappingKeys.Split('=')[1];
                exSql += " WHERE " + rMk + "=@" + rMk.Split('.')[1];
                //反射获取主表关联字段数据值
                dynamic valueMapping = DataPropertyHandle.GetObjectValue(odata, item.mappingKeys.Split('=')[0].Replace("this.", ""));
                if (valueMapping != null) //
                {
                    DynamicParameters pars = new DynamicParameters();
                    pars.Add(rMk.Split('.')[1], valueMapping);
                    updateOjb.Add(new Tuple<string, object>(exSql, pars));
                }
                if (value != null)
                {
                    //插入新数据
                    exSql = exteModel.Item2.insertSql;
                    foreach (var model in value)
                    {
                        updateOjb.Add(new Tuple<string, object>(exSql, model));
                    }
                }
            }
            return updateOjb;
        }

        public static List<Tuple<string, object>> GetDelete(T odata, List<Column> columList)
        {
            if (columList == null || columList.Count() <= 0)
            {
                return null;
            }
            List<Tuple<string, object>> deleteOjb = new List<Tuple<string, object>>();
            foreach (var item in columList)
            {
                //获取与主表关联关系，删除已有数据，重新插入
                var exteModel = ModelDictionaries.GetDic(item.sourceModel); //获取实体信息
                string exSql = exteModel.Item2.deleteSql;
                //删除条件设置
                string whereSql = string.Empty;
                string rMk = item.mappingKeys.Split('=')[1];
                exSql += " WHERE " + rMk + "=@" + rMk.Split('.')[1];
                //反射获取主表关联字段数据值
                dynamic valueMapping = DataPropertyHandle.GetObjectValue(odata, item.mappingKeys.Split('=')[0].Replace("this.", ""));
                if (valueMapping != null) //
                {
                    DynamicParameters pars = new DynamicParameters();
                    pars.Add(rMk.Split('.')[1], valueMapping);
                    deleteOjb.Add(new Tuple<string, object>(exSql, pars));
                }
            }
            return deleteOjb;
        }

        private static string GetListWhere(string column, BusinessModel bm)
        {
            //根据key返回所对应的表和字段
            var item = GetTableAndColum(column, bm);
            var where = item.Item1 + "." + item.Item2 + "=@" + column;
            return where;
        }

        private static Tuple<string, string> GetTableAndColum(string column, BusinessModel bm)
        {
            var columnInfo = bm.column.Where(p => p.dataCloumn.Equals(column)).FirstOrDefault();
            string tableName = string.Empty;
            string columnName = string.Empty;
            if (columnInfo.cloumnType == BindingcloumnType.Data)
            {
                tableName = bm.tableName;
            }
            else
            {
                tableName = ModelDictionaries.GetDicModel(columnInfo.sourceModel).tableName;
            }
            columnName = columnInfo.dataCloumn;
            return new Tuple<string, string>(tableName, columnName);
        }

        private static string GetEntitileWhere(WhereEntitile whereItem, BusinessModel bm, string i = "")
        {
            var item = GetTableAndColum(whereItem.filedName, bm);
            string where = string.Empty;
            if (whereItem.op.Equals("Like"))
            {
                where = item.Item1 + "." + item.Item2 + " " + whereItem.op + " concat('%', @" + whereItem.filedName + i + ", '%')";
            }
            else
            {
                where = item.Item1 + "." + item.Item2 + " " + whereItem.op + " @" + whereItem.filedName + i;
            }
            return where;
        }
    }
}
