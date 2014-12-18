namespace Core.Urls
{
    public class RunningCashgamePlayersJsonUrl : BunchUrl
    {
        public RunningCashgamePlayersJsonUrl(string slug)
            : base(RouteFormats.RunningCashgamePlayersJson, slug)
        {
        }
    }
}