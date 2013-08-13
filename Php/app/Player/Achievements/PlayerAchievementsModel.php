namespace app\Player\Achievements{

	use app\Player\Achievements\SingleAchievements\NumberOfGamesAchievement;
	use entities\Player;

	class PlayerAchievementsModel{

		private $player;
		private $cashgames;

		public $playedOneGame;
		public $playedTenGames;
		public $played50Games;
		public $played100Games;
        public $played200Games;
		public $played500Games;

		public function __construct(Player $player = null, $cashgames = null){
			$this->player = $player;
			$this->cashgames = $cashgames;

			if($player == null || $cashgames == null){
				return;
			}
			$this->setNumberOfGamesAchievements();
		}

		private function setNumberOfGamesAchievements(){
			$n1 = new NumberOfGamesAchievement($this->player, $this->cashgames, 1);
			$this->playedOneGame = $n1->earned();

			$n10 = new NumberOfGamesAchievement($this->player, $this->cashgames, 10);
			$this->playedTenGames = $n10->earned();

			$n50 = new NumberOfGamesAchievement($this->player, $this->cashgames, 50);
			$this->played50Games = $n50->earned();

			$n100 = new NumberOfGamesAchievement($this->player, $this->cashgames, 100);
			$this->played100Games = $n100->earned();

            $n200 = new NumberOfGamesAchievement($this->player, $this->cashgames, 200);
            $this->played200Games = $n200->earned();

			$n500 = new NumberOfGamesAchievement($this->player, $this->cashgames, 500);
			$this->played500Games = $n500->earned();
		}

	}

}