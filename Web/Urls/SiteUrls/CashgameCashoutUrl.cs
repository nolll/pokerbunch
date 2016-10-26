using Web.Routes;

namespace Web.Urls.SiteUrls
{
    public class CashgameCashoutUrl : SlugUrl
    {
        public CashgameCashoutUrl(string slug)
            : base(WebRoutes.Cashgame.Cashout, slug)
        {
        }
    }
}