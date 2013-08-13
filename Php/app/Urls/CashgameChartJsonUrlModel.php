namespace app\Urls{

	use app\RouteFormats;
	use app\Urls\BaseClasses\HomegameYearUrlModel;
	use app\UrlFormatter;
	use entities\Homegame;

	class CashgameChartJsonUrlModel extends HomegameYearUrlModel{

		public function __construct(Homegame $homegame, $year){
			parent::__construct(RouteFormats::cashgameChartJson, RouteFormats::cashgameChartJsonWithYear, $homegame, $year);
		}

	}

}