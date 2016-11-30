using Web.Urls;

namespace Web.Extensions
{
    public static class WebUrlExtensions
    {
        public static string GetAbsolute(this Url url)
        {
            return AbsoluteUrl.Create(url, SiteSettings.SiteHost, SiteSettings.ApiHost);
        }
    }
}