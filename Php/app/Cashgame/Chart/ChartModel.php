namespace app\Cashgame\Chart{

	use app\Cashgame\CashgameNavigationModel;
	use app\Urls\CashgameChartJsonUrlModel;
	use core\HomegamePageModel;
	use entities\Cashgame;
	use entities\CashgameSuite;
	use entities\Homegame;
	use Domain\Classes\User;
	use core\Globalization;

	class ChartModel extends HomegamePageModel {

		public $cashgameNavModel;
		private $playerResults;
		private $cashgames;
		private $year;

		public $chartDataUrl;

		public function __construct(User $user,
									Homegame $homegame,
									CashgameSuite $suite,
									$year,
									array $years = null,
									Cashgame $runningGame = null){
			parent::__construct($user, $homegame, $runningGame);
			$this->playerResults = $suite->getTotalResults();
			$this->cashgames = $suite->getCashgames();
			$this->year;
			$this->chartDataUrl = new CashgameChartJsonUrlModel($homegame, $year);
			$this->cashgameNavModel = new CashgameNavigationModel($homegame, 'chart', $years, $year, $runningGame);
		}

	}

}