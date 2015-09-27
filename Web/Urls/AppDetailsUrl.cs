using Web.Common.Routes;

namespace Web.Urls
{
    public class AppDetailsUrl : IdUrl
    {
        public AppDetailsUrl(int appId)
            : base(WebRoutes.AppDetails, appId)
        {
        }
    }
}