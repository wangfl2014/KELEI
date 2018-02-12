using KELEI.Commons.Helper;
using ProtoBuf;
using System;

namespace KELEI.PM.Entity
{
    [DataClassAttribute(Table = "Base_EmployeeRole_Log", Database = "SQLServer:ConnectionDb")]
    [ProtoContract]
    public class Base_EmployeeRole_Log
    {
        public const string SourceTable = "Base_EmployeeRole_Log";
        public const string DatabaseOption = "SQLServer:ConnectionDb";

        /// <summary>
        /// Id
        /// </summary>
        [ProtoMember(1)]
        [DataProperty(Field = "Id", IsKey = true, BindingFlag = BindingFlagType.Select | BindingFlagType.Where)]
        public int Id
        {
            get;
            set;
        }
        /// <summary>
        /// UserMail
        /// </summary>
        [ProtoMember(2)]
        [DataProperty(Field = "UserMail")]
        public string UserMail
        {
            get;
            set;
        }
        /// <summary>
        /// Role_Detail
        /// </summary>
        [ProtoMember(3)]
        [DataProperty(Field = "UserName")]
        public string UserName
        {
            get;
            set;
        }
        /// <summary>
        /// OperaDate
        /// </summary>
        [ProtoMember(4)]
        [DataProperty(Field = "OperaDate")]
        public DateTime OperaDate
        {
            get;
            set;
        }
        /// <summary>
        /// Detail
        /// </summary>
        [ProtoMember(5)]
        [DataProperty(Field = "Detail")]
        public string Detail
        {
            get;
            set;
        }
    }
}
