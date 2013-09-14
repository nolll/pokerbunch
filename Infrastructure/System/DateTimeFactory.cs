using System;

namespace Infrastructure.System{

	public static class DateTimeFactory{

		public static DateTime Now(TimeZoneInfo timezone = null){
			return Create(DateTime.Now, timezone);
		}

		public static DateTime Create(string str, TimeZoneInfo timezone = null)
		{
		    var dateTime = DateTime.Parse(str);
		    return Create(dateTime, timezone);
		}

        private static DateTime Create(DateTime dateTime, TimeZoneInfo timezone = null)
        {
            if(timezone != null){
				//todo: adjust for timezone
			}
		    return dateTime;
        }

		public static DateTime ToUtc(DateTime dateTime){
            //todo: adjust for timezone
			return dateTime;
		}

	}

}