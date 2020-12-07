using System;
using System.Security.Cryptography;
using System.Text;

namespace Web.Services
{
    public static class FontStyleService
    {
        private static string Css => @"
@font-face {
    font-family: 'FontAwesome';
    src: url('/dist/fonts/fontawesome-webfont.eot?v=3.0.1');
    src: url('/dist/fonts/fontawesome-webfont.eot?#iefix&v=3.0.1') format('embedded-opentype'), url('/dist/fonts/fontawesome-webfont.woff?v=3.0.1') format('woff'), url('/dist/fonts/fontawesome-webfont.ttf?v=3.0.1') format('truetype');
    font-weight: normal;
    font-style: normal;
}".Trim().Replace("\r\n", " ");

        public static string StyleTag => $"<style type=\"text/css\">{Css}</style>";
        public static string ComputedSha256Hash => ComputeHash(Css);

        private static string ComputeHash(string s)
        {
            using var sha256Hash = SHA256.Create();
            return Convert.ToBase64String(sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(s)));
        }
    }
}