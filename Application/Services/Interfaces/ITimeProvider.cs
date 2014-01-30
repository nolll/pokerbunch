using System;

namespace Application.Services.Interfaces{

	public interface ITimeProvider {
	    DateTime GetTime();
	    DateTime GetTime(TimeZoneInfo timeZone);
	    DateTime Parse(string str, TimeZoneInfo timezone = null);
	    DateTime ConvertToUtc(DateTime dateTime);
	}

}