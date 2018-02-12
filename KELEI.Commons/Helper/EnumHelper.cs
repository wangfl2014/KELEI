using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace KELEI.Commons.Helper
{
    public static class EnumHelper
    {
        /// <summary>
        /// 将枚举转化为数据源（数据源为枚举值与name）
        /// </summary>
        /// <param name="tEnum"></param>
        /// <returns></returns>
        public static Dictionary<int, string> EnumToDataSource(System.Type tEnum)
        {
            Array values = System.Enum.GetValues(tEnum);
            Dictionary<int, string> list = new Dictionary<int, string>(values.Length);
            foreach (var i in values)
            {
                list.Add((int)i, System.Enum.GetName(tEnum, i));
            }

            return list;
        }

        /// <summary>
        /// 将枚举返回成一个字典，int枚举值 string枚举描述或名称
        /// </summary>
        /// <param name="tEnum"></param>
        /// <returns></returns>
        public static Dictionary<int, string> EnumDescriptionsByte(System.Type tEnum)
        {

            Dictionary<int, string> dic = new Dictionary<int, string>();
            foreach (int myCode in Enum.GetValues(tEnum))
            {
                string name = System.Enum.GetName(tEnum, myCode);//获取名称
                string descr = string.Empty;

                FieldInfo fieldInfo = tEnum.GetField(name);
                DescriptionAttribute[] attributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (attributes.Length > 0)
                {
                    descr = attributes[0].Description;
                }
                else
                {
                    descr = name.ToString();
                }

                dic.Add(myCode, descr);
            }
            return dic;
        }

        /// <summary>
        /// 返回id, code,name列表，name为描述
        /// </summary>
        /// <param name="tEnum"></param>
        /// <returns></returns>
        public static List<EnumInfo> EnumToDataList(System.Type tEnum)
        {
            Array values = System.Enum.GetValues(tEnum);
            List<EnumInfo> enumList = new List<EnumInfo>();
            foreach (int i in values)
            {
                string nameDesc = EnumDescriptionsByte(tEnum, i);
                enumList.Add(new EnumInfo()
                {
                    id = i,
                    code = System.Enum.GetName(tEnum, i),
                    name = nameDesc
                });
            }

            return enumList;
        }
        /// <summary>
        /// 获取枚举类型值的描述
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string EnumDescription(System.Enum value)
        {
            string result = string.Empty;

            FieldInfo fieldInfo = value.GetType().GetField(value.ToString());
            if (fieldInfo != null)
            {
                DescriptionAttribute[] attributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attributes.Length > 0)
                {
                    result = attributes[0].Description;
                }
            }

            return result;
        }

        /// <summary>
        /// 获取一个枚举类型，指定int值的描述或名称
        /// </summary>
        /// <param name="tEnum"></param>
        /// <returns></returns>
        public static string EnumDescriptionsByte(System.Type tEnum, int value)
        {
            string descr = string.Empty;
            foreach (int myCode in Enum.GetValues(tEnum))
            {
                if (myCode == value)
                {
                    string name = System.Enum.GetName(tEnum, myCode);//获取名称
                    FieldInfo fieldInfo = tEnum.GetField(name);
                    DescriptionAttribute[] attributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
                    if (attributes.Length > 0)
                    {
                        descr = attributes[0].Description;
                    }
                    else
                    {
                        descr = name.ToString();
                    }
                }
            }
            return descr;
        }

        /// <summary>
        /// 获取一个枚举值，根据名称返回值
        /// </summary>
        /// <param name="tEnum"></param>
        /// <param name="coed"></param>
        /// <returns></returns>
        public static int EnumDescriptionsValue(System.Type tEnum, string coed)
        {
            int val = -1;
            foreach (int myCode in Enum.GetValues(tEnum))
            {
                string name = System.Enum.GetName(tEnum, myCode);//获取名称
                if (coed == name)
                {
                    val = myCode;
                    break;
                }
            }
            return val;
        }

        public static T GetEnumValueByCode<T>(System.Type tEnum, string coed)
        {
            var enumObj = Enum.Parse(tEnum, coed);
            return (T)enumObj;
        }
    }

    public class EnumInfo
    {
        public int id { get; set; }
        public string code { get; set; }
        public string name { get; set; }
    }
}