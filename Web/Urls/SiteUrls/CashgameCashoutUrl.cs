using Web.Routes;

namespace Web.Urls.SiteUrls
{
    public class CashgameCashoutUrl : SiteUrl
    {
        private readonly string _slug;

        public CashgameCashoutUrl(string slug)
        {
            _slug = slug;
        }

        protected override string Input => RouteParams.Replace(WebRoutes.Cashgame.Cashout, RouteReplace.Slug(_slug));
    }
}