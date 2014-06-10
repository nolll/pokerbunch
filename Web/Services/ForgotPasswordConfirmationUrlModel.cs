using Web.Models.UrlModels;
using Web.Routing;

namespace Web.Services
{
    public class ForgotPasswordConfirmationUrlModel : UrlModel
    {
        public ForgotPasswordConfirmationUrlModel()
            : base(RouteFormats.ForgotPasswordConfirmation)
        {
        }
    }
}