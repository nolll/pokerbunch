namespace Core.Urls
{
    public class CashgameActionUrl : CashgamePlayerUrl
    {
        public CashgameActionUrl(string slug, string dateStr, int playerId)
            : base(RouteFormats.CashgameAction, slug, dateStr, playerId)
        {
        }
    }
}