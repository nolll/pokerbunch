using Web.Routes;

namespace Web.Urls.SiteUrls
{
    public class LoginUrl : SiteUrl
    {
        public LoginUrl()
            : base(WebRoutes.Auth.Login)
        {
        }
    }
}