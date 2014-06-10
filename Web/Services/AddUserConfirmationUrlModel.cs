using Web.Models.UrlModels;
using Web.Routing;

namespace Web.Services
{
    public class AddUserConfirmationUrlModel : UrlModel
    {
        public AddUserConfirmationUrlModel()
            : base(RouteFormats.UserAddConfirmation)
        {
        }
    }
}