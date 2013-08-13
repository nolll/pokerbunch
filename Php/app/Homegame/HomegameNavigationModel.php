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
			$this->heading = $homegame->getDisplayName();
			$this->headingLink = new HomegameDetailsUrlModel($homegame);
			$this->cashgameLink = new CashgameIndexUrlModel($homegame);
			$this->playerLink = new PlayerIndexUrlModel($homegame);
			$this->createLink = new CashgameAddUrlModel($homegame);
			$this->runningLink = new RunningCashgameUrlModel($homegame);
			$this->cashgameIsRunning = $runningGame != null;
		}

	}

}
