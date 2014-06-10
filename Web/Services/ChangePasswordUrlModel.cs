using Web.Models.UrlModels;
using Web.Routing;

namespace Web.Services
{
    public class ChangePasswordUrlModel : UrlModel
    {
        public ChangePasswordUrlModel()
            : base(RouteFormats.ChangePassword)
        {
        }
    }
}