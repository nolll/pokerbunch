<?php
namespace app\Cashgame\Leaderboard{
	use entities\CashgameSuite;
	use entities\Homegame;

	class TableModel{

		public $itemModels;

		public function __construct(Homegame $homegame, CashgameSuite $suite){
			$this->itemModels = $this->getItemModels($homegame, $suite);
		}

		private function getItemModels(Homegame $homegame, CashgameSuite $suite){
			$results = $suite->getTotalResults();
			$models = array();
			$rank = 1;
			foreach($results as $result){
				$models[] = new ItemModel($homegame, $result, $rank);
				$rank++;
			}
			return $models;
		}

	}

}