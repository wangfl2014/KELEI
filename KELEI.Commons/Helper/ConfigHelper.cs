namespace KELEI.Commons.Helper
{
    public static class ConfigHelper
    {
        // <summary>
        /// 获取配置信息
        /// </summary>
        /// <param name="key">配置键值</param>
        /// <returns></returns>
        public static string GetAppSetting(string key)
        {
            //优先尝试从Weg.config取数据
            if (System.Web.Configuration.WebConfigurationManager.AppSettings != null &&
                System.Web.Configuration.WebConfigurationManager.AppSettings[key] != null)
            {
                return System.Web.Configuration.WebConfigurationManager.AppSettings[key];
            }
            //然后尝试从App.config取数据
            else if (System.Configuration.ConfigurationManager.AppSettings != null &&
                System.Configuration.ConfigurationManager.AppSettings[key] != null)
            {
                return System.Configuration.ConfigurationManager.AppSettings[key];
            }
            else
            {
                return string.Empty;
            }
        }
    }
}