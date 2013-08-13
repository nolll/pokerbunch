namespace app\Urls{

	use app\RouteFormats;
	use app\Urls\BaseClasses\HomegameUrlModel;
	use entities\Homegame;

	class PlayerAddConfirmationUrlModel extends HomegameUrlModel{

		public function __construct(Homegame $homegame){
			parent::__construct(RouteFormats::playerAddConfirmation, $homegame);
		}

	}

}