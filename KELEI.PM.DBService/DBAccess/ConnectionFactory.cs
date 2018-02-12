using System;
using System.Data;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using KELEI.PM.DBService.Commons;
using KELEI.Commons.Helper;

namespace KELEI.PM.DBService.DBAccess
{
    public class ConnectionFactory
    {
        /// <summary>
        /// 返回数据库连接对象
        /// </summary>
        /// <param name="databaseOption">web.config中的数据库连接字符串的Key，注意Key的命名规则【数据库类型:链接字符串】</param>
        /// 例如Key格式 Mysql:ConnectionDb,SQLServer:ConnectionDb
        /// ConstSet.DefaultDBConnection常量中配置的默认数据库连接字符串的key
        /// <returns></returns>
        public static IDbConnection CreateConnection(string databaseOption = ConstSet.DefaultDBConnection)
        {
            try
            {
                var connectionArry = databaseOption.Split(':');
                var dbType = connectionArry.Length <= 1 ? "SQLServer" : connectionArry[0];
                IDbConnection conn;
                switch ((Enumerator.DBType)Enum.Parse(typeof(Enumerator.DBType), dbType))
                {
                    case Enumerator.DBType.Mysql:
                        conn = new MySqlConnection(databaseOption);
                        break;
                    case Enumerator.DBType.SQLServer:
                        conn = new SqlConnection(databaseOption);
                        break;
                    default:
                        conn = new MySqlConnection(databaseOption);
                        break;
                }
                conn.Open();
                return conn;
            }
            catch(Exception ex)
            {
                LogHelper.Error($"数据库链接错误-ConnectionFactory.CreateConnection：{ex}");
                throw ex;
            }
        }
    }
}