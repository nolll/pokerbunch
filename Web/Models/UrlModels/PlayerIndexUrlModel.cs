using Web.Routing;

namespace Web.Models.UrlModels
{
    public class PlayerIndexUrlModel : HomegameUrlModel
    {
        public PlayerIndexUrlModel(string slug)
            : base(RouteFormats.PlayerIndex, slug)
        {
        }
    }
}