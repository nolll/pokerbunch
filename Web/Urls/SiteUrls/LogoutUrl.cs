using Web.Routes;

namespace Web.Urls.SiteUrls
{
    public class LogoutUrl : SiteUrl
    {
        public LogoutUrl()
            : base(WebRoutes.Auth.Logout)
        {
        }
    }
}