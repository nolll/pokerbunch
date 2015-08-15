namespace Core.Urls
{
    public abstract class CashgameUrl : Url
    {
        protected CashgameUrl(string format, string slug, string dateStr)
            : base(format.Replace("{slug}", slug).Replace("{dateStr}", dateStr))
        {
        }
    }
}