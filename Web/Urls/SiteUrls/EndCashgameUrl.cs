using Web.Routes;

namespace Web.Urls.SiteUrls
{
    public class EndCashgameUrl : SiteUrl
    {
        private readonly string _slug;

        public EndCashgameUrl(string slug)
        {
            _slug = slug;
        }

        protected override string Input => RouteParams.Replace(WebRoutes.Cashgame.End, RouteReplace.Slug(_slug));
    }
}