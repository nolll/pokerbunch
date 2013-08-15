using System;

namespace Infrastructure.System{
    public class TimeProvider : ITimeProvider {

		public DateTime GetTime(){
			return new DateTime();
		}

	}

}