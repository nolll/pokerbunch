using System;

namespace Infrastructure.System{

	public static class DateTimeFactory{

		public static DateTime Now(TimeZoneInfo timezone = null){
			return Create(null, timezone);
		}

		public static DateTime Create(string str, TimeZoneInfo timezone = null)
		{
		    var date = DateTime.Parse(str);
            if(timezone != null){
				//todo: adjust for timezone
			}
		    return date;
		}

		public static DateTime ToUtc(DateTime dateTime){
            //todo: adjust for timezone
			return dateTime;
		}

	}

}