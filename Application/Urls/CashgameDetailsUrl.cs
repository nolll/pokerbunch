namespace Application.Urls
{
    public class CashgameDetailsUrl : CashgameUrl
    {
        public CashgameDetailsUrl(string slug, string dateStr)
            : base(RouteFormats.CashgameDetails, slug, dateStr)
        {
        }
    }
}