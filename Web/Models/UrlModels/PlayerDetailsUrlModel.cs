using Web.Routing;

namespace Web.Models.UrlModels
{
    public class PlayerDetailsUrlModel : PlayerUrlModel
    {
        public PlayerDetailsUrlModel(string slug, int playerId)
            : base(RouteFormats.PlayerDetails, slug, playerId)
        {
        }
    }
}