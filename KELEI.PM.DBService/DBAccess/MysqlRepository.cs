using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using KELEI.Commons.Helper;
using KELEI.PM.DBService.Commons;

namespace KELEI.PM.DBService.DBAccess
{
    public class MysqlRepository<T>: IRepository<T> where T : class
    {
        #region 获取单条记录
        /// <summary>
        ///     根据Id获取实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="primaryId"></param>
        /// <returns></returns>
        public T GetByKey(params dynamic[] primaryArr)
        {
            try
            {
                //返回select语句
                var dic = RepositoryHelp<T>.GetDic();
                string selectSql = dic.Item2.selectSql;
                //主键where条件
                var key = dic.Item1.column.Where(p => p.isPrimaryKey).Select(p => p.dataCloumn).ToList();
                selectSql = RepositoryHelp<T>.GetWhere(selectSql, key) + " LIMIT 0, 1;";
                DynamicParameters pars = new DynamicParameters();
                for (int i = 0; i < key.Count(); i++)
                {
                    pars.Add(key[i], primaryArr[i]);
                }
                var result = DapperDBContext.Query<T>(selectSql, pars, databaseOption: dic.Item1.dbConnection).FirstOrDefault();
                if (result != null)
                {
                    //填充ExteModel,ExteModelList
                    //获取ExteModel类型
                    var columnList = dic.Item1.column.Where(p => p.cloumnType == BindingcloumnType.ExteModel && !p.isLazy)?.ToList();
                    result = RepositoryHelp<T>.SetExteModel(result, columnList);
                    //获取并填充ExteModelList
                    columnList = dic.Item1.column.Where(p => p.cloumnType == BindingcloumnType.ExteModelList && !p.isLazy)?.ToList();
                    result = RepositoryHelp<T>.SetExteModel(result, columnList);
                }
                return result;
            }
            catch (Exception ex)
            {
                LogHelper.Error($"返回实体信息错误-MysqlRepository.GetByKey：{ex}");
                return default(T);
            }
        }

        public T GetByKey(object keyObj)
        {
            try
            {
                var dic = RepositoryHelp<T>.GetDicByobj(keyObj);
                var result = GetByWhere(dic);
                return result;
            }
            catch (Exception ex)
            {
                LogHelper.Error($"返回实体信息错误-MysqlRepository.GetByKey：{ex}");
                return default(T);
            }

        }

        public T GetByWhere(Dictionary<string, dynamic> dic, Dictionary<string, Enumerator.DBOrderbyType> orderby = null)
        {
            try
            {
                //返回select语句
                var dicModel = RepositoryHelp<T>.GetDic();
                string selectSql = dicModel.Item2.selectSql;
                var item = RepositoryHelp<T>.GetDicWhere(selectSql, dic, dicModel.Item1);
                selectSql = RepositoryHelp<T>.GetOrderby(item.Item1, orderby, dicModel.Item1);
                selectSql = selectSql + " LIMIT 0, 1;";
                var result = DapperDBContext.Query<T>(selectSql, item.Item2, databaseOption: dicModel.Item1.dbConnection).FirstOrDefault();
                if (result != null)
                {
                    //填充ExteModel,ExteModelList
                    //获取ExteModel类型
                    var columnList = dicModel.Item1.column.Where(p => p.cloumnType == BindingcloumnType.ExteModel && !p.isLazy)?.ToList();
                    result = RepositoryHelp<T>.SetExteModel(result, columnList);
                    //获取并填充ExteModelList
                    columnList = dicModel.Item1.column.Where(p => p.cloumnType == BindingcloumnType.ExteModelList && !p.isLazy)?.ToList();
                    result = RepositoryHelp<T>.SetExteModel(result, columnList);
                }
                return result;
            }
            catch (Exception ex)
            {
                LogHelper.Error($"返回实体信息错误-MysqlRepository.GetByWhere：{ex}");
                return default(T);
            }
        }

        public T GetByWhere(object whereObj, Dictionary<string, Enumerator.DBOrderbyType> orderby = null)
        {
            try
            {
                var dic = RepositoryHelp<T>.GetDicByobj(whereObj);
                var result = GetByWhere(dic, orderby);
                return result;
            }
            catch (Exception ex)
            {
                LogHelper.Error($"返回实体信息错误-MysqlRepository.GetByWhere：{ex}");
                return default(T);
            }
        }

