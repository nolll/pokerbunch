using Core.Urls;

namespace Web.Urls
{
    public class RunningCashgameGameJsonUrl : SlugUrl
    {
        public RunningCashgameGameJsonUrl(string slug)
            : base(Routes.RunningCashgameGameJson, slug)
        {
        }
    }
}