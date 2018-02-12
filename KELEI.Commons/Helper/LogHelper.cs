using log4net;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace KELEI.Commons.Helper
{
    /// <summary>
    /// 日志辅助类
    /// </summary>
    public class LogHelper
    {
        private static ILog _log;
        private const string lockkey = "0";

        /// <summary>
        /// 错误日志
        /// </summary>
        /// <param name="msg"></param>
        public static void Error(string msg)
        {
            _log = LogManager.GetLogger("logerror");
            Task.Run(() => _log.Error(msg));

        }

        public static void Error(Exception e)
        {
            _log = LogManager.GetLogger("logerror");
            Task.Run(() => _log.Error("error", e));

        }

        public static void Info(string msg)
        {
            _log = LogManager.GetLogger("loginfo");
            Task.Run(() => _log.Info(msg));
        }

        public static void InterfaceInfo(string msg)
        {
            _log = LogManager.GetLogger("logInterfaceInfo");
            Task.Run(() => _log.Info(msg));
        }

        public static void CustomizeInfo(string msg, string fileName = "qlwCustomize")
        {
            lock (lockkey)
            {
                var appender = LogManager.GetRepository().GetAppenders().FirstOrDefault(apd => apd.Name == "LogCustomizeLogger");
                if (appender != null)
                {
                    var appenderModel = appender as log4net.Appender.FileAppender;
                    if (appenderModel != null)
                    {
                        var filePath = appenderModel.File;
                        appenderModel.File = filePath.Replace("{$}", fileName + DateTime.Now.ToString("_yyyyMMdd'.log'"));
                        appenderModel.ActivateOptions();
                        _log = LogManager.GetLogger("customizeLogger");
                        _log.Info(msg);
                        appenderModel.File = filePath;
                        appenderModel.ActivateOptions();
                    }
                    else
                    {
                        Error("CustomizeInfo 日志配置错误！ 未保存的错误日志：" + msg);
                    }
                }
                else
                {
                    Error("CustomizeInfo 日志配置错误！ 未保存的错误日志：" + msg);
                }
            }
        }
    }

}