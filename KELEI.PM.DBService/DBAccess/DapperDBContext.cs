using Dapper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KELEI.PM.DBService.Commons;
using KELEI.Commons.Helper;

namespace KELEI.PM.DBService.DBAccess
{
    public static class DapperDBContext
    {
        public static List<T> AsList<T>(this IEnumerable<T> source)
        {
            if (source != null && !(source is List<T>))
                return source.ToList();
            return (List<T>)source;
        }

        public static int Execute(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null, string databaseOption = ConstSet.DefaultDBConnection)
        {
            try
            {
                var conn = DBConnectObject.GetDBObject(databaseOption);
                if(conn!=null)
                {
                    var info = "SQL语句:" + sql + "  \n SQL参数: " + JsonConvert.SerializeObject(param) + " \n";
                    LogHelper.Info(info);
                    var sw = new Stopwatch(); sw.Start();
                    var restult = conn.Execute(sql, param, transaction, commandTimeout, commandType);
                    sw.Stop();
                    LogHelper.Info(info + "耗时:" + sw.ElapsedMilliseconds + (sw.ElapsedMilliseconds > 1000 ? "#####" : string.Empty) + "\n");
                    return restult;
                }
                else
                {
                    LogHelper.Error("SQL语句:" + sql + "  \n SQL命令类型:数据库链接null ");
                    throw new Exception("数据库链接null");
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("SQL语句:" + sql + "  \n SQL参数: " + JsonConvert.SerializeObject(param) + "数据库执行错误，错误为：" + ex.ToString());
                throw ex;
            }
        }

        public static int Execute(CommandDefinition command, string databaseOption = ConstSet.DefaultDBConnection)
        {
            try
            {
                var conn = DBConnectObject.GetDBObject(databaseOption);
                if (conn != null)
                {
                    var info = " SQL语句:" + command.CommandText + "  \n SQL命令类型: " + command.CommandType + " \n";
                    LogHelper.Info(info);
                    var sw = new Stopwatch(); sw.Start();
                    var restult = conn.Execute(command);
                    sw.Stop();
                    LogHelper.Info(info + "耗时:" + sw.ElapsedMilliseconds + (sw.ElapsedMilliseconds > 1000 ? "#####" : string.Empty) + "\n");
                    return restult;
                }
                else
                {
                    LogHelper.Error("SQL语句:" + command.CommandText + "  \n SQL命令类型:数据库链接null ");
                    throw new Exception("数据库链接null");
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("SQL语句:" + command.CommandText + "  \n SQL命令类型: " + command.CommandType + "数据库执行错误，错误为：" + ex.ToString());
                throw ex;
            }
        }

        public static object ExecuteScalar(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null, string databaseOption = ConstSet.DefaultDBConnection)
        {
            try
            {
                var conn = DBConnectObject.GetDBObject(databaseOption);
                if (conn != null)
                {
                    var info = "SQL语句:" + sql + "  \n SQL参数: " + JsonConvert.SerializeObject(param) + " \n";
                    LogHelper.Info(info);
                    var sw = new Stopwatch(); sw.Start();
                    var restult = conn.ExecuteScalar(sql, param, transaction, commandTimeout, commandType);
                    sw.Stop();
                    LogHelper.Info(info + "耗时:" + sw.ElapsedMilliseconds + (sw.ElapsedMilliseconds > 1000 ? "#####" : string.Empty) + "\n");
                    return restult;
                }
                else
                {
                    LogHelper.Error("SQL语句:" + sql + "  \n SQL命令类型:数据库链接null ");
                    throw new Exception("数据库链接null");
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("SQL语句:" + sql + "  \n SQL参数: " + JsonConvert.SerializeObject(param) + "数据库执行错误，错误为：" + ex.ToString());
                throw ex;
            }
        }

        public static T ExecuteScalar<T>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null, string databaseOption = ConstSet.DefaultDBConnection)
        {
            try
            {
                var conn = DBConnectObject.GetDBObject(databaseOption);
                if (conn != null)
                {
                    var info = "SQL语句:" + sql + "  \n SQL参数: " + JsonConvert.SerializeObject(param) + " \n";
                    LogHelper.Info(info);
                    var sw = new Stopwatch(); sw.Start();
                    var restult = conn.ExecuteScalar<T>(sql, param, transaction, commandTimeout, commandType);
                    sw.Stop();
                    LogHelper.Info(info + "耗时:" + sw.ElapsedMilliseconds + (sw.ElapsedMilliseconds > 1000 ? "#####" : string.Empty) + "\n");
                    return restult;
                }
                else
                {
                    LogHelper.Error("SQL语句:" + sql + "  \n SQL命令类型:数据库链接null ");
                    throw new Exception("数据库链接null");
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("SQL语句:" + sql + "  \n SQL参数: " + JsonConvert.SerializeObject(param) + "数据库执行错误，错误为：" + ex.ToString());
                throw ex;
            }
        }

        public static object ExecuteScalar(CommandDefinition command, string databaseOption = ConstSet.DefaultDBConnection)
        {
            try
            {
                var conn = DBConnectObject.GetDBObject(databaseOption);
                if (conn != null)
                {
                    var info = " SQL语句:" + command.CommandText + "  \n SQL命令类型: " + command.CommandType + " \n";
                    LogHelper.Info(info);
                    var sw = new Stopwatch(); sw.Start();
                    var restult = conn.ExecuteScalar(command);
                    sw.Stop();
                    LogHelper.Info(info + "耗时:" + sw.ElapsedMilliseconds + (sw.ElapsedMilliseconds > 1000 ? "#####" : string.Empty) + "\n");
                    return restult;
                }
                else
                {
                    LogHelper.Error("SQL语句:" + command.CommandText + "  \n SQL命令类型:数据库链接null ");
                    throw new Exception("数据库链接null");
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("SQL语句:" + command.CommandText + "  \n SQL命令类型: " + command.CommandType + "数据库执行错误，错误为：" + ex.ToString());
                throw ex;
            }
        }

        public static T ExecuteScalar<T>(CommandDefinition command, string databaseOption = ConstSet.DefaultDBConnection)
        {
            try
            {
                var conn = DBConnectObject.GetDBObject(databaseOption);
                if (conn != null)
                {
                    var info = " SQL语句:" + command.CommandText + "  \n SQL命令类型: " + command.CommandType + " \n";
                    LogHelper.Info(info);
                    var sw = new Stopwatch(); sw.Start();
                    var restult = conn.ExecuteScalar<T>(command);
                    sw.Stop();
                    LogHelper.Info(info + "耗时:" + sw.ElapsedMilliseconds + (sw.ElapsedMilliseconds > 1000 ? "#####" : string.Empty) + "\n");
                    return restult;
                }
                else
                {
                    LogHelper.Error("SQL语句:" + command.CommandText + "  \n SQL命令类型:数据库链接null ");
                    throw new Exception("数据库链接null");
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("SQL语句:" + command.CommandText + "  \n SQL命令类型: " + command.CommandType + "数据库执行错误，错误为：" + ex.ToString());
                throw ex;
            }
        }


        public static Task<IEnumerable<T>> QueryAsync<T>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null, string databaseOption = ConstSet.DefaultDBConnection)
        {
            try
            {
                var conn = DBConnectObject.GetDBObject(databaseOption);
                if (conn != null)
                {
                    var info = "SQL语句:" + sql + "  \n SQL参数: " + JsonConvert.SerializeObject(param) + " \n";
                    LogHelper.Info(info);
                    var sw = new Stopwatch(); sw.Start();
                    var restult = conn.QueryAsync<T>(sql, param, transaction, commandTimeout, commandType);
                    sw.Stop();
                    LogHelper.Info(info + "耗时:" + sw.ElapsedMilliseconds + (sw.ElapsedMilliseconds > 1000 ? "#####" : string.Empty) + "\n");
                    return restult;
                }
                else
                {
                    LogHelper.Error("SQL语句:" + sql + "  \n SQL命令类型:数据库链接null ");
                    throw new Exception("数据库链接null");
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("SQL语句:" + sql + "  \n SQL参数: " + JsonConvert.SerializeObject(param) + "数据库执行错误，错误为：" + ex.ToString());
                throw ex;
            }
        }

        public static IEnumerable<TReturn> Query<TFirst, TSecond, TReturn>(string sql, Func<TFirst, TSecond, TReturn> map, object param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null, string databaseOption = ConstSet.DefaultDBConnection)
        {
            try
            {
                var conn = DBConnectObject.GetDBObject(databaseOption);
                if (conn != null)
                {
                    var info = "SQL语句:" + sql + "  \n SQL参数: " + JsonConvert.SerializeObject(param) + " \n";
                    LogHelper.Info(info);
                    var sw = new Stopwatch(); sw.Start();
                    var restult = conn.Query(sql, map, param, transaction, buffered, splitOn, commandTimeout, commandType);
                    sw.Stop(); LogHelper.Info(info + "耗时:" + sw.ElapsedMilliseconds + (sw.ElapsedMilliseconds > 1000 ? "#####" : string.Empty) + "\n");
                    return restult;
                }
                else
                {
                    LogHelper.Error("SQL语句:" + sql + "  \n SQL命令类型:数据库链接null ");
                    throw new Exception("数据库链接null");
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("SQL语句:" + sql + "  \n SQL参数: " + JsonConvert.SerializeObject(param) + "数据库执行错误，错误为：" + ex.ToString());
                throw ex;
            }
        }

        public static IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TReturn>(string sql, Func<TFirst, TSecond, TThird, TReturn> map, object param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null, string databaseOption = ConstSet.DefaultDBConnection)
        {
            try
            {
                var conn = DBConnectObject.GetDBObject(databaseOption);
                if (conn != null)
                {
                    var info = "SQL语句:" + sql + "  \n SQL参数: " + JsonConvert.SerializeObject(param) + " \n";
                    LogHelper.Info(info);
                    var sw = new Stopwatch(); sw.Start();
                    var restult = conn.Query(sql, map, param, transaction, buffered, splitOn, commandTimeout, commandType);
                    sw.Stop();
                    LogHelper.Info(info + "耗时:" + sw.ElapsedMilliseconds + (sw.ElapsedMilliseconds > 1000 ? "#####" : string.Empty) + "\n");
                    return restult;
                }
                else
                {
                    LogHelper.Error("SQL语句:" + sql + "  \n SQL命令类型:数据库链接null ");
                    throw new Exception("数据库链接null");
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("SQL语句:" + sql + "  \n SQL参数: " + JsonConvert.SerializeObject(param) + "数据库执行错误，错误为：" + ex.ToString());
                throw ex;
            }
        }

        public static IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TFourth, TReturn>(string sql, Func<TFirst, TSecond, TThird, TFourth, TReturn> map, object param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null, string databaseOption = ConstSet.DefaultDBConnection)
        {
            try
            {
                var conn = DBConnectObject.GetDBObject(databaseOption);
                if (conn != null)
                {
                    var info = "SQL语句:" + sql + "  \n SQL参数: " + JsonConvert.SerializeObject(param) + " \n";
                    LogHelper.Info(info);
                    var sw = new Stopwatch(); sw.Start();
                    var restult = conn.Query(sql, map, param, transaction, buffered, splitOn, commandTimeout, commandType);
                    sw.Stop();
                    LogHelper.Info(info + "耗时:" + sw.ElapsedMilliseconds + (sw.ElapsedMilliseconds > 1000 ? "#####" : string.Empty) + "\n");
                    return restult;
                }
                else
                {
                    LogHelper.Error("SQL语句:" + sql + "  \n SQL命令类型:数据库链接null ");
                    throw new Exception("数据库链接null");
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("SQL语句:" + sql + "  \n SQL参数: " + JsonConvert.SerializeObject(param) + "数据库执行错误，错误为：" + ex.ToString());
                throw ex;
            }
        }

        public static IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TFourth, TFifth, TReturn>(string sql, Func<TFirst, TSecond, TThird, TFourth, TFifth, TReturn> map, object param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null, string databaseOption = ConstSet.DefaultDBConnection)
        {
            try
            {
                var conn = DBConnectObject.GetDBObject(databaseOption);
                if (conn != null)
                {
                    var info = "SQL语句:" + sql + "  \n SQL参数: " + JsonConvert.SerializeObject(param) + " \n";
                    LogHelper.Info(info);
                    var sw = new Stopwatch(); sw.Start();
                    var restult = conn.Query(sql, map, param, transaction, buffered, splitOn, commandTimeout, commandType);
                    sw.Stop();
                    LogHelper.Info(info + "耗时:" + sw.ElapsedMilliseconds + (sw.ElapsedMilliseconds > 1000 ? "#####" : string.Empty) + "\n");
                    return restult;
                }
                else
                {
                    LogHelper.Error("SQL语句:" + sql + "  \n SQL命令类型:数据库链接null ");
                    throw new Exception("数据库链接null");
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("SQL语句:" + sql + "  \n SQL参数: " + JsonConvert.SerializeObject(param) + "数据库执行错误，错误为：" + ex.ToString());
                throw ex;
            }
        }

        public static IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn>(string sql, Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn> map, object param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null, string databaseOption = ConstSet.DefaultDBConnection)
        {
            try
            {
                var conn = DBConnectObject.GetDBObject(databaseOption);
                if (conn != null)
                {
                    var info = "SQL语句:" + sql + "  \n SQL参数: " + JsonConvert.SerializeObject(param) + " \n";
                    LogHelper.Info(info);
                    var sw = new Stopwatch(); sw.Start();
                    var restult = conn.Query(sql, map, param, transaction, buffered, splitOn, commandTimeout, commandType);
                    sw.Stop(); LogHelper.Info(info + "耗时:" + sw.ElapsedMilliseconds + (sw.ElapsedMilliseconds > 1000 ? "#####" : string.Empty) + "\n");
                    return restult;
                }
                else
                {
                    LogHelper.Error("SQL语句:" + sql + "  \n SQL命令类型:数据库链接null ");
                    throw new Exception("数据库链接null");
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("SQL语句:" + sql + "  \n SQL参数: " + JsonConvert.SerializeObject(param) + "数据库执行错误，错误为：" + ex.ToString());
                throw ex;
            }
        }

        public static IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn>(string sql, Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn> map, object param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null, string databaseOption = ConstSet.DefaultDBConnection)
        {
            try
            {
                var conn = DBConnectObject.GetDBObject(databaseOption);
                if (conn != null)
                {
                    var info = "SQL语句:" + sql + "  \n SQL参数: " + JsonConvert.SerializeObject(param) + " \n";
                    LogHelper.Info(info);
                    var sw = new Stopwatch(); sw.Start();
                    var restult = conn.Query(sql, map, param, transaction, buffered, splitOn, commandTimeout, commandType);
                    sw.Stop();
                    LogHelper.Info(info + "耗时:" + sw.ElapsedMilliseconds + (sw.ElapsedMilliseconds > 1000 ? "#####" : string.Empty) + "\n");
                    return restult;
                }
                else
                {
                    LogHelper.Error("SQL语句:" + sql + "  \n SQL命令类型:数据库链接null ");
                    throw new Exception("数据库链接null");
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("SQL语句:" + sql + "  \n SQL参数: " + JsonConvert.SerializeObject(param) + "数据库执行错误，错误为：" + ex.ToString());
                throw ex;
            }
        }

        public static IEnumerable<TReturn> Query<TReturn>(string sql, Type[] types, Func<object[], TReturn> map, object param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null, string databaseOption = ConstSet.DefaultDBConnection)
        {
            try
            {
                var conn = DBConnectObject.GetDBObject(databaseOption);
                if (conn != null)
                {
                    var info = "SQL语句:" + sql + "  \n SQL参数: " + JsonConvert.SerializeObject(param) + " \n";
                    LogHelper.Info(info);
                    var sw = new Stopwatch(); sw.Start();
                    var restult = conn.Query(sql, types, map, param, transaction, buffered, splitOn, commandTimeout, commandType);
                    sw.Stop();
                    LogHelper.Info(info + "耗时:" + sw.ElapsedMilliseconds + (sw.ElapsedMilliseconds > 1000 ? "#####" : string.Empty) + "\n");
                    return restult;
                }
                else
                {
                    LogHelper.Error("SQL语句:" + sql + "  \n SQL命令类型:数据库链接null ");
                    throw new Exception("数据库链接null");
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("SQL语句:" + sql + "  \n SQL参数: " + JsonConvert.SerializeObject(param) + "数据库执行错误，错误为：" + ex.ToString());
                throw ex;
            }
        }


        public static IDataReader ExecuteReader(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null, string databaseOption = ConstSet.DefaultDBConnection)
        {
            try
            {
                var conn = DBConnectObject.GetDBObject(databaseOption);
                if (conn != null)
                {
                    var info = "SQL语句:" + sql + "  \n SQL参数: " + JsonConvert.SerializeObject(param) + " \n";
                    LogHelper.Info(info);
                    var sw = new Stopwatch(); sw.Start();
                    var restult = conn.ExecuteReader(sql, param, transaction, commandTimeout, commandType);
                    sw.Stop();
                    LogHelper.Info(info + "耗时:" + sw.ElapsedMilliseconds + (sw.ElapsedMilliseconds > 1000 ? "#####" : string.Empty) + "\n");
                    return restult;
                }
                else
                {
                    LogHelper.Error("SQL语句:" + sql + "  \n SQL命令类型:数据库链接null ");
                    throw new Exception("数据库链接null");
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("SQL语句:" + sql + "  \n SQL参数: " + JsonConvert.SerializeObject(param) + "数据库执行错误，错误为：" + ex.ToString());
                throw ex;
            }
        }

        public static IDataReader ExecuteReader(CommandDefinition command, string databaseOption = ConstSet.DefaultDBConnection)
        {
            try
            {
                var conn = DBConnectObject.GetDBObject(databaseOption);
                if (conn != null)
                {
                    var info = " SQL语句:" + command.CommandText + "  \n SQL命令类型: " + command.CommandType + " \n";
                    LogHelper.Info(info);
                    var sw = new Stopwatch(); sw.Start();
                    var restult = conn.ExecuteReader(command);
                    sw.Stop();
                    LogHelper.Info(info + "耗时:" + sw.ElapsedMilliseconds + (sw.ElapsedMilliseconds > 1000 ? "#####" : string.Empty) + "\n");
                    return restult;
                }
                else
                {
                    LogHelper.Error("SQL语句:" + command.CommandText + "  \n SQL命令类型:数据库链接null ");
                    throw new Exception("数据库链接null");
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("SQL语句:" + command.CommandText + "  \n SQL命令类型: " + command.CommandType + "数据库执行错误，错误为：" + ex.ToString());
                throw ex;
            }
        }

        public static IDataReader ExecuteReader(CommandDefinition command, CommandBehavior commandBehavior, string databaseOption = ConstSet.DefaultDBConnection)
        {
            try
            {
                var conn = DBConnectObject.GetDBObject(databaseOption);
                if (conn != null)
                {
                    var info = " SQL语句:" + command.CommandText + "  \n SQL命令类型: " + command.CommandType + " \n";
                    LogHelper.Info(info);
                    var sw = new Stopwatch(); sw.Start();
                    var restult = conn.ExecuteReader(command, commandBehavior);
                    sw.Stop();
                    LogHelper.Info(info + "耗时:" + sw.ElapsedMilliseconds + (sw.ElapsedMilliseconds > 1000 ? "#####" : string.Empty) + "\n");
                    return restult;
                }
                else
                {
                    LogHelper.Error("SQL语句:" + command.CommandText + "  \n SQL命令类型:数据库链接null ");
                    throw new Exception("数据库链接null");
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("SQL语句:" + command.CommandText + "  \n SQL命令类型: " + command.CommandType + "数据库执行错误，错误为：" + ex.ToString());
                throw ex;
            }
        }

        public static IEnumerable<object> Query(string sql, object param = null, IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null, string databaseOption = ConstSet.DefaultDBConnection)
        {
            try
            {
                var conn = DBConnectObject.GetDBObject(databaseOption);
                if (conn != null)
                {
                    var info = "SQL语句:" + sql + "  \n SQL参数: " + JsonConvert.SerializeObject(param) + " \n";
                    LogHelper.Info(info);
                    var sw = new Stopwatch(); sw.Start();
                    var restult = conn.Query(sql, param, transaction, buffered, commandTimeout, commandType);
                    sw.Stop();
                    LogHelper.Info(info + "耗时:" + sw.ElapsedMilliseconds + (sw.ElapsedMilliseconds > 1000 ? "#####" : string.Empty) + "\n");
                    return restult;
                }
                else
                {
                    LogHelper.Error("SQL语句:" + sql + "  \n SQL命令类型:数据库链接null ");
                    throw new Exception("数据库链接null");
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("SQL语句:" + sql + "  \n SQL参数: " + JsonConvert.SerializeObject(param) + "数据库执行错误，错误为：" + ex.ToString());
                throw ex;
            }
        }

        public static IEnumerable<T> Query<T>(string sql, object param = null, IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null, string databaseOption = ConstSet.DefaultDBConnection)
        {
            try
            {
                var conn = DBConnectObject.GetDBObject(databaseOption);
                if (conn != null)
                {
                    var info = "SQL语句:" + sql + "  \n SQL参数: " + JsonConvert.SerializeObject(param) + " \n";
                    LogHelper.Info(info);
                    var sw = new Stopwatch(); sw.Start();
                    var restult = conn.Query<T>(sql, param, transaction, buffered, commandTimeout, commandType);
                    sw.Stop();
                    LogHelper.Info(info + "耗时:" + sw.ElapsedMilliseconds + (sw.ElapsedMilliseconds > 1000 ? "#####" : string.Empty) + "\n");
                    return restult;
                }
                else
                {
                    LogHelper.Error("SQL语句:" + sql + "  \n SQL命令类型:数据库链接null ");
                    throw new Exception("数据库链接null");
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("SQL语句:" + sql + "  \n SQL参数: " + JsonConvert.SerializeObject(param) + "数据库执行错误，错误为：" + ex.ToString());
                throw ex;
            }
        }

        public static Tuple<long, List<T>> QueryList<T>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null, string databaseOption = ConstSet.DefaultDBConnection)
        {
            try
            {
                var conn = DBConnectObject.GetDBObject(databaseOption);
                if (conn != null)
                {
                    var info = "SQL语句:" + sql + "  \n SQL参数: " + JsonConvert.SerializeObject(param) + " \n";
                    LogHelper.Info(info);
                    var sw = new Stopwatch(); sw.Start();
                    var restult = conn.QueryMultiple(sql, param, transaction, commandTimeout, commandType);
                    var restultList = restult.Read<T>().ToList();
                    var restultCount = restult.Read<Int64>().First();
                    sw.Stop();
                    LogHelper.Info(info + "耗时:" + sw.ElapsedMilliseconds + (sw.ElapsedMilliseconds > 1000 ? "#####" : string.Empty) + "\n");
                    return new Tuple<long, List<T>>(restultCount, restultList);
                }
                else
                {
                    LogHelper.Error("SQL语句:" + sql + "  \n SQL命令类型:数据库链接null ");
                    throw new Exception("数据库链接null");
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("SQL语句:" + sql + "  \n SQL参数: " + JsonConvert.SerializeObject(param) + "数据库执行错误，错误为：" + ex.ToString());
                throw ex;
            }
        }

        public static IEnumerable<object> Query(Type type, string sql, object param = null, IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null, string databaseOption = ConstSet.DefaultDBConnection)
        {
            try
            {
                var conn = DBConnectObject.GetDBObject(databaseOption);
                if (conn != null)
                {
                    var info = "SQL语句:" + sql + "  \n SQL参数: " + JsonConvert.SerializeObject(param) + " \n";
                    LogHelper.Info(info);
                    var sw = new Stopwatch(); sw.Start();
                    var restult = conn.Query(type, sql, param, transaction, buffered, commandTimeout, commandType);
                    sw.Stop();
                    LogHelper.Info(info + "耗时:" + sw.ElapsedMilliseconds + (sw.ElapsedMilliseconds > 1000 ? "#####" : string.Empty) + "\n");
                    return restult;
                }
                else
                {
                    LogHelper.Error("SQL语句:" + sql + "  \n SQL命令类型:数据库链接null ");
                    throw new Exception("数据库链接null");
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("SQL语句:" + sql + "  \n SQL参数: " + JsonConvert.SerializeObject(param) + "数据库执行错误，错误为：" + ex.ToString());
                throw ex;
            }
        }

        public static IEnumerable<T> Query<T>(CommandDefinition command, string databaseOption = ConstSet.DefaultDBConnection)
        {
            try
            {
                var conn = DBConnectObject.GetDBObject(databaseOption);
                if (conn != null)
                {
                    var info = " SQL语句:" + command.CommandText + "  \n SQL命令类型: " + command.CommandType + " \n";
                    LogHelper.Info(info);
                    var sw = new Stopwatch(); sw.Start();
                    var restult = conn.Query<T>(command);
                    sw.Stop();
                    LogHelper.Info(info + "耗时:" + sw.ElapsedMilliseconds + (sw.ElapsedMilliseconds > 1000 ? "#####" : string.Empty) + "\n");
                    return restult;
                }
                else
                {
                    LogHelper.Error("SQL语句:" + command.CommandText + "  \n SQL命令类型:数据库链接null ");
                    throw new Exception("数据库链接null");
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("SQL语句:" + command.CommandText + "  \n SQL命令类型: " + command.CommandType + "数据库执行错误，错误为：" + ex.ToString());
                throw ex;
            }
        }

        public static SqlMapper.GridReader QueryMultiple(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null, string databaseOption = ConstSet.DefaultDBConnection)
        {
            var conn = DBConnectObject.GetDBObject(databaseOption);
            if (conn != null)
            {
                try
                {
                    var info = "SQL语句:" + sql + "  \n SQL参数: " + JsonConvert.SerializeObject(param) + " \n";
                    LogHelper.Info(info);
                    var sw = new Stopwatch(); sw.Start();
                    var list = conn.QueryMultiple(sql, param);
                    sw.Stop();
                    LogHelper.Info(info + "耗时:" + sw.ElapsedMilliseconds + (sw.ElapsedMilliseconds > 1000 ? "#####" : string.Empty) + "\n");
                    return list;
                }
                catch (Exception ex)
                {
                    LogHelper.Info("SQL语句:" + sql + "  \n SQL参数: " + ex.InnerException.ToString() + " \n");
                    throw ex;
                }
            }
            else
            {
                LogHelper.Error("SQL语句:" + sql + "  \n SQL命令类型:数据库链接null ");
                throw new Exception("数据库链接null");
            }
        }

        public static SqlMapper.GridReader QueryMultiple(CommandDefinition command, string databaseOption = ConstSet.DefaultDBConnection)
        {
            try
            {
                var conn = DBConnectObject.GetDBObject(databaseOption);
                if (conn != null)
                {
                    var info = " SQL语句:" + command.CommandText + "  \n SQL命令类型: " + command.CommandType + " \n";
                    LogHelper.Info(info);
                    var sw = new Stopwatch(); sw.Start();
                    var restult = conn.QueryMultiple(command);
                    sw.Stop();
                    LogHelper.Info(info + "耗时:" + sw.ElapsedMilliseconds + (sw.ElapsedMilliseconds > 1000 ? "#####" : string.Empty) + "\n");
                    return restult;
                }
                else
                {
                    LogHelper.Error("SQL语句:" + command.CommandText + "  \n SQL命令类型:数据库链接null ");
                    throw new Exception("数据库链接null");
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("SQL语句:" + command.CommandText + "  \n SQL命令类型: " + command.CommandType + "数据库执行错误，错误为：" + ex.ToString());
                throw ex;
            }
        }

        public static Task<int> ExecuteAsync(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null, string databaseOption = ConstSet.DefaultDBConnection)
        {
            try
            {
                var conn = DBConnectObject.GetDBObject(databaseOption);
                if (conn != null)
                {
                    var info = "SQL语句:" + sql + "  \n SQL参数: " + JsonConvert.SerializeObject(param) + " \n";
                    LogHelper.Info(info);
                    var sw = new Stopwatch(); sw.Start();
                    var restult = conn.ExecuteAsync(sql, param, transaction, commandTimeout, commandType);
                    sw.Stop();
                    LogHelper.Info(info + "耗时:" + sw.ElapsedMilliseconds + (sw.ElapsedMilliseconds > 1000 ? "#####" : string.Empty) + "\n");
                    return restult;
                }
                else
                {
                    LogHelper.Error("SQL语句:" + sql + "  \n SQL命令类型:数据库链接null ");
                    throw new Exception("数据库链接null");
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("SQL语句:" + sql + "  \n SQL参数: " + JsonConvert.SerializeObject(param) + "数据库执行错误，错误为：" + ex.ToString());
                throw ex;
            }
        }

        public static Task<int> ExecuteAsync(CommandDefinition command, string databaseOption = ConstSet.DefaultDBConnection)
        {
            try
            {
                var conn = DBConnectObject.GetDBObject(databaseOption);
                if (conn != null)
                {
                    var info = " SQL语句:" + command.CommandText + "  \n SQL命令类型: " + command.CommandType + " \n";
                    LogHelper.Info(info);
                    var sw = new Stopwatch(); sw.Start();
                    var restult = conn.ExecuteAsync(command);
                    sw.Stop();
                    LogHelper.Info(info + "耗时:" + sw.ElapsedMilliseconds + (sw.ElapsedMilliseconds > 1000 ? "#####" : string.Empty) + "\n");
                    return restult;
                }
                else
                {
                    LogHelper.Error("SQL语句:" + command.CommandText + "  \n SQL命令类型:数据库链接null ");
                    throw new Exception("数据库链接null");
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("SQL语句:" + command.CommandText + "  \n SQL命令类型: " + command.CommandType + "数据库执行错误，错误为：" + ex.ToString());
                throw ex;
            }
        }

        public static Tuple<bool, string> ExecuteTransaction(List<Tuple<string, object>> trans, string databaseOption = ConstSet.DefaultDBConnection, int? commandTimeout = null)
        {
            if (!trans.Any()) return new Tuple<bool, string>(false, "执行事务SQL语句不能为空！");
            var conn = DBConnectObject.GetDBObject(databaseOption);
            if (conn != null)
            {
                using (var transaction = conn.BeginTransaction())
                {
                    try
                    {
                        var sb = new StringBuilder("ExecuteTransaction 事务： ");
                        foreach (var tran in trans)
                        {
                            sb.Append("SQL语句:" + tran.Item1 + "  \n SQL参数: " + JsonConvert.SerializeObject(tran.Item2) + " \n");
                            //LogHelper.Info("SQL语句:" + tran.Item1 + "  \n SQL参数: " + JsonConvert.SerializeObject(tran.Item2) + " \n");
                            conn.Execute(tran.Item1, tran.Item2, transaction, commandTimeout);
                        }
                        var sw = new Stopwatch();
                        sw.Start();
                        transaction.Commit();
                        sw.Stop(); LogHelper.Info(sb.ToString() + "耗时:" + sw.ElapsedMilliseconds + (sw.ElapsedMilliseconds > 1000 ? "#####" : string.Empty) + "\n");
                        return new Tuple<bool, string>(true, string.Empty);
                    }
                    catch (Exception ex)
                    {
                        //todo:!!!transaction rollback can not work.
                        transaction.Rollback();
                        conn.Close();
                        conn.Dispose();
                        LogHelper.Error("数据库执行错误，错误为：" + ex.ToString());
                        return new Tuple<bool, string>(false, ex.ToString());
                    }
                    finally
                    {
                        conn.Close();
                        conn.Dispose();
                    }
                }
            }
            else
            {
                LogHelper.Error("SQL命令类型:数据库链接null ");
                throw new Exception("数据库链接null");
            }
        }

    }
}
