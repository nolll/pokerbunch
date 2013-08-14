using System;

namespace Infrastructure.System{

	class TimeProvider : ITimeProvider {

		public DateTime getTime(){
			return new DateTime();
		}

	}

}