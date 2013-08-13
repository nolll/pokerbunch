namespace app\Cashgame\Chart {

	use app\Chart\ChartColumnModel;
	use app\Chart\ChartModel;
	use app\Chart\ChartRowModel;
	use app\Chart\ChartValueModel;
	use core\Globalization;
	use entities\Cashgame;
	use entities\CashgameSuite;
	use entities\Checkpoints\CheckpointType;
	use entities\Homegame;

	class ChartData extends ChartModel{

		private $homegame;
		private $suite;
		private $results;
		private $year;
		private $playerSum;

		public function __construct(Homegame $homegame, CashgameSuite $suite, $year){
			$this->homegame = $homegame;
			$this->suite = $suite;
			$this->results = $suite->getTotalResults();
			$this->year = $year;
			$this->addChartColumns();
			$this->addGameRows();
		}

		private function addGameRows(){
			$this->initPlayerSumArray();
			$this->addFirstRow();
			$cashgames = $this->suite->getCashgames();
			for ($i = 0; $i < count($cashgames); $i++){
				$cashgame = $cashgames[count($cashgames) - $i - 1];
				$currentSum = array();
				for ($j = 0; $j < count($this->results); $j++) {
					$totalResult = $this->results[$j];
					$singleResult = $cashgame->getResult($totalResult->getPlayer());
					$playerId = $totalResult->getPlayer()->getId();
					if($singleResult != null || $i == count($cashgames) - 1){
						$res = $singleResult != null ? $singleResult->getStack() - $singleResult->getBuyin() : 0;
						$sum = $this->playerSum[$totalResult->getPlayer()->getId()] + $res;
						$this->playerSum[$playerId] = $this->playerSum[$totalResult->getPlayer()->getId()] + $res;
						$currentSum[$playerId] = $sum;
					} else {
						$currentSum[$playerId] = null;
					}
				}
				$this->addGameRow($cashgame, $currentSum);
			}
		}

		private function initPlayerSumArray(){
			$this->playerSum = array();
			foreach($this->results as $result){
				$this->playerSum[$result->getPlayer()->getId()] = 0;
			}
		}

		private function addChartColumns(){
			$dateCol = new ChartColumnModel('string', 'Date');
			$this->addColumn($dateCol);

			foreach($this->results as $playerResult) {
				$playerCol = new ChartColumnModel('number', $playerResult->getPlayer()->getDisplayName());
				$this->addColumn($playerCol);
			}
		}

		private function addFirstRow(){
			$row1 = new ChartRowModel();
			$row1->addValue(new ChartValueModel(null));
			foreach($this->results as $result){
				$row1->addValue(new ChartValueModel(0));
			}
			$this->addRow($row1);
		}

		private function addGameRow(Cashgame $cashgame, $currentSum){
			$row1 = new ChartRowModel();
			$row1->addValue(new ChartValueModel(Globalization::formatShortDate($cashgame->getStartTime())));
			foreach($this->results as $result){
				$sum = $currentSum[$result->getPlayer()->getId()];
				$row1->addValue(new ChartValueModel($sum));
			}
			$this->addRow($row1);
		}

	}

}