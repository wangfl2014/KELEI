using KELEI.Commons.Helper;
using ProtoBuf;

namespace KELEI.PM.Entity
{
    [DataClassAttribute(Table = "Base_SystemRole", Database = "SQLServer:ConnectionDb")]
    [ProtoContract]
    public class Base_SystemRole
    {
        public const string SourceTable = "Base_SystemRole";
        public const string DatabaseOption = "SQLServer:ConnectionDb";

        /// <summary>
        /// Role_ID
        /// </summary>
        [ProtoMember(1)]
        [DataProperty(Field = "Role_ID", IsKey = true, BindingFlag = BindingFlagType.Select | BindingFlagType.Where)]
        public int Role_ID
        {
            get;
            set;
        }
        /// <summary>
        /// Role_Name
        /// </summary>
        [ProtoMember(2)]
        [DataProperty(Field = "Role_Name")]
        public string Role_Name
        {
            get;
            set;
        }
        /// <summary>
        /// Role_Detail
        /// </summary>
        [ProtoMember(3)]
        [DataProperty(Field = "Role_Detail")]
        public string Role_Detail
        {
            get;
            set;
        }
        /// <summary>
        /// OrderID
        /// </summary>
        [ProtoMember(4)]
        [DataProperty(Field = "OrderID")]
        public int OrderID
        {
            get;
            set;
        }
        /// <summary>
        /// RoleType
        /// </summary>
        [ProtoMember(5)]
        [DataProperty(Field = "RoleType")]
        public int RoleType
        {
            get;
            set;
        }
        /// <summary>
        /// SubRoleType
        /// </summary>
        [ProtoMember(6)]
        [DataProperty(Field = "SubRoleType")]
        public int SubRoleType
        {
            get;
            set;
        }
        /// <summary>
        /// RoleRangeType
        /// </summary>
        [ProtoMember(7)]
        [DataProperty(Field = "RoleRangeType")]
        public int RoleRangeType
        {
            get;
            set;
        }
    }
}
