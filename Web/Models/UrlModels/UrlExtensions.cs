using System.Web.Configuration;
using Application.Urls;

namespace Web.Models.UrlModels
{
    public static class UrlExtensions
    {
        public static string Absolute(this Url url)
        {
            var siteUrl = WebConfigurationManager.AppSettings.Get("SiteUrl");
            return string.Concat(siteUrl, url.Relative);
        }
    }
}