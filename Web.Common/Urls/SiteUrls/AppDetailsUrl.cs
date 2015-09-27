using Web.Common.Routes;

namespace Web.Common.Urls.SiteUrls
{
    public class AppDetailsUrl : IdUrl
    {
        public AppDetailsUrl(int appId)
            : base(WebRoutes.AppDetails, appId)
        {
        }
    }
}