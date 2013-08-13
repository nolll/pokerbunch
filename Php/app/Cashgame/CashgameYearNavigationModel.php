namespace app\Cashgame{

	use app\Urls\CashgameChartUrlModel;
	use app\Urls\CashgameFactsUrlModel;
	use app\Urls\CashgameLeaderboardUrlModel;
	use app\Urls\CashgameListingUrlModel;
	use app\Urls\CashgameMatrixUrlModel;
	use entities\Homegame;

	class CashgameYearNavigationModel{

		private $homegame;
		private $years;
		private $view;

		public $selected;
		public $yearModels;

		public function __construct(Homegame $homegame, $years, $year = null, $view = null){
			$this->homegame = $homegame;
			$this->years = $years != null ? $years : array();
			$this->view = $view;

			$this->selected = $year != null ? $year : 'All Time';
			$this->yearModels = array();

			$this->setupNav();
		}

		private function setupNav(){
			if($this->years != null){
				for($i = 0; $i < count($this->years); $i++){
					$year = $this->years[$i];
					$this->yearModels[] = new NavigationYearModel($this->getNavigationUrl($year), $year);
				}
				$this->yearModels[] = new NavigationYearModel($this->getNavigationUrl(), 'All Time');
			}
		}

		private function getNavigationUrl($year = null){
			if($this->view == 'matrix'){
				return new CashgameMatrixUrlModel($this->homegame, $year);
			}
			if($this->view == 'leaderboard'){
				return new CashgameLeaderboardUrlModel($this->homegame, $year);
			}
			if($this->view == 'chart'){
				return new CashgameChartUrlModel($this->homegame, $year);
			}
			if($this->view == 'listing'){
				return new CashgameListingUrlModel($this->homegame, $year);
			}
			if($this->view == 'facts'){
				return new CashgameFactsUrlModel($this->homegame, $year);
			}
			return null;
		}

	}

}
