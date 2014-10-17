using System;

namespace Tests.Common.Builders
{
    public static class TestService
    {
        public static TimeZoneInfo LocalTimeZone
        {
            get { return TimeZoneInfo.FindSystemTimeZoneById(LocalTimeZoneName); }
        }

        public static string LocalTimeZoneName
        {
            get { return "W. Europe Standard Time"; }
        }
    }
}