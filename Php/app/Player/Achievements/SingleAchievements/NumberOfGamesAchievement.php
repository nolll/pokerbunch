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
			$this->player = $player;
			$this->cashgames = $cashgames;
			if($this->cashgames == null){
				$this->earned = false;
				return;
			}
			$this->earned = $this->getNumberOfPlayedGames() >= $numberToCheck;
		}

		public function earned(){
			return $this->earned;
		}

		private function getNumberOfPlayedGames(){
			$numberOfGames = 0;
			foreach($this->cashgames as $cashgame){
				if($cashgame->isInGame($this->player)){
					$numberOfGames++;
				}
			}
			return $numberOfGames;
		}

	}

}