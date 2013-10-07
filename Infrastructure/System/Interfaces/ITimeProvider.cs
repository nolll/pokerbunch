using System;

namespace Infrastructure.System{

	public interface ITimeProvider {

		DateTime GetTime();
        DateTime GetTime(TimeZoneInfo timeZone);

	}

}