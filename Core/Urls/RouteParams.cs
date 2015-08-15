using System.Globalization;

namespace Core.Urls
{
    public static class RouteParams
    {
        public static string ReplaceSlug(string format, string slug)
        {
            return Replace(format, "{slug}", slug);
        }

        public static string ReplaceOptionalYear(string format, int year)
        {
            return Replace(format, "{year?}", year);
        }

        public static string ReplaceDateStr(string format, string dateStr)
        {
            return Replace(format, "{dateStr}", dateStr);
        }

        public static string ReplacePlayerId(string format, int playerId)
        {
            return Replace(format, "{playerId}", playerId);
        }

        public static string ReplaceCheckpointId(string format, int checkpointId)
        {
            return Replace(format, "{checkpointId}", checkpointId);
        }

        public static string ReplaceId(string format, int id)
        {
            return Replace(format, "{id}", id);
        }

        public static string ReplaceEventId(string format, int eventId)
        {
            return Replace(format, "{eventId}", eventId);
        }

        public static string ReplaceUserName(string format, string userName)
        {
            return Replace(format, "{userName}", userName);
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