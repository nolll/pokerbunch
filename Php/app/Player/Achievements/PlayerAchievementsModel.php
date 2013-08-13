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
			player = $player;
			cashgames = $cashgames;

			if($player == null || $cashgames == null){
				return;
			}
			setNumberOfGamesAchievements();
		}

		private function setNumberOfGamesAchievements(){
			$n1 = new NumberOfGamesAchievement(player, cashgames, 1);
			playedOneGame = $n1.earned();

			$n10 = new NumberOfGamesAchievement(player, cashgames, 10);
			playedTenGames = $n10.earned();

			$n50 = new NumberOfGamesAchievement(player, cashgames, 50);
			played50Games = $n50.earned();

			$n100 = new NumberOfGamesAchievement(player, cashgames, 100);
			played100Games = $n100.earned();

            $n200 = new NumberOfGamesAchievement(player, cashgames, 200);
            played200Games = $n200.earned();

			$n500 = new NumberOfGamesAchievement(player, cashgames, 500);
			played500Games = $n500.earned();
		}

	}

}