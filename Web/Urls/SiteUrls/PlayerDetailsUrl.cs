namespace Web.Urls.SiteUrls
{
    public class PlayerDetailsUrl : IdUrl
    {
        public PlayerDetailsUrl(string playerId)
            : base(WebRoutes.Player.Details, playerId)
        {
        }
    }
}