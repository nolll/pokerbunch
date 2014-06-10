using Web.Routing;

namespace Web.Models.UrlModels
{
    public class AddUserConfirmationUrlModel : UrlModel
    {
        public AddUserConfirmationUrlModel()
            : base(RouteFormats.UserAddConfirmation)
        {
        }
    }
}