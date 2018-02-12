using KELEI.Commons.Helper;
using KELEI.PM.DBService.Commons;
using System;
using Topshelf;

namespace KELEI.PM.DBService
{
    class Program
    {
        static void Main(string[] args)
        {
            LogHelper.Info("准备启动服务！");
            Console.WriteLine("准备启动服务！");
            HostFactory.Run(x =>
            {
                x.RunAsLocalSystem();
                x.SetDescription(ConfigHelper.GetAppSetting(ConstSet.ServerDescription));
                x.SetDisplayName(ConfigHelper.GetAppSetting(ConstSet.ServerDisplayName));
                x.SetServiceName(ConfigHelper.GetAppSetting(ConstSet.ServerServiceName));

                x.Service<SystemService>(s =>
                {
                    s.ConstructUsing(name => new SystemService());
                    s.WhenStarted(tc => tc.Start());
                    s.WhenStopped(tc => tc.Stop());
                });
            });

        }
    }
}
