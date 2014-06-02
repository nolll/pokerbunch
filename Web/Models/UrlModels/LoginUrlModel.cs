using Web.Routing;

namespace Web.Models.UrlModels
{
    public class LoginUrlModel : UrlModel
    {
        public LoginUrlModel()
            : base(RouteFormats.AuthLogin)
        {
        }
    }
}