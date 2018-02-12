using System;
using KELEI.PM.DBService.Commons;
using KELEI.PM.DBService.DBAccess;

namespace KELEI.PM.DBService.Repository
{
    public class BaseRepository<T> where T : class
    {
        public IRepository<T> repository {
            get
            {
                var dic = RepositoryHelp<T>.GetDic();
                var dbType = dic.Item1.dbConnection.Split(':')[0];
                switch ((Enumerator.DBType)Enum.Parse(typeof(Enumerator.DBType), dbType))
                {
                    case Enumerator.DBType.Mysql:
                        return new MysqlRepository<T>();
                    case Enumerator.DBType.SQLServer:
                        return new SqlServerRepository<T>();
                    default:
                        return new MysqlRepository<T>();
                }
            }
        }
    }
}
