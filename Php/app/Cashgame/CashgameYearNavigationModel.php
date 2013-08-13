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
			homegame = $homegame;
			years = $years != null ? $years : array();
			view = $view;

			selected = $year != null ? $year : 'All Time';
			yearModels = array();

			setupNav();
		}

		private function setupNav(){
			if(years != null){
				for($i = 0; $i < count(years); $i++){
					$year = years[$i];
					yearModels[] = new NavigationYearModel(getNavigationUrl($year), $year);
				}
				yearModels[] = new NavigationYearModel(getNavigationUrl(), 'All Time');
			}
		}

		private function getNavigationUrl($year = null){
			if(view == 'matrix'){
				return new CashgameMatrixUrlModel(homegame, $year);
			}
			if(view == 'leaderboard'){
				return new CashgameLeaderboardUrlModel(homegame, $year);
			}
			if(view == 'chart'){
				return new CashgameChartUrlModel(homegame, $year);
			}
			if(view == 'listing'){
				return new CashgameListingUrlModel(homegame, $year);
			}
			if(view == 'facts'){
				return new CashgameFactsUrlModel(homegame, $year);
			}
			return null;
		}

	}

}
