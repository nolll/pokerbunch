using Web.Routing;

namespace Web.Models.UrlModels
{
    public class AddPlayerConfirmationUrlModel : HomegameUrlModel
    {
        public AddPlayerConfirmationUrlModel(string slug)
            : base(RouteFormats.PlayerAddConfirmation, slug)
        {
        }
    }
}