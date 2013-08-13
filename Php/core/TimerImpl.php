namespace core{

	use DateTime;

	class TimerImpl implements Timer {

		public function getTime(){
			return new DateTime();
		}

	}

}