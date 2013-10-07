using System;

namespace Infrastructure.System{
    public class TimeProvider : ITimeProvider {

        public DateTime GetTime()
        {
            return DateTime.Now;
        }

        public DateTime GetTime(TimeZoneInfo timeZone)
        {
            return TimeZoneInfo.ConvertTime(DateTime.Now, timeZone);
        }

	}

}