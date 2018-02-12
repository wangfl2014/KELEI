using KELEI.Commons.Helper;
using System.Collections;
using KELEI.PM.DBService.Commons;
using System.Data;
using System.Collections.Generic;

namespace KELEI.PM.DBService.DBAccess
{
    public class DBConnectObject
    {
        private static Dictionary<string, IDbConnection> dbConnects = null;
        public static Dictionary<string, IDbConnection> DBConnects
        {
            get
            {
                if (dbConnects == null || dbConnects.Count<=0)
                {
                    dbConnects = new Dictionary<string, IDbConnection>();
                    var connectArry = EnumHelper.EnumToDataSource(typeof(Enumerator.ConnectionTypes));
                    foreach(var item in connectArry)
                    {
                        string connect = item.Value.Replace("_", ":");
                        IDbConnection dbObject = ConnectionFactory.CreateConnection(connect);
                        dbConnects.Add(connect, dbObject);
                    }
                }
                return dbConnects;
            }
        }

        public static IDbConnection GetDBObject(string dbkey)
        {
            var connects = DBConnects;
            if (connects.ContainsKey(dbkey))
            {
                return connects[dbkey];
            }
            else
            {
                return null;
            }
        }

        public static void Dispose()
        {
            if(dbConnects == null)
            {
                foreach(var item in dbConnects)
                {
                    item.Value.Close();
                    item.Value.Dispose();
                }
                dbConnects.Clear();
                dbConnects = null;
            }
        }

        ~DBConnectObject()
        {
            if (dbConnects == null)
            {
                foreach (var item in dbConnects)
                {
                    item.Value.Close();
                    item.Value.Dispose();
                }
                dbConnects.Clear();
                dbConnects = null;
            }
        }
    }
}
