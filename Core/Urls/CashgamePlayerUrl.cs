namespace Core.Urls
{
    public abstract class CashgamePlayerUrl : Url
    {
        protected CashgamePlayerUrl(string format, string slug, string dateStr, int playerId)
            : base(string.Format(format, slug, dateStr, playerId))
        {
        }
    }
}