using Core.Urls;

namespace Web.Urls
{
    public class CashgameBuyinUrl : SlugUrl
    {
        public CashgameBuyinUrl(string slug)
            : base(Routes.CashgameBuyin, slug)
        {
        }
    }
}