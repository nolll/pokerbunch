using System;
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

        public static Uri GetUri(string key)
        {
            var str = Get(key);
            if (str == null)
                return null;

            var urlWithScheme = str.StartsWith("http") ? str : $"https://{str}";
            return new Uri(urlWithScheme);
        }

        public static string Get(string key)
        {
            return ConfigurationManager.AppSettings.Get(key);
        }
    }
}