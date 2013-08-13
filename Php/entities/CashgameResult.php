<?php
namespace entities{

	use DateTime;
	use entities\Checkpoints\Checkpoint;

	class CashgameResult{

		private $player;
		private $buyin;
		private $stack;
		private $winnings;
		private $buyinTime;
		private $cashoutTime;
		private $lastReportTime;
		private $playedTime;
		private $cashoutCheckpoint;
		private $hasReported;

		/**
		 * @var Checkpoint[]
		 */
		private $checkpoints;

		public function __construct(){
			$this->checkpoints = array();
		}

		/**
		 * @return Player
		 */
		public function getPlayer(){
			return $this->player;
		}

		public function setPlayer(Player $player){
			$this->player = $player;
		}

		public function getBuyin(){
			return $this->buyin;
		}

		public function setBuyin($amount){
			$this->buyin = $amount;
		}

		public function getWinnings(){
			return $this->winnings;
		}

		public function setWinnings($winnings){
			$this->winnings = $winnings;
		}

        public function getCheckpoints(){
            return $this->checkpoints;
        }

		public function setCheckpoints(array $checkpoints){
			$this->checkpoints = $checkpoints;
		}

		public function getBuyinTime(){
			return $this->buyinTime;
		}

		public function setBuyinTime(DateTime $time = null){
			$this->buyinTime = $time;
		}

		public function getCashoutTime(){
			return $this->cashoutTime;
		}

		public function setCashoutTime(DateTime $time = null){
			$this->cashoutTime = $time;
		}

		public function getPlayedTime(){
			return $this->playedTime;
		}

		public function setPlayedTime($minutes){
			$this->playedTime = $minutes;
		}

		public function getStack(){
			return $this->stack;
		}

		public function setStack($stack){
			$this->stack = $stack;
		}

		public function getLastReportTime(){
			return $this->lastReportTime;
		}

		public function setLastReportTime(DateTime $time = null){
			$this->lastReportTime = $time;
		}

		public function getCashoutCheckpoint(){
			return $this->cashoutCheckpoint;
		}

		public function setCashoutCheckpoint(Checkpoint $checkPoint = null){
			$this->cashoutCheckpoint = $checkPoint;
		}

		public function hasReported(){
			$this->hasReported;
		}

		public function setHasReported($hasReported){
			$this->hasReported = $hasReported;
		}

	}

}