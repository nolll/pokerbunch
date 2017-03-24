using Web.Routes;

namespace Web.Urls.SiteUrls
{
    public class AllAppsUrl : SiteUrl
    {
        protected override string Input => WebRoutes.App.All;
    }
}