using System;

namespace Infrastructure.System{

	class TimeProvider : ITimeProvider {

		public DateTime GetTime(){
			return new DateTime();
		}

	}

}