using Web.Routes;

namespace Web.Urls.SiteUrls
{
    public class AppDetailsUrl : IdUrl
    {
        public AppDetailsUrl(string appId)
            : base(WebRoutes.App.Details, appId)
        {
        }
    }
}