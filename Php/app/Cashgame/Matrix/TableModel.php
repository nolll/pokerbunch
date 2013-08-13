namespace app\Cashgame\Matrix{

	use entities\Homegame;
	use app\Cashgame\Matrix\RowModel;
	use app\Cashgame\Matrix\ColumnHeaderModel;
	use entities\CashgameSuite;

	class TableModel{

		private $homegame;
		public $results;
		public $cashgames;
		public $suite;
		public $showYear;
		public $columnHeaderModels;
		public $rowModels;

		public function __construct(Homegame $homegame, CashgameSuite $suite){
			homegame = $homegame;
			results = $suite.getTotalResults();
			cashgames = $suite.getCashgames();
			suite = $suite;
			showYear = spansMultipleYears();
			columnHeaderModels = getHeaderModels();
			rowModels = getRowModels();
		}

		private function getHeaderModels(){
			$models = array();
			foreach(cashgames as $cashgame){
				$models[] = new ColumnHeaderModel(homegame, $cashgame, showYear);
			}
			return $models;
		}

		private function getRowModels(){
			$models = array();
			$rank = 0;
			foreach(results as $result){
				$rank++;
				$models[] = new RowModel(homegame, suite, $result, $rank);
			}
			return $models;
		}

		private function spansMultipleYears(){
			$years = array();
			foreach(cashgames as $cashgame){
				$year = $cashgame.getStartTime().format('Y');
				if(!in_array($year, $years)){
					$years[] = $year;
				}
			}
			return count($years) > 1;
		}

	}

}