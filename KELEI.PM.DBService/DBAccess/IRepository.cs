using System;
using System.Collections.Generic;
using KELEI.PM.DBService.Commons;

namespace KELEI.PM.DBService.DBAccess
{
    public interface IRepository<T> where T : class
    {
        #region 获取单条记录
        /// <summary>
        /// 根据主键，返回一条数据
        /// </summary>
        /// <param name="keyObj">主键对象,多主键按顺序对应</param>
        /// <returns></returns>
        T GetByKey(params dynamic[] primaryArr);

        /// <summary>
        /// 根据主键，返回一条数据
        /// </summary>
        /// <param name="keyObj">主键对象,支持联合主键，参数名称需要和主键名称相同</param>
        /// <returns></returns>
        T GetByKey(object keyObj);

        /// <summary>
        /// 根据查询条件返回一条数据
        /// </summary>
        /// <param name="dic">查询条件<字段,值>多个使用and链接</param>
        /// <param name="orderby">排序</param>
        /// <returns></returns>
        T GetByWhere(Dictionary<string, dynamic> dic, Dictionary<string, Enumerator.DBOrderbyType> orderby = null);

        /// <summary>
        /// 根据查询条件返回一条数据
        /// </summary>
        /// <param name="whereObj">查询条件，多个使用and链接，需要有字段名=值，或变了名等于字段名</param>
        /// <param name="orderby">排序</param>
        /// <returns></returns>
        T GetByWhere(object whereObj, Dictionary<string, Enumerator.DBOrderbyType> orderby = null);

        /// <summary>
        /// 根据查询条件，返回一条数据
        /// </summary>
        /// <param name="whereList"></param>
        /// <param name="orderby"></param>
        /// <returns></returns>
        T GetByWhere(List<WhereEntitile> whereList = null, Dictionary<string, Enumerator.DBOrderbyType> orderby = null);
        #endregion

        #region 根据查询条件获取多条记录
        /// <summary>
        /// 根据查询条件返回List实体
        /// </summary>
        /// <param name="whereObj">查询条件,字段=值，多条件中间为and</param>
        /// <param name="orderby">排序</param>
        /// <param name="startLine">起始行</param>
        /// <param name="endLine">检索行数</param>
        /// <returns></returns>
        Tuple<long, List<T>> GetListByObj(object whereObj, Dictionary<string, Enumerator.DBOrderbyType> orderby = null, long startLine = 0, long endLine = 0);

        /// <summary>
        /// 根据查询条件返回List实体
        /// </summary>
        /// <param name="dic">查询条件，字典<字段,值>，多条件中间为and</param>
        /// <param name="orderby">排序</param>
        /// <param name="startLine">起始行</param>
        /// <param name="endLine">检索行数</param>
        /// <returns></returns>
        Tuple<long, List<T>> GetListByDic(Dictionary<string, dynamic> dic = null, Dictionary<string, Enumerator.DBOrderbyType> orderby = null, long startLine = 0, long endLine = 0);

        /// <summary>
        /// 根据查询条件返回List实体
        /// </summary>
        /// <param name="whereList">查询条件，支持=，>=,<=,>,<,like,and ,or</param>
        /// <param name="orderby">排序</param>
        /// <param name="startLine">起始行</param>
        /// <param name="endLine">检索行数</param>
        /// <returns></returns>
        Tuple<long, List<T>> GetListByEntitile(List<WhereEntitile> whereList = null, Dictionary<string, Enumerator.DBOrderbyType> orderby = null, long startLine = 0, long endLine = 0);
        #endregion

        #region 插入数据
        /// <summary>
        /// 将实体插入到数据库
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        Tuple<bool, string> InsertEntitile(T t);

        /// <summary>
        /// 将实体列表插入到数据库
        /// </summary>
        /// <param name="tList"></param>
        /// <returns></returns>
        Tuple<bool, string> InsertEntitile(List<T> tList);
        #endregion

        #region 修改数据
        /// <summary>
        /// 更新实体，主键值不变，以主键为修改条件
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        Tuple<bool, string> UpdateEntitile(T t);

        /// <summary>
        /// 更新实体，以字典为修改条件，key=value，中间使用and 
        /// </summary>
        /// <param name="t"></param>
        /// <param name="dic"></param>
        /// <returns></returns>
        Tuple<bool, string> UpdateEntitile(T t, Dictionary<string, dynamic> dic);

        // <summary>
        /// 更新实体，
        /// </summary>
        /// <param name="t"></param>
        /// <param name="whereList"></param>
        /// <returns></returns>
        Tuple<bool, string> UpdateEntitile(T t, List<WhereEntitile> whereList);

        /// <summary>
        /// 单表更新,根据条件，更新单个表字段
        /// </summary>
        /// <param name="setValue">待更新字段,字段=值</param>
        /// <param name="whereObj">查询条件 字段=值，多个中间为and</param>
        /// <returns></returns>
        Tuple<bool, string> UpdateData(object setValue, object whereObj);

        /// <summary>
        /// 单表更新,根据条件，更新单个表字段
        /// </summary>
        /// <param name="dic">待更新字段</param>
        /// <param name="whereDic">查询条件，多个中间为and</param>
        /// <returns></returns>
        Tuple<bool, string> UpdateData(Dictionary<string, dynamic> dic, Dictionary<string, dynamic> whereDic);

        /// <summary>
        /// 指定修改字段，对数据进行修改
        /// </summary>
        /// <param name="dic">待更新字段</param>
        /// <param name="whereList">查询条件</param>
        /// <returns></returns>
        Tuple<bool, string> UpdateData(Dictionary<string, dynamic> dic, List<WhereEntitile> whereList);
        #endregion

        /// <summary>
        /// 根据主键删除实体
        /// </summary>
        /// <param name="primaryArr">主键对象,多主键按顺序对应</param>
        /// <returns></returns>
        Tuple<bool, string> DeleteEntitile(params dynamic[] primaryArr);

        /// <summary>
        /// 根据条件删除实体
        /// </summary>
        /// <param name="whereObj">删除条件 字段=值</param>
        /// <returns></returns>
        Tuple<bool, string> DeleteEntitile(object whereObj);

        /// <summary>
        /// 根据条件删除实体
        /// </summary>
        /// <param name="dic">删除条件 <字段,值></字段></param>
        /// <returns></returns>
        Tuple<bool, string> DeleteEntitile(Dictionary<string, dynamic> dic);

        /// <summary>
        /// 根据条件删除实体
        /// </summary>
        /// <param name="whereList">删除条件</param>
        /// <returns></returns>
        Tuple<bool, string> DeleteEntitile(List<WhereEntitile> whereList);
    }
}
