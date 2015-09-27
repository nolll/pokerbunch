using Web.Common.Routes;

namespace Web.Urls
{
    public class PlayerDetailsUrl : IdUrl
    {
        public PlayerDetailsUrl(int playerId)
            : base(WebRoutes.PlayerDetails, playerId)
        {
        }
    }
}