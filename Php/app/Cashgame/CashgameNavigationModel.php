<?php
namespace app\Cashgame{

	use entities\Cashgame;
	use entities\Homegame;

	class CashgameNavigationModel{

		public $pageNavModel;
		public $yearNavModel;

		public function __construct(Homegame $homegame, $view, $years, $year = null, Cashgame $runningGame = null){
			$this->pageNavModel = new CashgamePageNavigationModel($homegame, $year, $view, $runningGame);
			$this->yearNavModel = new CashgameYearNavigationModel($homegame, $years, $year, $view);
		}

	}

}