namespace app\Urls{

	use app\RouteFormats;
	use app\Urls\BaseClasses\HomegameYearUrlModel;
	use entities\Homegame;

	class CashgameFactsUrlModel extends HomegameYearUrlModel{

		public function __construct(Homegame $homegame, $year){
			parent::__construct(RouteFormats::cashgameFacts, RouteFormats::cashgameFactsWithYear, $homegame, $year);
		}

	}

}