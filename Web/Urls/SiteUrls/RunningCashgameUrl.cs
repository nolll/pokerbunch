using Web.Routes;

namespace Web.Urls.SiteUrls
{
    public class RunningCashgameUrl : SiteUrl
    {
        private readonly string _slug;

        public RunningCashgameUrl(string slug)
        {
            _slug = slug;
        }

        protected override string Input => RouteParams.Replace(WebRoutes.Cashgame.Running, RouteParam.Slug(_slug));
    }
}