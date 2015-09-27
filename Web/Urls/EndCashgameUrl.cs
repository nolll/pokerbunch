using Web.Common.Routes;

namespace Web.Urls
{
    public class EndCashgameUrl : SlugUrl
    {
        public EndCashgameUrl(string slug)
            : base(WebRoutes.CashgameEnd, slug)
        {
        }
    }
}