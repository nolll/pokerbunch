using System;

namespace Core.Settings
{
    public static class SettingsConverter
    {
        public static bool ToBool(string str)
        {
            return bool.TryParse(str, out var ret) && ret;
        }

        public static Uri ToUri(string str)
        {
            if (str == null)
                return null;

            var urlWithScheme = str.StartsWith("http") ? str : $"https://{str}";
            return new Uri(urlWithScheme);
        }
    }
}