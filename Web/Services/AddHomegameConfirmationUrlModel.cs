using Web.Models.UrlModels;
using Web.Routing;

namespace Web.Services
{
    public class AddHomegameConfirmationUrlModel : UrlModel
    {
        public AddHomegameConfirmationUrlModel()
            : base(RouteFormats.HomegameAddConfirmation)
        {
        }
    }
}