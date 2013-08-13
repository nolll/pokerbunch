<?php
namespace app\Cashgame\Listing\CashgameTable{

	use entities\Homegame;
	use app\Cashgame\Listing\CashgameTableItem\CashgameTableItemModel;

	class CashgameTableModel{

		public $homegame;
		public $cashgames;
		public $showYear;
		public $listItemModels;

		public function __construct(Homegame $homegame, array $cashgames){
			$this->homegame = $homegame;
			$this->cashgames = $cashgames;
			$this->showYear = $this->spansMultipleYears();
			$this->listItemModels = $this->getListItemModels();
		}

		private function getListItemModels(){
			$models = array();
			foreach($this->cashgames as $cashgame){
				$models[] = new CashgameTableItemModel($this->homegame, $cashgame, $this->showYear);
			}
			return $models;
		}

		private function spansMultipleYears(){
			$years = array();
			foreach($this->cashgames as $cashgame){ /** @var $cashgame \entities\Cashgame */
				$year = $cashgame->getStartTime()->format('Y');
				if(!in_array($year, $years)){
					$years[] = $year;
				}
			}
			return count($years) > 1;
		}

	}

}