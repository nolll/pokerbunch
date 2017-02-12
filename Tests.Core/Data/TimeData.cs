using System;

namespace Tests.Core.Data
{
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