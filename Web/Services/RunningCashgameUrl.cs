using Web.Models.UrlModels;
using Web.Routing;

namespace Web.Services
{
    public class RunningCashgameUrl : HomegameUrlModel
    {
        public RunningCashgameUrl(string slug)
            : base(RouteFormats.RunningCashgame, slug)
        {
        }
    }
}