namespace app\Urls\BaseClasses{

	use app\UrlFormatter;
	use entities\Cashgame;
	use entities\Homegame;

	class CashgameUrlModel extends UrlModel{

		public function __construct($format, Homegame $homegame, Cashgame $cashgame){
			$url = UrlFormatter::formatCashgame($format, $homegame, $cashgame);
			parent::__construct($url);
		}

	}

}