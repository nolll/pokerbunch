using Web.Routing;

namespace Web.Models.UrlModels
{
    public class ChangePasswordUrl : Url
    {
        public ChangePasswordUrl()
            : base(RouteFormats.ChangePassword)
        {
        }
    }
}