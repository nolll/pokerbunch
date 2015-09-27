using Web.Common.Routes;

namespace Web.Urls
{
    public class CashgameIndexUrl : SlugUrl
    {
        public CashgameIndexUrl(string slug)
            : base(WebRoutes.CashgameIndex, slug)
        {
        }
    }
}