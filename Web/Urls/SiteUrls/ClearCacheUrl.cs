using Web.Routes;

namespace Web.Urls.SiteUrls
{
    public class ClearCacheUrl : SiteUrl
    {
        protected override string Input => WebRoutes.Admin.ClearCache;
    }
}