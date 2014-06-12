using Web.Routing;

namespace Web.Models.UrlModels
{
    public class ChangePasswordConfirmationUrl : Url
    {
        public ChangePasswordConfirmationUrl()
            : base(RouteFormats.ChangePasswordConfirmation)
        {
        }
    }
}