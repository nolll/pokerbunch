using Core.Urls;

namespace Web.Urls
{
    public class PlayerDetailsUrl : IdUrl
    {
        public PlayerDetailsUrl(int playerId)
            : base(Routes.PlayerDetails, playerId)
        {
        }
    }
}