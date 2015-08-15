using Core.Urls;

namespace Web.Urls
{
    public class PlayerDetailsUrl : PlayerUrl
    {
        public PlayerDetailsUrl(int playerId)
            : base(Routes.PlayerDetails, playerId)
        {
        }
    }
}