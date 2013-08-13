namespace app\Urls{

	use app\RouteFormats;
	use app\Urls\BaseClasses\CashgameUrlModel;
	use app\UrlFormatter;
	use entities\Cashgame;
	use entities\Homegame;

	class CashgameDetailsChartJsonUrlModel extends CashgameUrlModel{

		public function __construct(Homegame $homegame, Cashgame $cashgame){
			parent::__construct(RouteFormats::cashgameDetailsChartJson, $homegame, $cashgame);
		}

	}

}