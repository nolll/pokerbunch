using Web.Routing;

namespace Web.Models.UrlModels
{
    public class RunningCashgameUrlModel : HomegameUrlModel
    {
        public RunningCashgameUrlModel(string slug)
            : base(RouteFormats.RunningCashgame, slug)
        {
        }
    }
}