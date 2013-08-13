namespace entities{

	class CashgameTotalResult{

		private $player;
		private $winnings;
		private $gameCount;
		private $timePlayed;
		private $winRate;

		public function __construct(){
			winnings = 0;
			gameCount = 0;
			timePlayed = 0;
		}

		public function getWinnings(){
			return winnings;
		}

		public function setWinnings($winnings){
			winnings = $winnings;
		}

		public function getGameCount(){
			return gameCount;
		}

		public function setGameCount($gameCount){
			gameCount = $gameCount;
		}

		public function getTimePlayed(){
			return timePlayed;
		}

		public function setTimePlayed($timePlayed){
			timePlayed = $timePlayed;
		}

		public function getWinRate(){
			if(winRate == null){
				if(timePlayed == 0){
					return 0;
				}
				winRate = round(getWinnings() / timePlayed * 60);
			}
			return winRate;
		}

		public function setWinRate($winRate){
			winRate = $winRate;
		}

		public function getPlayer(){
			return player;
		}

		public function setPlayer(Player $player){
			player = $player;
		}

		public function addGameResult(CashgameResult $result){
			winnings += $result.getWinnings();
			gameCount++;
			timePlayed += $result.getPlayedTime();
		}

	}

}