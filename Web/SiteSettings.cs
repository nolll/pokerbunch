using System.Configuration;
using Web.Common;

namespace Web
{
    public class SiteSettings : CommonSettings
    {
        public SiteSettings()
            : base(Get("SiteHost"), Get("ApiHost"))
        {
        }

        public static bool IsInProduction => GetBool("InProduction");

        private static bool GetBool(string key)
        {
            bool ret;
            var str = Get(key);
            return bool.TryParse(str, out ret) ? ret : ret;
        }

        private static string Get(string key)
        {
            return ConfigurationManager.AppSettings.Get(key);
        }
    }
}
