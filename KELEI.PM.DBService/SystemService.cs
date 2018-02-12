using KELEI.Commons.AccessRPC;
using KELEI.Commons.Helper;
using KELEI.PM.DBService.Commons;
using KELEI.PM.DBService.DBAccess;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KELEI.PM.DBService
{
    public class SystemService
    {
        public static List<NetMqWork> works=new List<NetMqWork>();
        public void Start()
        {
            try
            {
                //日志配置
                XmlConfigurator.ConfigureAndWatch(new FileInfo(FileHelper.ApplicationPath("log4config.config")));

                Console.WriteLine("启动数据库服务开始");
                //根据表确定数据库类型，因此不需要做抽象
                //IocRepositoryHelper.RegisterRepository(ConfigHelper.GetAppSetting(ConstSet.SystemRepositoryAssembly)); //数据库访问Ioc依赖注入
                ModelManager.Init();//数据库访问的实体类初始化，生成sql语句
                string objXmlPath = ConfigHelper.GetAppSetting(ConstSet.NetMQListenerObjectXML) ?? "Configs/ServiceListenerObject.xml";
                string objXmlNodeName = ConfigHelper.GetAppSetting(ConstSet.NetMQListenerObjectNodeName) ?? "/objects/object";
                ServiceListenerObject.Init(objXmlPath, objXmlNodeName);//服务器端提供RPC服务初始化
                Console.WriteLine("启动数据库服务完成");

                Console.WriteLine("启动网络服务开始");
                string ipAddress = ConfigHelper.GetAppSetting(ConstSet.NetMQIPAddress);
                string[] arrIP = ipAddress.Split('|');
                foreach (string ipItem in arrIP)
                {
                    var ip = ipItem.Split(',');
                    NetMqWork work = new NetMqWork(ip[0], ip[1]);
                    work.Read();
                    works.Add(work);
                }
                Console.WriteLine("启动网络服务完成");

                Console.WriteLine("程序已启动,按任意键退出");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                LogHelper.Error("启动服务错误：" + ex.ToString());
            }
        }

        public void Stop()
        {
            LogHelper.Info("停止服务！");
            if (works != null)
            {
                foreach (NetMqWork work in works)
                {
                    if (work != null)
                    {
                        work.Dispose();
                    }
                }
                works.Clear();
            }
            ModelManager.Delete();
            IocRepositoryHelper.DeleteRepository();
            ServiceListenerObject.DelectDic();
            DBConnectObject.Dispose();
        }
    }
}
