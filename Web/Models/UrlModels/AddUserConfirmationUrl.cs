using Web.Routing;

namespace Web.Models.UrlModels
{
    public class AddUserConfirmationUrl : Url
    {
        public AddUserConfirmationUrl()
            : base(RouteFormats.UserAddConfirmation)
        {
        }
    }
}