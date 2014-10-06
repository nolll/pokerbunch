namespace Core.Urls
{
    public class RunningCashgameUrl : BunchUrl
    {
        public RunningCashgameUrl(string slug)
            : base(RouteFormats.RunningCashgame, slug)
        {
        }
    }
}