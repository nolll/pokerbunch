using Web.Routing;

namespace Web.Models.UrlModels
{
    public class RunningCashgameUrl : HomegameUrl
    {
        public RunningCashgameUrl(string slug)
            : base(RouteFormats.RunningCashgame, slug)
        {
        }
    }
}