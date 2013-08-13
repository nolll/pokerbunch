namespace app\Urls{

	use app\RouteFormats;
	use app\Urls\BaseClasses\HomegameUrlModel;
	use entities\Homegame;

	class CashgameAddUrlModel extends HomegameUrlModel{

		public function __construct(Homegame $homegame){
			parent::__construct(RouteFormats::cashgameAdd, $homegame);
		}

	}

}