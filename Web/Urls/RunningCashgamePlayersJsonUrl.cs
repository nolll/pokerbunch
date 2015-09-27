using Web.Common.Routes;

namespace Web.Urls
{
    public class RunningCashgamePlayersJsonUrl : SlugUrl
    {
        public RunningCashgamePlayersJsonUrl(string slug)
            : base(WebRoutes.RunningCashgamePlayersJson, slug)
        {
        }
    }
}