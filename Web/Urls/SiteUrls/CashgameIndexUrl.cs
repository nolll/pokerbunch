using Web.Routes;

namespace Web.Urls.SiteUrls
{
    public class CashgameIndexUrl : SlugUrl
    {
        public CashgameIndexUrl(string slug)
            : base(WebRoutes.Cashgame.Index, slug)
        {
        }
    }
}