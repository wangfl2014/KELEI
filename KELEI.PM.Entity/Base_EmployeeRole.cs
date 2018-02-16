using KELEI.Commons.Helper;
using ProtoBuf;

namespace KELEI.PM.Entity
{
    [DataClassAttribute(Table = "Base_EmployeeRole", Database = "SQLServer:ConnectionDb")]
    [ProtoContract]
    public class Base_EmployeeRole
    {
        public const string SourceTable = "Base_EmployeeRole";
        public const string DatabaseOption = "SQLServer:ConnectionDb";
        /// <summary>
        /// ID
        /// </summary>
        [ProtoMember(1)]
        [DataProperty(Field = "ID", IsKey = true, BindingFlag = BindingFlagType.Select | BindingFlagType.Where)]
        public int ID
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
        /// UserName
        /// </summary>
        [ProtoMember(3)]
        [DataProperty(Field = "UserName")]
        public string UserName
        {
            get;
            set;
        }
        /// <summary>
        /// Role_ID
        /// </summary>
        [ProtoMember(4)]
        [DataProperty(Field = "Role_ID")]
        public int Role_ID
        {
            get;
            set;
        }

        /// <summary>
        /// Role_Name
        /// </summary>
        [ProtoMember(5)]
        [DataProperty(Field = "Role_Name", CloumnType = BindingcloumnType.ExteData, MappingKeys = "this.Role_ID=Base_SystemRole.Role_ID", SourceModel = "Base_SystemRole")]
        public string Role_Name
        {
            get;
            set;
        }

        /// <summary>
        /// Role_Detail
        /// </summary>
        [ProtoMember(6)]
        [DataProperty(Field = "Role_Detail", CloumnType = BindingcloumnType.ExteData, MappingKeys = "this.Role_ID=Base_SystemRole.Role_ID", SourceModel = "Base_SystemRole")]
        public string Role_Detail
        {
            get;
            set;
        }

        /// <summary>
        /// RoleType
        /// </summary>
        [ProtoMember(7)]
        [DataProperty(Field = "RoleType", CloumnType = BindingcloumnType.ExteData, MappingKeys = "this.Role_ID=Base_SystemRole.Role_ID", SourceModel = "Base_SystemRole")]
        public string RoleType
        {
            get;
            set;
        }

        /// <summary>
        /// RangeID
        /// </summary>
        [ProtoMember(8)]
        [DataProperty(Field = "RangeID")]
        public string RangeID
        {
            get;
            set;
        }
        /// <summary>
        /// IsBackup
        /// </summary>
        [ProtoMember(9)]
        [DataProperty(Field = "IsBackup")]
        public int IsBackup
        {
            get;
            set;
        }
        /// <summary>
        /// ConUserMail
        /// </summary>
        [ProtoMember(10)]
        [DataProperty(Field = "ConUserMail")]
        public string ConUserMail
        {
            get;
            set;
        }
    }
}
