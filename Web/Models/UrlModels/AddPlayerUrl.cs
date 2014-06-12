using Web.Routing;

namespace Web.Models.UrlModels
{
    public class AddPlayerUrl : HomegameUrl
    {
        public AddPlayerUrl(string slug)
            : base(RouteFormats.PlayerAdd, slug)
        {
        }
    }
}