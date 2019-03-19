using PokerBunch.Common.Urls.ApiUrls;
using PokerBunch.Common.Urls.SiteUrls;
using Web.Services;
using Web.Settings;

namespace Web.Extensions
{
    public static class UrlExtensions
    {
        public static string Absolute(this SiteUrl url)
        {
            return url.Absolute(SiteSettings.SiteHost);
        }

        public static string Absolute(this ApiUrl url)
        {
            return url.Absolute(SiteSettings.ApiHost);
        }
    }
}