        public T GetByWhere(List<WhereEntitile> whereList = null, Dictionary<string, Enumerator.DBOrderbyType> orderby = null)
        {
            try
            {
                //返回select语句
                var dicModel = RepositoryHelp<T>.GetDic();
                string selectSql = dicModel.Item2.selectSql;
                var item = RepositoryHelp<T>.GetEntitileWhere(selectSql, whereList, dicModel.Item1);
                selectSql = RepositoryHelp<T>.GetOrderby(item.Item1, orderby, dicModel.Item1);
                selectSql = selectSql + " LIMIT 0, 1;";
                var result = DapperDBContext.Query<T>(selectSql, item.Item2, databaseOption: dicModel.Item1.dbConnection).FirstOrDefault();
                if (result != null)
                {
                    //填充ExteModel,ExteModelList
                    //获取ExteModel类型
                    var columnList = dicModel.Item1.column.Where(p => p.cloumnType == BindingcloumnType.ExteModel && !p.isLazy)?.ToList();
                    result = RepositoryHelp<T>.SetExteModel(result, columnList);
                    //获取并填充ExteModelList
                    columnList = dicModel.Item1.column.Where(p => p.cloumnType == BindingcloumnType.ExteModelList && !p.isLazy)?.ToList();
                    result = RepositoryHelp<T>.SetExteModel(result, columnList);
                }
                return result;
            }
            catch (Exception ex)
            {
                LogHelper.Error($"返回实体信息错误-MysqlRepository.GetByEntitile：{ex}");
                return default(T);
            }
        }
        #endregion

        #region 根据查询条件获取多条记录
        /// <summary>
        /// 字典条件，and key=value形式
        /// </summary>
        /// <param name="dic"></param>
        /// <param name="orderby"></param>
        /// <param name="startLine"></param>
        /// <param name="endLine"></param>
        /// <returns></returns>
        public Tuple<long, List<T>> GetListByObj(object whereObj, Dictionary<string, Enumerator.DBOrderbyType> orderby = null, long startLine = 0, long endLine = 0)
        {
            try
            {
                //返回select语句
                var dic = RepositoryHelp<T>.GetDicByobj(whereObj);
                var result = GetListByDic(dic, orderby, startLine, endLine);
                return new Tuple<long, List<T>>(result.Item1, result.Item2);
            }
            catch (Exception ex)
            {
                LogHelper.Error($"返回实体信息错误-MysqlRepository.GetListByObj：{ex}");
                return new Tuple<long, List<T>>(0, null);
            }
        }

        /// <summary>
        /// 字典条件，and key=value形式
        /// </summary>
        /// <param name="dic"></param>
        /// <param name="orderby"></param>
        /// <param name="startLine"></param>
        /// <param name="endLine"></param>
        /// <returns></returns>
        public Tuple<long, List<T>> GetListByDic(Dictionary<string, dynamic> dic = null, Dictionary<string, Enumerator.DBOrderbyType> orderby = null, long startLine = 0, long endLine = 0)
        {
            try
            {
                //返回select语句
                var dicModel = RepositoryHelp<T>.GetDic();
                string selectSql = dicModel.Item2.selectSql;
                var item = RepositoryHelp<T>.GetDicWhere(selectSql, dic, dicModel.Item1);
                string whereSql = item.Item1.Replace(selectSql, "");
                selectSql = RepositoryHelp<T>.GetOrderby(item.Item1, orderby, dicModel.Item1);
                var result = RepositoryHelp<T>.GetMysqlList(selectSql, whereSql, item.Item2, dicModel.Item1.dbConnection, dicModel.Item1, startLine, endLine);
                return new Tuple<long, List<T>>(result.Item1, result.Item2);
            }
            catch (Exception ex)
            {
                LogHelper.Error($"返回实体信息错误-MysqlRepository.GetListByDic：{ex}");
                return new Tuple<long, List<T>>(0, null);
            }
        }

