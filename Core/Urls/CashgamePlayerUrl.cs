namespace Core.Urls
{
    public abstract class CashgamePlayerUrl : Url
    {
        protected CashgamePlayerUrl(string format, string slug, string dateStr, int playerId)
            : base(BuildUrl(format, slug, dateStr, playerId))
        {
        }

        private static string BuildUrl(string format, string slug, string dateStr, int playerId)
        {
            var url = RouteParams.ReplaceSlug(format, slug);
            url = RouteParams.ReplaceDateStr(url, dateStr);
            return RouteParams.ReplacePlayerId(url, playerId);
        }
    }
}