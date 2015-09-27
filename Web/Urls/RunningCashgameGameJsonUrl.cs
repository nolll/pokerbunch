using Web.Common.Routes;

namespace Web.Urls
{
    public class RunningCashgameGameJsonUrl : SlugUrl
    {
        public RunningCashgameGameJsonUrl(string slug)
            : base(WebRoutes.RunningCashgameGameJson, slug)
        {
        }
    }
}