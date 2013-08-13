<?php
namespace tests\Fakes {

	use core\Timer;
	use DateTime;

	class TimerFake implements Timer {

		private $time;

		public function setTime(DateTime $time){
			$this->time = $time;
		}

		public function getTime(){
			return $this->time;
		}

	}

}