using Web.Routing;

namespace Web.Models.UrlModels
{
    public class PlayerIndexUrl : HomegameUrl
    {
        public PlayerIndexUrl(string slug)
            : base(RouteFormats.PlayerIndex, slug)
        {
        }
    }
}