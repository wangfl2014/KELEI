using ProtoBuf;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace KELEI.Commons.Helper
{
    /// <summary>
    /// 对象属性的绑定标志，标识出该属性会出现在Insert、Update或Where中，当然也可以指定为None，不出现在SQL中
    /// </summary>
    [Flags]
    public enum BindingFlagType
    {
        /// <summary>
        /// 任何情况下都不出现
        /// </summary>
        None = 0,

        /// <summary>
        /// 表示属性会出现在Insert中
        /// </summary>
        Insert = 1,

        /// <summary>
        /// 表示属性会出现在Update中
        /// </summary>
        Update = 2,

        /// <summary>
        /// 表示属性会出现在Where语句部分
        /// </summary>
        Where = 4,

        /// <summary>
        /// 表示属性会出现在查询的返回字段中
        /// </summary>
        Select = 8,

        /// <summary>
        /// 在碰到枚举类型时，不使用Enum的描述，而是用其整型值
        /// </summary>
        UseEnumValue = 64,

        /// <summary>
        /// 在所有情况下都会出现
        /// </summary>
        All = 255,
    }

    [Flags]
    public enum BindingcloumnType
    {
        /// <summary>
        /// 数据库列
        /// </summary>
        Data = 0,

        /// <summary>
        /// 扩展字段，一般为本表基数，如金额列，金额=单价*数量
        /// </summary>
        Extended = 1,

        /// <summary>
        /// 扩展数据，一般为关联表字段
        /// </summary>
        ExteData = 2,

        /// <summary>
        /// 关联实体，关联表实体
        /// </summary>
        ExteModel = 4,

        /// <summary>
        /// 关联实体列表，子主表情况
        /// </summary>
        ExteModelList = 5,
    }

    /// <summary>
    /// 表述数据表
    /// </summary>
    [ProtoContract]
    [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public class DataClassAttribute : System.Attribute
    {
        [ProtoMember(1)]
        public string Table { get; set; }
        [ProtoMember(2)]
        public string Database{ get; set; }
    }

    /// <summary>
    /// 描述数据对象的属性
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    public class DataPropertyAttribute : System.Attribute
    {
        private string _Field = string.Empty;
        private bool _IsKey = false;
        private BindingFlagType _BindingFlag = BindingFlagType.All;
        private BindingcloumnType _CloumnType = BindingcloumnType.Data;
        private string _SourceModel = string.Empty;//关联实体，多表查询时使用
        private string _MappingKeys = string.Empty;//关联条件
        private string _DefaultExpression = string.Empty;//数据库缺少值，如果值为空时数据库缺省值（常量，或数据库函数）
        private string _ColumCode = string.Empty;
        private bool _IsLazy = false;

        public DataPropertyAttribute()
        {
        }

        public DataPropertyAttribute(string strField)
        {
            _Field = strField;
        }

        /// <summary>
        /// 对应的数据库的对象名称
        /// </summary>
        public string Field
        {
            get
            {
                return _Field;
            }
            set
            {
                _Field = value;
            }
        }

        /// <summary>
        /// 关联表数据实体
        /// </summary>
        public string SourceModel
        {
            get
            {
                return _SourceModel;
            }
            set
            {
                _SourceModel = value;
            }
        }

        /// <summary>
        /// 关联表，关联关系
        /// </summary>
        public string MappingKeys
        {
            get
            {
                return _MappingKeys;
            }
            set
            {
                _MappingKeys = value;
            }
        }

        /// <summary>
        /// 如果该值为空，对应的数据库的缺省表达式。注意，如果该值为字符串，<b>不要忘记在前后加上引号</b>
        /// </summary>
        public string DefaultExpression
        {
            get
            {
                return _DefaultExpression;
            }
            set
            {
                _DefaultExpression = value;
            }
        }

        /// <summary>
        /// 该字段是否为关键字缺省是false
        /// </summary>
        public bool IsKey
        {
            get
            {
                return _IsKey;
            }
            set
            {
                _IsKey = value;
            }
        }
        /// <summary>
        /// 对象属性的绑定标志，标识出该属性会出现在Insert、Update或Where中，当然也可以指定为None，不出现在SQL中
        /// </summary>
        public BindingFlagType BindingFlag
        {
            get
            {
                return _BindingFlag;
            }
            set
            {
                _BindingFlag = value;
            }
        }

        /// <summary>
        /// 绑定列类型，本表中读取字段为data（数据列），Extended（扩展列，计算列），ExteData（其他表关联字段），ExteModel（关联实体），ExteModelList（关联实体列表）
        /// </summary>
        public BindingcloumnType CloumnType
        {
            get
            {
                return _CloumnType;
            }
            set
            {
                _CloumnType = value;
            }
        }

        public string ColumCode
        {
            get
            {
                return _ColumCode;
            }
            set
            {
                _ColumCode = value;
            }
        }

        /// <summary>
        /// 查询时不加载扩展类型，默认false，true不加载
        /// </summary>
        public bool IsLazy
        {
            get
            {
                return _IsLazy;
            }
            set
            {
                _IsLazy = value;
            }
        }
    }

    /// <summary>
    /// 数据保存前校验及null值时的默认值
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = true)]
    public class DataSaveCheckAttribute : System.Attribute
    {

    }

    /// <summary>
    /// 数据实体默认值配置
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    public class DataDefaultAttribute : System.Attribute
    {
        private string _InitDefault = string.Empty;
        private string _NullDefault = string.Empty;

        public DataDefaultAttribute()
        {
        }

        public DataDefaultAttribute(string initDefault)
        {
            _InitDefault = initDefault;
        }

        public DataDefaultAttribute(string initDefault, string nullDefault) : this(initDefault)
        {
            _NullDefault = nullDefault;
        }

        /// <summary>
        /// 新建对象时的默认值
        /// </summary>
        public string InitDefault
        {
            get
            {
                return _InitDefault;
            }
            set
            {
                _InitDefault = value;
            }
        }

        /// <summary>
        /// 保存时如果为空，替换的默认值
        /// </summary>
        public string NullDefault
        {
            get
            {
                return _NullDefault;
            }
            set
            {
                _NullDefault = value;
            }
        }
    }

    public class DataPropertyHandle
    {
        /// <summary>
        /// 得到一个对象的所有DataProperty描述
        /// </summary>
        /// <param name="oData">对象</param>
        /// <param name="bindingFlag">所关心的Binding方式</param>
        /// <param name="ignorePropertis">所忽略的属性</param>
        /// <returns>model名称，表名，链接数据库字符串，DataProperty的集合</returns>
        public static Tuple<string, string, string, List<DataPropertyAttribute>> GetTypeProperties(Type oData, BindingFlagType bindingFlag, params string[] ignorePropertis)
        {
            string modelName = oData.Name;
            //返回表名、数据库连接串名称
            DataClassAttribute dca = (DataClassAttribute)Attribute.GetCustomAttribute(oData, typeof(DataClassAttribute));
            if (dca == null || string.IsNullOrEmpty(dca.Table))
            {
                LogHelper.Info($"{modelName}未定义实体对应表与数据库信息");
                //DmcExceptionTools.TrueThrow(true, $"该对象{modelName}没有定义相关的表与数据库！");
                return null;
            }
            string tableName = dca.Table;
            string dbConnection = dca.Database;

      
            PropertyInfo[] piArray = oData.GetProperties(BindingFlags.Public | BindingFlags.IgnoreCase | BindingFlags.Instance);
            if(piArray==null || piArray.Length<=0)
            {
                LogHelper.Info($"{modelName}没有Public的属性，不能构造出数据字段信息");
                DmcExceptionTools.TrueThrow(true, $"该对象{modelName}没有Public的属性，不能构造出数据字段信息！");
                return null;
            }
            //返回熟悉特性
            List<DataPropertyAttribute> list = new List<DataPropertyAttribute>();
            for (int i = 0; i < piArray.Length; i++)
            {
                PropertyInfo pi = piArray[i];

                if (IsStringInArray(pi.Name, ignorePropertis) == false && pi.CanRead)
                {
                    DataPropertyAttribute dpa = (DataPropertyAttribute)Attribute.GetCustomAttribute(pi, typeof(DataPropertyAttribute));

                    if (dpa == null)
                        continue;

                    if ((dpa.BindingFlag & bindingFlag) != 0)
                    {
                        if (string.IsNullOrEmpty(dpa.Field))
                            continue;
                        dpa.ColumCode = pi.Name;
                        list.Add(dpa);
                    }
                }
            }
            return new Tuple<string, string, string, List<DataPropertyAttribute>>(modelName, tableName, dbConnection, list);
        }

        public static dynamic GetObjectValue(object oData, string columName)
        {
            PropertyInfo[] piArray = oData.GetType().GetProperties(BindingFlags.Public | BindingFlags.IgnoreCase | BindingFlags.Instance);
            PropertyInfo pi = piArray.Where(p => p.Name.Equals(columName)).FirstOrDefault();
            var value = pi.GetValue(oData, null);
            return value;
        }

        public static dynamic SetObjectValue(object oData, string columName, dynamic value)
        {
            PropertyInfo[] piArray = oData.GetType().GetProperties(BindingFlags.Public | BindingFlags.IgnoreCase | BindingFlags.Instance);
            PropertyInfo property = piArray.Where(p => p.Name.Equals(columName)).FirstOrDefault();
            if (!property.PropertyType.IsGenericType)
            {
                //非泛型
                property.SetValue(oData, Convert.ChangeType(value, property.PropertyType), null);
            }
            else
            {
                //泛型Nullable<>
                Type genericTypeDefinition = property.PropertyType.GetGenericTypeDefinition();
                if (genericTypeDefinition == typeof(Nullable<>))
                {
                    property.SetValue(oData, Convert.ChangeType(value, Nullable.GetUnderlyingType(property.PropertyType)), null);
                }
            }
            return value;
        }

        public static void SetObjectListValue(object oData, string columName, List<dynamic> value, Type type)
        {
            PropertyInfo[] piArray = oData.GetType().GetProperties(BindingFlags.Public | BindingFlags.IgnoreCase | BindingFlags.Instance);
            PropertyInfo property = piArray.Where(p => p.Name.Equals(columName)).FirstOrDefault();
            if (!property.PropertyType.IsGenericType)
            {
                //非泛型
                property.SetValue(oData, Convert.ChangeType(value, property.PropertyType), null);
            }
            else
            {
                //泛型Nullable<>
                Type genericTypeDefinition = property.PropertyType.GetGenericTypeDefinition();
                if (genericTypeDefinition == typeof(Nullable<>))
                {
                    property.SetValue(oData, Convert.ChangeType(value, Nullable.GetUnderlyingType(property.PropertyType)), null);
                }
                else if (genericTypeDefinition == typeof(List<>))
                {
                    Type listType = typeof(List<>);
                    Type specificListType = listType.MakeGenericType(type);
                    IList listValue = (IList)Activator.CreateInstance(specificListType);
                    foreach (var objItem in value)
                    {
                        listValue.Add(objItem);
                    }
                    property.SetValue(oData, listValue, null);
                }
            }
        }

        private IList MakeListOfType(Type listType)
        {
            Type type = typeof(List<>);
            Type specificListType = type.MakeGenericType(listType);

            return (IList)Activator.CreateInstance(specificListType);
        }
        public static Dictionary<string, dynamic> GetObjectKeyValue(object oData, bool dataflg = false)
        {
            Dictionary<string, dynamic> dic = new Dictionary<string, dynamic>();
            PropertyInfo[] piArray = oData.GetType().GetProperties(BindingFlags.Public | BindingFlags.IgnoreCase | BindingFlags.Instance);
            if (piArray == null || piArray.Count() <= 0) return null;
            foreach (var pi in piArray)
            {
                if (!string.IsNullOrEmpty(pi.Name))
                {
                    if (dataflg)
                    {
                        var att = pi.GetCustomAttributes(typeof(DataPropertyAttribute), false);
                        if (att != null && att[0] != null)
                        {
                            if (((DataPropertyAttribute)att[0]).CloumnType == (int)BindingcloumnType.Data)
                            {
                                dic.Add(pi.Name, pi.GetValue(oData, null));
                            }
                        }
                    }
                    else
                    {
                        dic.Add(pi.Name, pi.GetValue(oData, null));
                    }
                }
            }
            return dic;
        }

        private static bool IsStringInArray(string strValue, string[] strArray)
        {
            bool bResult = false;
            if (strArray == null) return bResult;
            for (int i = 0; i < strArray.Length; i++)
            {
                if (string.Compare(strValue, strArray[i], true) == 0)
                {
                    bResult = true;
                }
            }

            return bResult;
        }

        public static Tuple<List<string>, object> GetQueryCriteria(Type oData, Dictionary<string, dynamic> dic)
        {
            PropertyInfo[] piArray = oData.GetProperties(BindingFlags.Public | BindingFlags.IgnoreCase | BindingFlags.Instance);
            List<string> dicKey = new List<string>();
            List<object> dicValue = new List<object>();
            foreach (var item in dic)
            {
                dicKey.Add(item.Key);

                var pi = piArray.Where(p => p.Name == item.Key).FirstOrDefault();
                pi.SetValue(oData, item.Value);
                dicValue.Add(pi);
            }
            return new Tuple<List<string>, object>(dicKey, dicValue.ToArray());
        }
    }
    /// <summary>
    /// 包装了一个对象的属性描述信息
    /// </summary>
    public struct PropertyAttribute
    {
        public DataPropertyAttribute Attribute;
        public PropertyInfo PropertyInfo;
    }
}
