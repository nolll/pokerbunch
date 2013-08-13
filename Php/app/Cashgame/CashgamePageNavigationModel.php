namespace app\Cashgame{

	use app\Urls\CashgameChartUrlModel;
	use app\Urls\CashgameFactsUrlModel;
	use app\Urls\CashgameLeaderboardUrlModel;
	use app\Urls\CashgameListingUrlModel;
	use app\Urls\CashgameMatrixUrlModel;
	use entities\Cashgame;
	use entities\Homegame;

	class CashgamePageNavigationModel{

		private $homegame;
		private $year;
		private $view;
		private $runningGame;

		public $selected;
		public $matrixLink;
		public $leaderboardLink;
		public $chartLink;
		public $listingLink;
		public $factsLink;

		public function __construct(Homegame $homegame, $year = null, $view = null, Cashgame $runningGame = null){
			homegame = $homegame;
			year = $year;
			view = $view;
			runningGame = $runningGame;

			selected = $view;

			setupNav();
		}

		private function setupNav(){
			matrixLink = new CashgameMatrixUrlModel(homegame, year);
			leaderboardLink = new CashgameLeaderboardUrlModel(homegame, year);
			chartLink = new CashgameChartUrlModel(homegame, year);
			listingLink = new CashgameListingUrlModel(homegame, year);
			factsLink = new CashgameFactsUrlModel(homegame, year);
		}

	}

}
