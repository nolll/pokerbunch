using System;
using System.Security.Cryptography;
using System.Text;
using Web.Settings;

namespace Web.Services
{
    public static class VueConfigScriptService
    {
        private static string Script(AppSettings appSettings)
        {
            var apiUrl = appSettings.Urls.ApiUri.AbsoluteUri.TrimEnd('/');
            return $"window.vueConfig = {{ apiUrl: '{apiUrl}' }};";
        }

        public static string ScriptTag(AppSettings appSettings)
        {
            var script = Script(appSettings);
            return $"<script>{script}</script>";
        }

        public static string ComputedSha256Hash(AppSettings appSettings)
        {
            var script = Script(appSettings);
            return ComputeHash(script);
        }

        private static string ComputeHash(string s)
        {
            using var sha256Hash = SHA256.Create();
            return Convert.ToBase64String(sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(s)));
        }
    }
}