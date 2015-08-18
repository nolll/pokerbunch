using System.Globalization;

namespace Core.Urls
{
    public static class RouteParams
    {
        public const string Slug = "{slug}";
        public const string Year = "{year?}";
        public const string PlayerId = "{playerId}";
        public const string CashgameId = "{cashgameId}";
        public const string Id = "{id}";
        public const string UserName = "{userName}";

        public static string ReplaceSlug(string format, string slug)
        {
            return Replace(format, Slug, slug);
        }

        public static string ReplaceOptionalYear(string format, int year)
        {
            return Replace(format, Year, year);
        }

        public static string ReplacePlayerId(string format, int playerId)
        {
            return Replace(format, PlayerId, playerId);
        }

        public static string ReplaceCashgameId(string format, int cashgameId)
        {
            return Replace(format, CashgameId, cashgameId);
        }

        public static string ReplaceId(string format, int id)
        {
            return Replace(format, Id, id);
        }

        public static string ReplaceUserName(string format, string userName)
        {
            return Replace(format, UserName, userName);
        }

        private static string Replace(string format, string key, string value)
        {
            return format.Replace(key, value);
        }

        private static string Replace(string format, string key, int value)
        {
            return format.Replace(key, value.ToString(CultureInfo.InvariantCulture));
        }
    }
}