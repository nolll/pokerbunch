using System.Configuration;

namespace Web.Settings
{
    public static class SettingsReader
    {
        public static bool GetBool(string key)
        {
            var str = Get(key);
            return bool.TryParse(str, out var ret) && ret;
        }

        public static string Get(string key)
        {
            return ConfigurationManager.AppSettings.Get(key);
        }
    }
}