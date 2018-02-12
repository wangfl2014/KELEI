using KELEI.Commons.Helper;
using KELEI.PM.DBService.Commons;
using System;
using System.Reflection;

namespace KELEI.PM.DBService.DBAccess
{
    public static class ModelManager
    {
        /// <summary>
        /// 将model生产sql语句，放入内存中
        /// </summary>
        /// <param name="modelName"></param>
        public static void Init()
        {
            string modelName = ConfigHelper.GetAppSetting(ConstSet.SystemModelAssembly) ?? "";
            if (!string.IsNullOrEmpty(modelName))
            {
                var arrAssembly = modelName.Split(',');
                foreach (var nameItem in arrAssembly)
                {
                    Type[] arrType = GetModels(nameItem);//返回程序集下的所有类
                    foreach (Type type in arrType)
                    {
                        ModelAnalysisToSQL<Type>.GetModelProperty(type);
                    }
                }
            }

        }

        public static void Delete()
        {
            ModelDictionaries.DeleteDicAll();
            ModelDictionaries.DeleteDicModelAnalysisAll();
        }
        /// <summary>
        /// 返回指定程序集下所有的类（实体类）
        /// </summary>
        /// <param name="modelName">程序集名称</param>
        /// <returns></returns>
        private static Type[] GetModels(string modelName)
        {
            Assembly assModel = Assembly.Load(modelName);
            Type[] arrType = assModel.GetTypes();
            return arrType;
        }

    }
}
