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
			homegame = $homegame;
			suite = $suite;
			results = $suite.getTotalResults();
			year = $year;
			addChartColumns();
			addGameRows();
		}

		private function addGameRows(){
			initPlayerSumArray();
			addFirstRow();
			$cashgames = suite.getCashgames();
			for ($i = 0; $i < count($cashgames); $i++){
				$cashgame = $cashgames[count($cashgames) - $i - 1];
				$currentSum = array();
				for ($j = 0; $j < count(results); $j++) {
					$totalResult = results[$j];
					$singleResult = $cashgame.getResult($totalResult.getPlayer());
					$playerId = $totalResult.getPlayer().getId();
					if($singleResult != null || $i == count($cashgames) - 1){
						$res = $singleResult != null ? $singleResult.getStack() - $singleResult.getBuyin() : 0;
						$sum = playerSum[$totalResult.getPlayer().getId()] + $res;
						playerSum[$playerId] = playerSum[$totalResult.getPlayer().getId()] + $res;
						$currentSum[$playerId] = $sum;
					} else {
						$currentSum[$playerId] = null;
					}
				}
				addGameRow($cashgame, $currentSum);
			}
		}

		private function initPlayerSumArray(){
			playerSum = array();
			foreach(results as $result){
				playerSum[$result.getPlayer().getId()] = 0;
			}
		}

		private function addChartColumns(){
			$dateCol = new ChartColumnModel('string', 'Date');
			addColumn($dateCol);

			foreach(results as $playerResult) {
				$playerCol = new ChartColumnModel('number', $playerResult.getPlayer().getDisplayName());
				addColumn($playerCol);
			}
		}

		private function addFirstRow(){
			$row1 = new ChartRowModel();
			$row1.addValue(new ChartValueModel(null));
			foreach(results as $result){
				$row1.addValue(new ChartValueModel(0));
			}
			addRow($row1);
		}

		private function addGameRow(Cashgame $cashgame, $currentSum){
			$row1 = new ChartRowModel();
			$row1.addValue(new ChartValueModel(Globalization::formatShortDate($cashgame.getStartTime())));
			foreach(results as $result){
				$sum = $currentSum[$result.getPlayer().getId()];
				$row1.addValue(new ChartValueModel($sum));
			}
			addRow($row1);
		}

	}

}