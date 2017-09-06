using Web.Routes;

namespace Web.Urls.SiteUrls
{
    public class RunningCashgamePlayersJsonUrl : SiteUrl
    {
        private readonly string _slug;

        public RunningCashgamePlayersJsonUrl(string slug)
        {
            _slug = slug;
        }

        protected override string Input => RouteParams.Replace(WebRoutes.Cashgame.RunningPlayersJson, RouteReplace.Slug(_slug));
    }
}