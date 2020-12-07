using System;
using System.Security.Cryptography;
using System.Text;

namespace Web.Services
{
    public static class GaScriptService
    {
        private const string Code = "UA-8453410-7";
        private static string Script => $"window.ga=window.ga||function(){{(ga.q=ga.q||[]).push(arguments)}};ga.l=+new Date;ga('create', '{Code}', 'auto');ga('send', 'pageview');";
        public static string ScriptTag => $"<script>{Script}</script>";
        public static string ComputedSha256Hash => ComputeHash(Script);

        private static string ComputeHash(string s)
        {
            using var sha256Hash = SHA256.Create();
            return Convert.ToBase64String(sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(s)));
        }
    }
}