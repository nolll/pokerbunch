namespace Application.Urls
{
    public class RunningCashgameUrl : HomegameUrl
    {
        public RunningCashgameUrl(string slug)
            : base(RouteFormats.RunningCashgame, slug)
        {
        }
    }
}