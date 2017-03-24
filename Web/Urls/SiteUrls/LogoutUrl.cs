using Web.Routes;

namespace Web.Urls.SiteUrls
{
    public class LogoutUrl : SiteUrl
    {
        protected override string Input => WebRoutes.Auth.Logout;
    }
}