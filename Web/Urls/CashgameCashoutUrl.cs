using Web.Common.Routes;

namespace Web.Urls
{
    public class CashgameCashoutUrl : SlugUrl
    {
        public CashgameCashoutUrl(string slug)
            : base(WebRoutes.CashgameCashout, slug)
        {
        }
    }
}