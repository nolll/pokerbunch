using Web.Common.Routes;

namespace Web.Common.Urls.SiteUrls
{
    public class AppDetailsUrl : IdUrl
    {
        public AppDetailsUrl(string appId)
            : base(WebRoutes.App.Details, appId)
        {
        }
    }
}