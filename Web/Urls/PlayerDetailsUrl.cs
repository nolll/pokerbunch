namespace Web.Urls
{
    public class PlayerDetailsUrl : IdUrl
    {
        public PlayerDetailsUrl(int playerId)
            : base(Routes.PlayerDetails, playerId)
        {
        }
    }

    public class AppDetailsUrl : IdUrl
    {
        public AppDetailsUrl(int appId)
            : base(Routes.AppDetails, appId)
        {
        }
    }
}