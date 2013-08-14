using System;

namespace Infrastructure.System{

	public static class DateTimeFactory{

		public static DateTime now(TimeZoneInfo timezone = null){
			return create(null, timezone);
		}

		public static DateTime create(string str, TimeZoneInfo timezone = null)
		{
		    var date = DateTime.Parse(str);
            if(timezone != null){
				//adjust for timezone
			}
		    return date;
		}

		public static DateTime toUtc(DateTime dateTime){
            //adjust for timezone
			return dateTime;
		}

	}

}