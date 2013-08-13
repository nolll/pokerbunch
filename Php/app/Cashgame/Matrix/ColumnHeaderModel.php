<?php
namespace app\Cashgame\Matrix{

	use entities\Homegame;
	use app\Urls\CashgameDetailsUrlModel;
	use core\Globalization;
	use entities\Cashgame;

	class ColumnHeaderModel{

		public $date;
		public $cashgameUrl;

		public function __construct(Homegame $homegame, Cashgame $cashgame, $showYear = false){
			$this->date = Globalization::formatShortDate($cashgame->getStartTime(), $showYear);
			$this->cashgameUrl = new CashgameDetailsUrlModel($homegame, $cashgame);
		}

	}

}