        /// <summary>
        /// 查询条件实体，支持or，<,=,>,<=,>=,like
        /// </summary>
        /// <param name="whereList"></param>
        /// <param name="orderby"></param>
        /// <param name="startLine"></param>
        /// <param name="endLine"></param>
        /// <returns></returns>
        public Tuple<long, List<T>> GetListByEntitile(List<WhereEntitile> whereList = null, Dictionary<string, Enumerator.DBOrderbyType> orderby = null, long startLine = 0, long endLine = 0)
        {
            try
            {
                //返回select语句
                var dicModel = RepositoryHelp<T>.GetDic();
                string selectSql = dicModel.Item2.selectSql;
                var item = RepositoryHelp<T>.GetEntitileWhere(selectSql, whereList, dicModel.Item1);
                string whereSql = item.Item1.Replace(selectSql, "");
                selectSql = RepositoryHelp<T>.GetOrderby(item.Item1, orderby, dicModel.Item1);
                var result = RepositoryHelp<T>.GetMysqlList(selectSql, whereSql, item.Item2, dicModel.Item1.dbConnection, dicModel.Item1, startLine, endLine);
                return new Tuple<long, List<T>>(result.Item1, result.Item2);
            }
            catch (Exception ex)
            {
                LogHelper.Error($"返回实体信息错误-MysqlRepository.GetListByEntitile：{ex}");
                return new Tuple<long, List<T>>(0, null);
            }
        }
        #endregion

        #region 插入数据
        public Tuple<bool, string> InsertEntitile(T t)
        {
            try
            {
                //返回insert语句
                var dicModel = RepositoryHelp<T>.GetDic();
                string selectSql = dicModel.Item2.insertSql;
                List<Tuple<string, object>> insertObject = new List<Tuple<string, object>>();
                insertObject.Add(new Tuple<string, object>(selectSql, t));
                //判断是否有子表
                //一对一关联表
                var columnList = dicModel.Item1.column.Where(p => p.cloumnType == BindingcloumnType.ExteModel).ToList();
                var reult = RepositoryHelp<T>.GetInsert(t, columnList);
                if (reult != null)
                    insertObject.AddRange(reult);
                //一对多关联表
                columnList = dicModel.Item1.column.Where(p => p.cloumnType == BindingcloumnType.ExteModelList).ToList();
                reult = RepositoryHelp<T>.GetInsert(t, columnList);
                if (reult != null)
                    insertObject.AddRange(reult);
                var resultData = DapperDBContext.ExecuteTransaction(insertObject, databaseOption: dicModel.Item1.dbConnection);
                return resultData;
            }
            catch (Exception ex)
            {
                LogHelper.Error($"返回实体信息错误-MysqlRepository.InsertEntitile：{ex}");
                return new Tuple<bool, string>(false, ex.ToString());
            }
        }

        public Tuple<bool, string> InsertEntitile(List<T> tList)
        {
            try
            {
                var dicModel = RepositoryHelp<T>.GetDic();
                List<Tuple<string, object>> insertObject = new List<Tuple<string, object>>();
                foreach(var t in tList)
                {
                    string selectSql = dicModel.Item2.insertSql;
                    insertObject.Add(new Tuple<string, object>(selectSql, t));
                    //一对一关联表
                    var columnList = dicModel.Item1.column.Where(p => p.cloumnType == BindingcloumnType.ExteModel).ToList();
                    var reult = RepositoryHelp<T>.GetInsert(t, columnList);
                    if (reult != null)
                        insertObject.AddRange(reult);
                    //一对多关联表
                    columnList = dicModel.Item1.column.Where(p => p.cloumnType == BindingcloumnType.ExteModelList).ToList();
                    reult = RepositoryHelp<T>.GetInsert(t, columnList);
                    if (reult != null)
                        insertObject.AddRange(reult);
                }
                var resultData = DapperDBContext.ExecuteTransaction(insertObject, databaseOption: dicModel.Item1.dbConnection);
                return resultData;
            }
            catch(Exception ex)
            {
                LogHelper.Error($"返回实体信息错误-MysqlRepository.InsertEntitile：{ex}");
                return new Tuple<bool, string>(false, ex.ToString());
            }
        }
        #endregion

