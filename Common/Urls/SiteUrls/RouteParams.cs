namespace PokerBunch.Common.Urls.SiteUrls
{
    public class RouteReplace
    {
        public string Pattern { get; }
        public string Value { get; }

        private RouteReplace(string pattern, string value)
        {
            Pattern = pattern;
            Value = value;
        }

        public static RouteReplace BunchId(string bunchId) => new RouteReplace(nameof(bunchId), bunchId);
        public static RouteReplace PlayerId(string playerId) => new RouteReplace(nameof(playerId), playerId);
        public static RouteReplace UserName(string userName) => new RouteReplace(nameof(userName), userName);
    }

    public static class RouteParams
    {
        public static string Replace(string format, params RouteReplace[] routeReplaces)
        {
            var result = format;
            foreach (var rp in routeReplaces)
            {
                result = Replace(result, $"{{{rp.Pattern}}}", rp.Value);
            }
            return result;
        }

        private static string Replace(string format, string key, string value)
        {
            return format.Replace(key, value);
        }
    }
}