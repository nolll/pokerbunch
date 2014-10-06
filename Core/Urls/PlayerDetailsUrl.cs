namespace Core.Urls
{
    public class PlayerDetailsUrl : PlayerUrl
    {
        public PlayerDetailsUrl(string slug, int playerId)
            : base(RouteFormats.PlayerDetails, slug, playerId)
        {
        }
    }
}