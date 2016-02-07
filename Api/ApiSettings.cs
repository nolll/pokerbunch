using System.Configuration;
using Web.Common;

namespace Api
{
    public class ApiSettings : CommonSettings
    {
        public ApiSettings()
            : base(Get("SiteHost"), Get("ApiHost"))
        {
        }

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
