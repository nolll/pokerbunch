namespace app\Cashgame\Facts{

	use app\Cashgame\CashgameNavigationModel;
	use core\Globalization;
	use core\HomegamePageModel;
	use entities\Cashgame;
	use entities\CashgameResult;
	use entities\CashgameSuite;
	use entities\CashgameTotalResult;
	use entities\Homegame;
	use Domain\Classes\User;

	class CashgameFactsModel extends HomegamePageModel {

		public $cashgameNavModel;
		public $gameCount;
		public $totalGameTime;
		public $bestResultAmount;
		public $bestResultName;
		public $worstResultAmount;
		public $worstResultName;
		public $mostTimeDuration;
		public $mostTimeName;

		public function __construct(User $user,
									Homegame $homegame,
									CashgameSuite $suite,
									array $years = null,
									$year = null,
									Cashgame $runningGame = null){
			parent::__construct($user, $homegame, $runningGame);
			homegame = $homegame;
			gameCount = $suite.getGameCount();
			totalGameTime = Globalization::formatDuration($suite.getTotalGametime());
			setBestResult($suite.getBestResult());
			setWorstResult($suite.getWorstResult());
			setMostTime($suite.getMostTimeResult());
			cashgameNavModel = new CashgameNavigationModel($homegame, 'facts', $years, $year, $runningGame);
		}

		private function setBestResult(CashgameResult $result = null){
			if($result != null){
				bestResultAmount = Globalization::formatResult(homegame.getCurrency(), $result.getWinnings());
				$player = $result.getPlayer();
				if($player != null){
					bestResultName = $player.getDisplayName();
				}
			}
		}

		private function setWorstResult(CashgameResult $result = null){
			if($result != null){
				worstResultAmount = Globalization::formatResult(homegame.getCurrency(), $result.getWinnings());
				$player = $result.getPlayer();
				if($player != null){
					worstResultName = $player.getDisplayName();
				}
			}
		}

		private function setMostTime(CashgameTotalResult $result = null){
			if($result != null){
				mostTimeDuration = Globalization::formatDuration($result.getTimePlayed());
				$player = $result.getPlayer();
				if($player != null){
					mostTimeName = $player.getDisplayName();
				}
			}
		}

	}

}