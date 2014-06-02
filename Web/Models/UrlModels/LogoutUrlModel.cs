using Web.Routing;

namespace Web.Models.UrlModels
{
    public class LogoutUrlModel : UrlModel
    {
        public LogoutUrlModel()
            : base(RouteFormats.AuthLogout)
        {
        }
    }
}