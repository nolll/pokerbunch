namespace app\Cashgame\Listing\CashgameTable{

	use entities\Homegame;
	use app\Cashgame\Listing\CashgameTableItem\CashgameTableItemModel;

	class CashgameTableModel{

		public $homegame;
		public $cashgames;
		public $showYear;
		public $listItemModels;

		public function __construct(Homegame $homegame, array $cashgames){
			homegame = $homegame;
			cashgames = $cashgames;
			showYear = spansMultipleYears();
			listItemModels = getListItemModels();
		}

		private function getListItemModels(){
			$models = array();
			foreach(cashgames as $cashgame){
				$models[] = new CashgameTableItemModel(homegame, $cashgame, showYear);
			}
			return $models;
		}

		private function spansMultipleYears(){
			$years = array();
			foreach(cashgames as $cashgame){ /** @var $cashgame \entities\Cashgame */
				$year = $cashgame.getStartTime().format('Y');
				if(!in_array($year, $years)){
					$years[] = $year;
				}
			}
			return count($years) > 1;
		}

	}

}