using System;

namespace Tests.Core.Data
{
    public static class PlayerData
    {
        public const string Id1 = "player-id-1";
        public const string Name1 = "player-name-1";
        public const string Color1 = "#111";

        public const string Id2 = "player-id-2";
        public const string Name2 = "player-name-2";
        public const string Color2 = "#222";
    }

    public static class TimeData
    {
        public static DateTime Swedish(string s)
        {
            var timeUnspec = DateTime.SpecifyKind(DateTime.Parse(s), DateTimeKind.Unspecified);
            var timezone = TimezoneData.Swedish;
            var timeUtc = TimeZoneInfo.ConvertTimeToUtc(timeUnspec, TimezoneData.Swedish);
            return TimeZoneInfo.ConvertTime(timeUtc, timezone);
        }

        public static DateTime Utc(string s)
        {
            var time = DateTime.Parse(s);
            return DateTime.SpecifyKind(time, DateTimeKind.Utc);
        }
    }
}