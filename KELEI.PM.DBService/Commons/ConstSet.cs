using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KELEI.PM.DBService.Commons
{
    public class ConstSet
    {
        /// <summary>
        /// 数据库连接配置
        /// </summary>
        public const string DefaultDBConnection = "SQLServer:ConnectionDb";
        public const string RsqlConnection = "SQLServer:RsqlConnection";

        /// <summary>
        /// model实体所在程序集
        /// </summary>
        public const string SystemModelAssembly = "SystemModelAssembly";

        /// <summary>
        /// 数据访问抽象类与实现类所在程序集
        /// </summary>
        public const string SystemRepositoryAssembly = "SystemRepositoryAssembly";

        /// <summary>
        /// redis配置
        /// </summary>
        public const string RedisDataBaseIndex = "RedisDataBaseIndex";//redis使用库
        public const string RedisServer = "RedisServer";//redis服务地址
    }
}
