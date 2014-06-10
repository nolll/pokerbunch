using Web.Routing;

namespace Web.Models.UrlModels
{
    public class RunningCashgameUrl : HomegameUrlModel
    {
        public RunningCashgameUrl(string slug)
            : base(RouteFormats.RunningCashgame, slug)
        {
        }
    }
}