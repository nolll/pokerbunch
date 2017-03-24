using Web.Routes;

namespace Web.Urls.SiteUrls
{
    public class CashgameIndexUrl : SiteUrl
    {
        private readonly string _slug;

        public CashgameIndexUrl(string slug)
        {
            _slug = slug;
        }

        protected override string Input => RouteParams.Replace(WebRoutes.Cashgame.Index, RouteParam.Slug(_slug));
    }
}