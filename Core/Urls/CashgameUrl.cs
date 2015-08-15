namespace Core.Urls
{
    public abstract class CashgameUrl : Url
    {
        protected CashgameUrl(string format, string slug, string dateStr)
            : base(BuildUrl(format, slug, dateStr))
        {
        }

        private static string BuildUrl(string format, string slug, string dateStr)
        {
            var url = RouteParams.ReplaceSlug(format, slug);
            return RouteParams.ReplaceDateStr(url, dateStr);
        }
    }
}