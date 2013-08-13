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
			$this->homegame = $homegame;
			$this->results = $suite->getTotalResults();
			$this->cashgames = $suite->getCashgames();
			$this->suite = $suite;
			$this->showYear = $this->spansMultipleYears();
			$this->columnHeaderModels = $this->getHeaderModels();
			$this->rowModels = $this->getRowModels();
		}

		private function getHeaderModels(){
			$models = array();
			foreach($this->cashgames as $cashgame){
				$models[] = new ColumnHeaderModel($this->homegame, $cashgame, $this->showYear);
			}
			return $models;
		}

		private function getRowModels(){
			$models = array();
			$rank = 0;
			foreach($this->results as $result){
				$rank++;
				$models[] = new RowModel($this->homegame, $this->suite, $result, $rank);
			}
			return $models;
		}

		private function spansMultipleYears(){
			$years = array();
			foreach($this->cashgames as $cashgame){
				$year = $cashgame->getStartTime()->format('Y');
				if(!in_array($year, $years)){
					$years[] = $year;
				}
			}
			return count($years) > 1;
		}

	}

}