        #region 修改数据
        /// <summary>
        /// 修改实体，主键值不变，以主键为修改条件
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public Tuple<bool, string> UpdateEntitile(T t)
        {
            try
            {
                //根据实体主键修改数据，要求主键值不修改
                //返回update语句
                var dicModel = RepositoryHelp<T>.GetDic();
                string updateSql = dicModel.Item2.updateSql;
                var key = dicModel.Item1.column.Where(p => p.isPrimaryKey).ToList();//返回主键
                List<WhereEntitile> whereList = new List<WhereEntitile>();
                foreach (var item in key)
                {
                    whereList.Add(new WhereEntitile() { filedName = item.cloumnid, op = "=", oValue = DataPropertyHandle.GetObjectValue(t, item.cloumnid) });
                }
                var result = RepositoryHelp<T>.GetUpdateEntitileSql(updateSql, whereList, t, dicModel.Item1);
                List<Tuple<string, object>> updateObject = new List<Tuple<string, object>>();
                updateObject.Add(new Tuple<string, object>(result.Item1, result.Item2));
                //关联实体修改一对一
                var columnList = dicModel.Item1.column.Where(p => p.cloumnType == BindingcloumnType.ExteModel).ToList();
                var sql = RepositoryHelp<T>.GetUpdate(t, columnList);
                if (sql != null && sql.Count() > 0)
                {
                    updateObject.AddRange(sql);
                }

                //关联实体修改一对多
                columnList = dicModel.Item1.column.Where(p => p.cloumnType == BindingcloumnType.ExteModelList).ToList();
                sql = RepositoryHelp<T>.GetUpdate(t, columnList);
                if (sql != null && sql.Count() > 0)
                {
                    updateObject.AddRange(sql);
                }
                var resultData = DapperDBContext.ExecuteTransaction(updateObject, databaseOption: dicModel.Item1.dbConnection);
                return resultData;
            }
            catch (Exception ex)
            {
                LogHelper.Error($"返回实体信息错误-MysqlRepository.UpdateEntitile：{ex}");
                return new Tuple<bool, string>(false, ex.ToString());
            }
        }

        /// <summary>
        /// 以字典为修改条件，key=value，中间使用and 
        /// </summary>
        /// <param name="t"></param>
        /// <param name="dic"></param>
        /// <returns></returns>
        public Tuple<bool, string> UpdateEntitile(T t, Dictionary<string, dynamic> dic)
        {
            try
            {
                //根据实体主键修改数据，要求主键值不修改
                var dicModel = RepositoryHelp<T>.GetDic();
                string updateSql = dicModel.Item2.updateSql;
                List<WhereEntitile> whereList = new List<WhereEntitile>();
                foreach (var item in dic)
                {
                    whereList.Add(new WhereEntitile() { filedName = item.Key, op = "=", oValue = item.Value });
                }
                var result = RepositoryHelp<T>.GetUpdateEntitileSql(updateSql, whereList, t, dicModel.Item1);
                List<Tuple<string, object>> updateObject = new List<Tuple<string, object>>();
                //关联实体修改一对一
                var columnList = dicModel.Item1.column.Where(p => p.cloumnType == BindingcloumnType.ExteModel).ToList();
                var sql = RepositoryHelp<T>.GetUpdate(t, columnList);
                if (sql != null && sql.Count() > 0)
                {
                    updateObject.AddRange(sql);
                }
                //关联实体修改一对多
                columnList = dicModel.Item1.column.Where(p => p.cloumnType == BindingcloumnType.ExteModelList).ToList();
                sql = RepositoryHelp<T>.GetUpdate(t, columnList);
                if (sql != null && sql.Count() > 0)
                {
                    updateObject.AddRange(sql);
                }
                var resultData = DapperDBContext.ExecuteTransaction(updateObject, databaseOption: dicModel.Item1.dbConnection);
                return resultData;
            }
            catch (Exception ex)
            {
                LogHelper.Error($"返回实体信息错误-MysqlRepository.UpdateEntitile：{ex}");
                return new Tuple<bool, string>(false, ex.ToString());
            }
        }

        /// <summary>
        /// 实体修改条件
        /// </summary>
        /// <param name="t"></param>
        /// <param name="whereList"></param>
        /// <returns></returns>
        public Tuple<bool, string> UpdateEntitile(T t, List<WhereEntitile> whereList)
        {
            try
            {
                //指定条件更新
                var dicModel = RepositoryHelp<T>.GetDic();
                string updateSql = dicModel.Item2.updateSql;
                var result = RepositoryHelp<T>.GetUpdateEntitileSql(updateSql, whereList, t, dicModel.Item1);
                List<Tuple<string, object>> updateObject = new List<Tuple<string, object>>();
                //关联实体修改一对一
                var columnList = dicModel.Item1.column.Where(p => p.cloumnType == BindingcloumnType.ExteModel).ToList();
                var sql = RepositoryHelp<T>.GetUpdate(t, columnList);
                if (sql != null && sql.Count() > 0)
                {
                    updateObject.AddRange(sql);
                }
                //关联实体修改一对多
                columnList = dicModel.Item1.column.Where(p => p.cloumnType == BindingcloumnType.ExteModelList).ToList();
                sql = RepositoryHelp<T>.GetUpdate(t, columnList);
                if (sql != null && sql.Count() > 0)
                {
                    updateObject.AddRange(sql);
                }
                var resultData = DapperDBContext.ExecuteTransaction(updateObject, databaseOption: dicModel.Item1.dbConnection);
                return resultData;
            }
            catch (Exception ex)
            {
                LogHelper.Error($"返回实体信息错误-MysqlRepository.UpdateEntitile：{ex}");
                return new Tuple<bool, string>(false, ex.ToString());
            }
        }

