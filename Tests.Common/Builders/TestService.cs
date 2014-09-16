using System;

namespace Tests.Common.Builders
{
    public static class TestService
    {
        public static TimeZoneInfo LocalTimeZone
        {
            get { return TimeZoneInfo.FindSystemTimeZoneById("W. Europe Standard Time"); }
        }
    }
}