<?php
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
			$this->homegame = $homegame;
			$this->gameCount = $suite->getGameCount();
			$this->totalGameTime = Globalization::formatDuration($suite->getTotalGametime());
			$this->setBestResult($suite->getBestResult());
			$this->setWorstResult($suite->getWorstResult());
			$this->setMostTime($suite->getMostTimeResult());
			$this->cashgameNavModel = new CashgameNavigationModel($homegame, 'facts', $years, $year, $runningGame);
		}

		private function setBestResult(CashgameResult $result = null){
			if($result != null){
				$this->bestResultAmount = Globalization::formatResult($this->homegame->getCurrency(), $result->getWinnings());
				$player = $result->getPlayer();
				if($player != null){
					$this->bestResultName = $player->getDisplayName();
				}
			}
		}

		private function setWorstResult(CashgameResult $result = null){
			if($result != null){
				$this->worstResultAmount = Globalization::formatResult($this->homegame->getCurrency(), $result->getWinnings());
				$player = $result->getPlayer();
				if($player != null){
					$this->worstResultName = $player->getDisplayName();
				}
			}
		}

		private function setMostTime(CashgameTotalResult $result = null){
			if($result != null){
				$this->mostTimeDuration = Globalization::formatDuration($result->getTimePlayed());
				$player = $result->getPlayer();
				if($player != null){
					$this->mostTimeName = $player->getDisplayName();
				}
			}
		}

	}

}