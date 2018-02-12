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

        /// <summary>
        /// model实体所在程序集
        /// </summary>
        public const string SystemModelAssembly = "SystemModelAssembly";

        /// <summary>
        /// 数据访问抽象类与实现类所在程序集
        /// </summary>
        public const string SystemRepositoryAssembly = "SystemRepositoryAssembly";

        /// <summary>
        /// RPC监听服务端配置
        /// </summary>
        public const string NetMQListenerObjectXML = "NetMQListenerObjectXML";//RPC服务接口服务XML配置文件
        public const string NetMQListenerObjectNodeName = "NetMQListenerObjectNodeName";//RPC服务接口服务配置文件节点

        /// <summary>
        /// redis配置
        /// </summary>
        public const string RedisDataBaseIndex = "RedisDataBaseIndex";//redis使用库
        public const string RedisServer = "RedisServer";//redis服务地址


        public const string ServerDescription = "ServerDescription";
        public const string ServerDisplayName = "ServerDisplayName";
        public const string ServerServiceName = "ServerServiceName";

        public const string NetMQIPAddress = "NetMQIPAddress";
    }
}
