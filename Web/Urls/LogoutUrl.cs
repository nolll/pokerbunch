using Web.Common.Routes;

namespace Web.Urls
{
    public class LogoutUrl : SiteUrl
    {
        public LogoutUrl()
            : base(WebRoutes.AuthLogout)
        {
        }
    }
}