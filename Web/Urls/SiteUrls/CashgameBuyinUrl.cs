using Web.Routes;

namespace Web.Urls.SiteUrls
{
    public class CashgameBuyinUrl : SiteUrl
    {
        private readonly string _slug;

        public CashgameBuyinUrl(string slug)
        {
            _slug = slug;
        }

        protected override string Input => RouteParams.Replace(WebRoutes.Cashgame.Buyin, RouteParam.Slug(_slug));
    }
}