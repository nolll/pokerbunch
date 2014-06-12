using Web.Routing;

namespace Web.Models.UrlModels
{
    public class AddPlayerConfirmationUrl : HomegameUrl
    {
        public AddPlayerConfirmationUrl(string slug)
            : base(RouteFormats.PlayerAddConfirmation, slug)
        {
        }
    }
}