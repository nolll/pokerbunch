<?php
namespace app\Cashgame\Running{
	use core\Timer;
	use entities\Cashgame;
	use core\Globalization;
	use entities\Homegame;

	class StatusTableModel{

		/** @var StatusItemModel[] */
		public $statusModels;
		public $totalBuyin;
		public $totalStacks;

		public function __construct(Homegame $homegame,
									Cashgame $cashgame,
									$isManager,
									Timer $timer = null){
			$results = $this->getSortedResults($cashgame);
			$resultModels = array();
			foreach($results as $result){
				$resultModels[] = new StatusItemModel($homegame, $cashgame, $result, $isManager, $timer);
			}
			$this->statusModels = $resultModels;
			$this->totalBuyin = Globalization::formatCurrency($homegame->getCurrency(), $cashgame->getTurnover());
			$this->totalStacks = Globalization::formatCurrency($homegame->getCurrency(), $cashgame->getTotalStacks());
		}

		private function getSortedResults(Cashgame $cashgame){
			$results = $cashgame->getResults();
			usort($results, 'entities\CashgameResultComparer::compareWinnings');
			return $results;
		}

	}

}