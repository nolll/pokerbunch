namespace entities{

	class CashgameTotalResult{

		private $player;
		private $winnings;
		private $gameCount;
		private $timePlayed;
		private $winRate;

		public function __construct(){
			$this->winnings = 0;
			$this->gameCount = 0;
			$this->timePlayed = 0;
		}

		public function getWinnings(){
			return $this->winnings;
		}

		public function setWinnings($winnings){
			$this->winnings = $winnings;
		}

		public function getGameCount(){
			return $this->gameCount;
		}

		public function setGameCount($gameCount){
			$this->gameCount = $gameCount;
		}

		public function getTimePlayed(){
			return $this->timePlayed;
		}

		public function setTimePlayed($timePlayed){
			$this->timePlayed = $timePlayed;
		}

		public function getWinRate(){
			if($this->winRate == null){
				if($this->timePlayed == 0){
					return 0;
				}
				$this->winRate = round($this->getWinnings() / $this->timePlayed * 60);
			}
			return $this->winRate;
		}

		public function setWinRate($winRate){
			$this->winRate = $winRate;
		}

		public function getPlayer(){
			return $this->player;
		}

		public function setPlayer(Player $player){
			$this->player = $player;
		}

		public function addGameResult(CashgameResult $result){
			$this->winnings += $result->getWinnings();
			$this->gameCount++;
			$this->timePlayed += $result->getPlayedTime();
		}

	}

}