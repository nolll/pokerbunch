namespace Core.Urls
{
    public class CashgameActionUrl : CashgamePlayerUrl
    {
        public CashgameActionUrl(string slug, string dateStr, int playerId)
            : base(Routes.CashgameAction, slug, dateStr, playerId)
        {
        }
    }
}