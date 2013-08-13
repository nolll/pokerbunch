<?php
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
			$this->homegame = $homegame;
			$this->year = $year;
			$this->view = $view;
			$this->runningGame = $runningGame;

			$this->selected = $view;

			$this->setupNav();
		}

		private function setupNav(){
			$this->matrixLink = new CashgameMatrixUrlModel($this->homegame, $this->year);
			$this->leaderboardLink = new CashgameLeaderboardUrlModel($this->homegame, $this->year);
			$this->chartLink = new CashgameChartUrlModel($this->homegame, $this->year);
			$this->listingLink = new CashgameListingUrlModel($this->homegame, $this->year);
			$this->factsLink = new CashgameFactsUrlModel($this->homegame, $this->year);
		}

	}

}