        public Tuple<bool, string> UpdateData(object setValue, object whereObj)
        {
            try
            {
                var dic = RepositoryHelp<T>.GetDicByobj(setValue);
                var whereDic = RepositoryHelp<T>.GetDicByobj(whereObj);
                return UpdateData(dic, whereDic);
            }
            catch(Exception ex)
            {
                LogHelper.Error($"返回实体信息错误-MysqlRepository.UpdateData：{ex}");
                return new Tuple<bool, string>(false, ex.ToString());
            }

        }
        /// <summary>
        /// 指定修改字段，对数据进行修改
        /// </summary>
        /// <param name="dic"></param>
        /// <param name="whereList"></param>
        /// <returns></returns>
        public Tuple<bool, string> UpdateData(Dictionary<string, dynamic> dic, Dictionary<string, dynamic> whereDic)
        {
            try
            {
                //根据指定条件，修改指定字段
                var dicModel = RepositoryHelp<T>.GetDic();
                string tableName = dicModel.Item1.tableName;
                List<string> updateList = new List<string>();
                foreach (var item in dic)
                {
                    updateList.Add(item.Key + "=@" + item.Key);
                }
                var updateSql = string.Format("UPDATE {0} SET {1}", tableName, string.Join(",", updateList));
                var itemWhere = RepositoryHelp<T>.GetDicWhere(updateSql, whereDic, dicModel.Item1);
                DynamicParameters param = new DynamicParameters();
                param = itemWhere.Item2;
                foreach (var dicItem in dic)
                {
                    param.Add(dicItem.Key, dicItem.Value);

                }
                var resultData = DapperDBContext.Execute(itemWhere.Item1, param, databaseOption: dicModel.Item1.dbConnection);
                if (resultData > 0)
                {
                    return new Tuple<bool, string>(true, "更新" + resultData + "行");
                }
                else
                {
                    return new Tuple<bool, string>(false, "更新0行");
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error($"返回实体信息错误-MysqlRepository.UpdateData：{ex}");
                return new Tuple<bool, string>(false, ex.ToString());
            }
        }
        /// <summary>
        /// 指定修改字段，对数据进行修改
        /// </summary>
        /// <param name="dic"></param>
        /// <param name="whereList"></param>
        /// <returns></returns>
        public Tuple<bool, string> UpdateData(Dictionary<string, dynamic> dic, List<WhereEntitile> whereList)
        {
            try
            {
                //根据指定条件，修改指定字段
                var dicModel = RepositoryHelp<T>.GetDic();
                string tableName = dicModel.Item1.tableName;
                List<string> updateList = new List<string>();
                foreach (var item in dic)
                {
                    updateList.Add(item + "=@" + item);
                }
                var updateSql = string.Format("UPDATE {0} SET {1}", tableName, string.Join(",", updateList));
                var itemWhere = RepositoryHelp<T>.GetEntitileWhere(updateSql, whereList, dicModel.Item1);
                DynamicParameters param = new DynamicParameters();
                param = itemWhere.Item2;
                foreach (var dicItem in dic)
                {
                    param.Add(dicItem.Key, dicItem.Value);

                }
                var resultData = DapperDBContext.Execute(itemWhere.Item1, param, databaseOption: dicModel.Item1.dbConnection);
                if (resultData > 0)
                {
                    return new Tuple<bool, string>(true, "更新" + resultData + "行");
                }
                else
                {
                    return new Tuple<bool, string>(false, "更新0行");
                }
            }
            catch (Exception ex)
            {
                return new Tuple<bool, string>(false, ex.ToString());
            }
        }
        #endregion

        #region 删除数据
        /// <summary>
        /// 删除实体，并且删除关联的实体
        /// </summary>
        /// <param name="primaryArr"></param>
        /// <returns></returns>
        public Tuple<bool, string> DeleteEntitile(dynamic[] primaryArr)
        {
            try
            {
                var dicModel = RepositoryHelp<T>.GetDic();
                var DeleteEnt = GetByKey(primaryArr);//返回待删除的实体
                List<Tuple<string, object>> deleteObject = new List<Tuple<string, object>>();
                //删除实体关联的其他实体
                if (DeleteEnt != null)
                {
                    //删除一对一关系关联表数据
                    var columnList = dicModel.Item1.column.Where(p => p.cloumnType == BindingcloumnType.ExteModel).ToList();
                    var deleteMapp = RepositoryHelp<T>.GetDelete(DeleteEnt, columnList);
                    if (deleteMapp != null && deleteMapp.Count > 0)
                    {
                        deleteObject.AddRange(deleteMapp);
                    }
                    //删除一对多关系关联数据
                    columnList = dicModel.Item1.column.Where(p => p.cloumnType == BindingcloumnType.ExteModelList).ToList();
                    deleteMapp = RepositoryHelp<T>.GetDelete(DeleteEnt, columnList);
                    if (deleteMapp != null && deleteMapp.Count > 0)
                    {
                        deleteObject.AddRange(deleteMapp);
                    }
                }
                string deleteSql = dicModel.Item2.deleteSql;
                //主键where条件
                var key = dicModel.Item1.column.Where(p => p.isPrimaryKey).Select(p => p.dataCloumn).ToList();
                deleteSql = RepositoryHelp<T>.GetWhere(deleteSql, key);
                DynamicParameters pars = new DynamicParameters();
                for (int i = 0; i < key.Count(); i++)
                {
                    pars.Add(key[i], primaryArr[i]);
                }
                deleteObject.Add(new Tuple<string, object>(deleteSql, pars));

                var resultData = DapperDBContext.ExecuteTransaction(deleteObject, databaseOption: dicModel.Item1.dbConnection);
                return resultData;
            }
            catch (Exception ex)
            {
                LogHelper.Error($"删除信息错误-MysqlRepository.DeleteEntitile：{ex}");
                return new Tuple<bool, string>(false, ex.ToString());
            }
        }

        public Tuple<bool, string> DeleteEntitile(object whereObj)
        {
            try
            {
                var dic = RepositoryHelp<T>.GetDicByobj(whereObj);
                var result = DeleteEntitile(dic);
                return result;
            }
            catch (Exception ex)
            {
                LogHelper.Error($"删除信息错误-MysqlRepository.DeleteEntitile：{ex}");
                return new Tuple<bool, string>(false, ex.ToString());
            }
        }
        public Tuple<bool, string> DeleteEntitile(Dictionary<string, dynamic> dic)
        {
            try
            {
                var dicModel = RepositoryHelp<T>.GetDic();
                var DeleteEnt = GetByKey(dic);//返回待删除的实体
                List<Tuple<string, object>> deleteObject = new List<Tuple<string, object>>();
                //删除实体关联的其他实体
                if (DeleteEnt != null)
                {
                    //删除一对一关系关联表数据
                    var columnList = dicModel.Item1.column.Where(p => p.cloumnType == BindingcloumnType.ExteModel).ToList();
                    var deleteMapp = RepositoryHelp<T>.GetDelete(DeleteEnt, columnList);
                    if (deleteMapp != null && deleteMapp.Count > 0)
                    {
                        deleteObject.AddRange(deleteMapp);
                    }
                    //删除一对多关系关联数据
                    columnList = dicModel.Item1.column.Where(p => p.cloumnType == BindingcloumnType.ExteModelList).ToList();
                    deleteMapp = RepositoryHelp<T>.GetDelete(DeleteEnt, columnList);
                    if (deleteMapp != null && deleteMapp.Count > 0)
                    {
                        deleteObject.AddRange(deleteMapp);
                    }
                }
                string deleteSql = dicModel.Item2.deleteSql;

                var deletePars = RepositoryHelp<T>.GetDicWhere(deleteSql, dic, dicModel.Item1);
                deleteObject.Add(new Tuple<string, object>(deletePars.Item1, deletePars.Item2));

                var resultData = DapperDBContext.ExecuteTransaction(deleteObject, databaseOption: dicModel.Item1.dbConnection);
                return resultData;
            }
            catch (Exception ex)
            {
                LogHelper.Error($"删除信息错误-MysqlRepository.DeleteEntitile：{ex}");
                return new Tuple<bool, string>(false, ex.ToString());
            }
        }

        public Tuple<bool, string> DeleteEntitileMultiple(Dictionary<string, dynamic> dic)
        {
            try
            {
                var dicModel = RepositoryHelp<T>.GetDic();
                var DeleteEnt = GetListByDic(dic);//返回待删除的实体
                List<Tuple<string, object>> deleteObject = new List<Tuple<string, object>>();
                //删除实体关联的其他实体
                if (DeleteEnt.Item2 != null)
                {
                    foreach (var DeleteItem in DeleteEnt.Item2)
                    {
                        //删除一对一关系关联表数据
                        var columnList = dicModel.Item1.column.Where(p => p.cloumnType == BindingcloumnType.ExteModel).ToList();
                        var deleteMapp = RepositoryHelp<T>.GetDelete(DeleteItem, columnList);
                        if (deleteMapp != null && deleteMapp.Count > 0)
                        {
                            deleteObject.AddRange(deleteMapp);
                        }
                        //删除一对多关系关联数据
                        columnList = dicModel.Item1.column.Where(p => p.cloumnType == BindingcloumnType.ExteModelList).ToList();
                        deleteMapp = RepositoryHelp<T>.GetDelete(DeleteItem, columnList);
                        if (deleteMapp != null && deleteMapp.Count > 0)
                        {
                            deleteObject.AddRange(deleteMapp);
                        }
                    }
                }
                string deleteSql = dicModel.Item2.deleteSql;

                var deletePars = RepositoryHelp<T>.GetDicWhere(deleteSql, dic, dicModel.Item1);
                deleteObject.Add(new Tuple<string, object>(deletePars.Item1, deletePars.Item2));

                var resultData = DapperDBContext.ExecuteTransaction(deleteObject, databaseOption: dicModel.Item1.dbConnection);
                return resultData;
            }
            catch (Exception ex)
            {
                LogHelper.Error($"删除信息错误-MysqlRepository.DeleteEntitileMultiple：{ex}");
                return new Tuple<bool, string>(false, ex.ToString());
            }
        }

        public Tuple<bool, string> DeleteEntitile(List<WhereEntitile> whereList)
        {
            try
            {
                var dicModel = RepositoryHelp<T>.GetDic();
                var DeleteEnt = GetListByEntitile(whereList);//返回待删除的实体
                List<Tuple<string, object>> deleteObject = new List<Tuple<string, object>>();
                //删除实体关联的其他实体
                if (DeleteEnt.Item2 != null)
                {
                    foreach (var DeleteItem in DeleteEnt.Item2)
                    {
                        //删除一对一关系关联表数据
                        var columnList = dicModel.Item1.column.Where(p => p.cloumnType == BindingcloumnType.ExteModel).ToList();
                        var deleteMapp = RepositoryHelp<T>.GetDelete(DeleteItem, columnList);
                        if (deleteMapp != null && deleteMapp.Count > 0)
                        {
                            deleteObject.AddRange(deleteMapp);
                        }
                        //删除一对多关系关联数据
                        columnList = dicModel.Item1.column.Where(p => p.cloumnType == BindingcloumnType.ExteModelList).ToList();
                        deleteMapp = RepositoryHelp<T>.GetDelete(DeleteItem, columnList);
                        if (deleteMapp != null && deleteMapp.Count > 0)
                        {
                            deleteObject.AddRange(deleteMapp);
                        }
                    }
                }
                string deleteSql = dicModel.Item2.deleteSql;

                var deletePars = RepositoryHelp<T>.GetEntitileWhere(deleteSql, whereList, dicModel.Item1);
                deleteObject.Add(new Tuple<string, object>(deletePars.Item1, deletePars.Item2));

                var resultData = DapperDBContext.ExecuteTransaction(deleteObject, databaseOption: dicModel.Item1.dbConnection);
                return resultData;
            }
            catch (Exception ex)
            {
                LogHelper.Error($"删除信息错误-MysqlRepository.DeleteEntitileMultiple：{ex}");
                return new Tuple<bool, string>(false, ex.ToString());
            }
        }
        #endregion
    }
}
