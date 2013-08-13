namespace app\Cashgame\Running{

	use entities\Homegame;
	use entities\Cashgame;
	use app\Urls\RunningCashgameUrlModel;

	class BarModel{

		public $gameIsRunning;
		public $gameUrl;

		public function __construct(Homegame $homegame, Cashgame $runningGame = null){
			if($runningGame != null){
				$this->gameIsRunning = true;
				$this->gameUrl = new RunningCashgameUrlModel($homegame);
			}
		}

	}

}