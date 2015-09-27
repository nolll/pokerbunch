using Web.Common.Routes;

namespace Web.Common.Urls.SiteUrls
{
    public class AddCashgameUrl : SlugUrl
    {
        public AddCashgameUrl(string slug)
            : base(WebRoutes.CashgameAdd, slug)
        {
        }
    }
}