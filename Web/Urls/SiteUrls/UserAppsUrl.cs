using Web.Routes;

namespace Web.Urls.SiteUrls
{
    public class UserAppsUrl : SiteUrl
    {
        protected override string Input => WebRoutes.App.List;
    }
}