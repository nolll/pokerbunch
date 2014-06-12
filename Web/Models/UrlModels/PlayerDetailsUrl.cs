using Web.Routing;

namespace Web.Models.UrlModels
{
    public class PlayerDetailsUrl : PlayerUrl
    {
        public PlayerDetailsUrl(string slug, int playerId)
            : base(RouteFormats.PlayerDetails, slug, playerId)
        {
        }
    }
}