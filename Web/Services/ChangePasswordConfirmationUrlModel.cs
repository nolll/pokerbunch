using Web.Models.UrlModels;
using Web.Routing;

namespace Web.Services
{
    public class ChangePasswordConfirmationUrlModel : UrlModel
    {
        public ChangePasswordConfirmationUrlModel()
            : base(RouteFormats.ChangePasswordConfirmation)
        {
        }
    }
}