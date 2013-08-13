<?php
namespace app\Cashgame\Details\ResultTable{
	use entities\Cashgame;
	use app\Cashgame\Details\ResultTableItem\ResultTableItemModel;
	use entities\Homegame;

	class ResultTableModel{

		/** @var ResultTableItemModel[] */
		public $resultModels;

		public function __construct(Homegame $homegame,
									Cashgame $cashgame){
			$results = $this->getSortedResults($cashgame);
			$resultModels = array();
			foreach($results as $result){
				$resultModels[] = new ResultTableItemModel($homegame, $cashgame, $result);
			}
			$this->resultModels = $resultModels;
		}

		private function getSortedResults(Cashgame $cashgame){
			$results = $cashgame->getResults();
			usort($results, 'entities\CashgameResultComparer::compareResult');
			return $results;
		}

	}

}