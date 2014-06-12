using Web.Routing;

namespace Web.Models.UrlModels
{
    public class LoginUrl : Url
    {
        public LoginUrl()
            : base(RouteFormats.AuthLogin)
        {
        }
    }
}