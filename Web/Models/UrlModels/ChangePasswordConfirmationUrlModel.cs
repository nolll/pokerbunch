using Web.Routing;

namespace Web.Models.UrlModels
{
    public class ChangePasswordConfirmationUrlModel : UrlModel
    {
        public ChangePasswordConfirmationUrlModel()
            : base(RouteFormats.ChangePasswordConfirmation)
        {
        }
    }
}