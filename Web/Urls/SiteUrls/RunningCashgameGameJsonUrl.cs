using Web.Routes;

namespace Web.Urls.SiteUrls
{
    public class RunningCashgameGameJsonUrl : SiteUrl
    {
        private readonly string _slug;

        public RunningCashgameGameJsonUrl(string slug)
        {
            _slug = slug;
        }

        protected override string Input => RouteParams.Replace(WebRoutes.Cashgame.RunningGameJson, RouteReplace.Slug(_slug));
    }
}