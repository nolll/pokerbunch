using Web.Models.UrlModels;
using Web.Routing;

namespace Web.Services
{
    public class AddPlayerConfirmationUrlModel : HomegameUrlModel
    {
        public AddPlayerConfirmationUrlModel(string slug)
            : base(RouteFormats.PlayerAddConfirmation, slug)
        {
        }
    }
}