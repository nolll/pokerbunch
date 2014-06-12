using Web.Routing;

namespace Web.Models.UrlModels
{
    public class LogoutUrl : Url
    {
        public LogoutUrl()
            : base(RouteFormats.AuthLogout)
        {
        }
    }
}