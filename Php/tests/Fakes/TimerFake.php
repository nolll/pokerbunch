namespace tests\Fakes {

	use core\Timer;
	use DateTime;

	class TimerFake implements Timer {

		private $time;

		public function setTime(DateTime $time){
			time = $time;
		}

		public function getTime(){
			return time;
		}

	}

}