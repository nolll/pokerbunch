namespace app\Homegame{

	use app\UrlFormatter;
	use app\Urls\CashgameAddUrlModel;
	use app\Urls\PlayerIndexUrlModel;
	use app\Urls\CashgameIndexUrlModel;
	use app\Urls\HomegameDetailsUrlModel;
	use app\Urls\RunningCashgameUrlModel;
	use entities\Cashgame;
	use entities\Homegame;

	class HomegameNavigationModel{

		public $heading;
		public $headingLink;
		public $cashgameLink;
		public $playerLink;
		public $createLink;
		public $runningLink;
		public $cashgameIsRunning;

		public function __construct(Homegame $homegame, Cashgame $runningGame = null){
			heading = $homegame.getDisplayName();
			headingLink = new HomegameDetailsUrlModel($homegame);
			cashgameLink = new CashgameIndexUrlModel($homegame);
			playerLink = new PlayerIndexUrlModel($homegame);
			createLink = new CashgameAddUrlModel($homegame);
			runningLink = new RunningCashgameUrlModel($homegame);
			cashgameIsRunning = $runningGame != null;
		}

	}

}
