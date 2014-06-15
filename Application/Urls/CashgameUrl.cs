namespace Application.Urls
{
    public abstract class CashgameUrl : Url
    {
        protected CashgameUrl(string format, string slug, string dateStr)
            : base(string.Format(format, slug, dateStr))
        {
        }
    }
}