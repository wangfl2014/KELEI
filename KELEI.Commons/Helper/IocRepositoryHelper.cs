using Autofac;
using System;
using System.Reflection;

namespace KELEI.Commons.Helper
{
    public class IocRepositoryHelper
    {
        private static ContainerBuilder builder;
        private static IContainer container;
        public static void RegisterRepository(string assemblys)
        {

            //var config = new ConfigurationBuilder();
            //config.AddJsonFile(ConfigHelper.GetAppSetting(ConstSet.ServiceRepositoryJson));

            //var module = new ConfigurationModule(config.Build());
            //builder = new ContainerBuilder();
            //builder.RegisterModule(module);
            //container = builder.Build();
            if (!string.IsNullOrEmpty(assemblys))
            {
                var arrAssembly = assemblys.Split(',');
                foreach (var nameItem in arrAssembly)
                {
                    var arrType = GetModels(nameItem);
                    builder = new ContainerBuilder();
                    foreach (Type type in arrType)
                    {
                        if (type.FullName.IndexOf(assemblys) >= 0 && type.BaseType.FullName.IndexOf(assemblys) >= 0)
                        {
                            builder.RegisterType(type).As(type.BaseType);
                        }
                    }
                }
            }

            container = builder.Build();
        }
        public static T GetClass<T>()
        {
            return container.Resolve<T>();
        }

        public static void DeleteRepository()
        {
            if (container != null)
            {
                container.Dispose();
                container = null;
            }
            if (builder != null)
            {
                builder = null;
            }

        }

        private static Type[] GetModels(string modelName)
        {
            Assembly assModel = Assembly.Load(modelName);
            Type[] arrType = assModel.GetTypes();
            return arrType;
        }
    }
}