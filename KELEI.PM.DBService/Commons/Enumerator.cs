using System.ComponentModel;


namespace KELEI.PM.DBService.Commons
{
    public class Enumerator
    {
        public enum ConnectionTypes
        {
            [Description("实时业务库")]
            SQLServer_ConnectionDb = 1
        }

        /// <summary>
        /// 数据库类型
        /// </summary>
        public enum DBType
        {
            [Description("Mysql")]
            Mysql = 0,
            [Description("SQLServer")]
            SQLServer = 1
        }

        /// <summary>
        /// 排序类型
        /// </summary>
        public enum DBOrderbyType
        {
            asc,
            desc,
        }
    }
}
