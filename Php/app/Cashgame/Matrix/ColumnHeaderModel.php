namespace app\Cashgame\Matrix{

	use entities\Homegame;
	use app\Urls\CashgameDetailsUrlModel;
	use core\Globalization;
	use entities\Cashgame;

	class ColumnHeaderModel{

		public $date;
		public $cashgameUrl;

		public function __construct(Homegame $homegame, Cashgame $cashgame, $showYear = false){
			date = Globalization::formatShortDate($cashgame.getStartTime(), $showYear);
			cashgameUrl = new CashgameDetailsUrlModel($homegame, $cashgame);
		}

	}

}