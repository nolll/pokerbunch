namespace Core.Urls
{
    public class CashgameIndexUrl : SlugUrl
    {
        public CashgameIndexUrl(string slug)
            : base(Routes.CashgameIndex, slug)
        {
        }
    }
}