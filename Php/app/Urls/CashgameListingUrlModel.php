namespace app\Urls{

	use app\RouteFormats;
	use app\Urls\BaseClasses\HomegameYearUrlModel;
	use entities\Homegame;

	class CashgameListingUrlModel extends HomegameYearUrlModel{

		public function __construct(Homegame $homegame, $year){
			parent::__construct(RouteFormats::cashgameListing, RouteFormats::cashgameListingWithYear, $homegame, $year);
		}

	}

}