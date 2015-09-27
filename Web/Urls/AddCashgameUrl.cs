using Web.Common.Routes;

namespace Web.Urls
{
    public class AddCashgameUrl : SlugUrl
    {
        public AddCashgameUrl(string slug)
            : base(WebRoutes.CashgameAdd, slug)
        {
        }
    }
}