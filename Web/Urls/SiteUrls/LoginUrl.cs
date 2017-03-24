using Web.Routes;

namespace Web.Urls.SiteUrls
{
    public class LoginUrl : SiteUrl
    {
        protected override string Input => WebRoutes.Auth.Login;
    }
}