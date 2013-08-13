namespace app\Player\Achievements\SingleAchievements{

	use entities\Player;
	use entities\Cashgame;

	class NumberOfGamesAchievement implements Achievement{

		private $earned;
		/** @var Player */
		private $player;
		/** @var Cashgame[] */
		private $cashgames;

		public function __construct(Player $player, $cashgames, $numberToCheck){
			player = $player;
			cashgames = $cashgames;
			if(cashgames == null){
				earned = false;
				return;
			}
			earned = getNumberOfPlayedGames() >= $numberToCheck;
		}

		public function earned(){
			return earned;
		}

		private function getNumberOfPlayedGames(){
			$numberOfGames = 0;
			foreach(cashgames as $cashgame){
				if($cashgame.isInGame(player)){
					$numberOfGames++;
				}
			}
			return $numberOfGames;
		}

	}

}