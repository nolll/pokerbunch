namespace app\Cashgame\Running{

	use entities\Homegame;
	use entities\Cashgame;
	use app\Urls\RunningCashgameUrlModel;

	class BarModel{

		public $gameIsRunning;
		public $gameUrl;

		public function __construct(Homegame $homegame, Cashgame $runningGame = null){
			if($runningGame != null){
				gameIsRunning = true;
				gameUrl = new RunningCashgameUrlModel($homegame);
			}
		}

	}

}