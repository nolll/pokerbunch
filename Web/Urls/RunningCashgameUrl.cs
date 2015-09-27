using Web.Common.Routes;

namespace Web.Urls
{
    public class RunningCashgameUrl : SlugUrl
    {
        public RunningCashgameUrl(string slug)
            : base(WebRoutes.RunningCashgame, slug)
        {
        }
    }
}