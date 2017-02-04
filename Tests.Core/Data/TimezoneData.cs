using System;

namespace Tests.Core.Data
{
    public static class TimezoneData
    {
        public static readonly TimeZoneInfo Utc = TimeZoneInfo.Utc;
        public static readonly TimeZoneInfo Swedish = TimeZoneInfo.FindSystemTimeZoneById("W. Europe Standard Time");
    }
}