using KELEI.Commons.Helper;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace KELEI.PM.Entity
{
    [Serializable]
    [ProtoContract]
    [DataClassAttribute(Table = "Base_Employee", Database = "SQLServer:ConnectionDb")]
    public class Base_Employee
    {
        /// <summary>
        /// DataSourceID
        /// </summary>
        [ProtoMember(1)]
        [DataProperty(Field = "UserMail", IsKey = true)]
        public string UserMail
        {
            get;
            set;
        }
        /// <summary>
        /// DataSourceName
        /// </summary>
        [ProtoMember(2)]
        [DataProperty(Field = "UserName")]
        public string UserName
        {
            get;
            set;
        }

        /// <summary>
        /// UserLogs
        /// </summary>
        [ProtoMember(3)]
        [DataProperty(Field = "UserLogs", CloumnType = BindingcloumnType.ExteModelList, MappingKeys = "this.UserMail=Base_EmployeeRole_Log.UserMail", SourceModel = "Base_EmployeeRole_Log")]
        public List<Base_EmployeeRole_Log> UserLogs
        {
            get;
            set;
        }

        /// <summary>
        /// UserRoles
        /// </summary>
        [ProtoMember(4)]
        [DataProperty(Field = "UserRoles", CloumnType = BindingcloumnType.ExteModelList, MappingKeys = "this.UserMail=Base_EmployeeRole.UserMail", SourceModel = "Base_EmployeeRole")]
        public List<Base_EmployeeRole> UserRoles
        {
            get;
            set;
        }
    }

    [Serializable]
    [ProtoContract]
    public class Base_EmployeeList
    {
        [ProtoMember(1)]
        public long CountRow { get; set; }

        [ProtoMember(2)]
        public List<Base_Employee> Base_Employees { get; set; }
    }
}
