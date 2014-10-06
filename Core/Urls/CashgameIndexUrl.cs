namespace Core.Urls
{
    public class CashgameIndexUrl : BunchUrl
    {
        public CashgameIndexUrl(string slug)
            : base(RouteFormats.CashgameIndex, slug)
        {
        }
    }
}