using Web.Common.Routes;

namespace Web.Urls
{
    public class LoginUrl : SiteUrl
    {
        public LoginUrl()
            : base(WebRoutes.AuthLogin)
        {
        }
    }
}