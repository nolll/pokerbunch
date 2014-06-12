using Web.Routing;

namespace Web.Models.UrlModels
{
    public class ForgotPasswordConfirmationUrl : Url
    {
        public ForgotPasswordConfirmationUrl()
            : base(RouteFormats.ForgotPasswordConfirmation)
        {
        }
    }
}