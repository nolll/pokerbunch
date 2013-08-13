namespace app\Cashgame\Add{

	use core\HomegamePageModel;
	use entities\Homegame;
	use Domain\Classes\User;
	use core\FormFields\LocationFieldModel;
	use core\Globalization;
	use entities\Cashgame;

	class AddModel extends HomegamePageModel {

		public $date;
		public $location;
		public $locationSelectModel;

		public function __construct(User $user = null,
									Homegame $homegame,
									Cashgame $cashgame = null,
									array $locations,
									array $years = null,
									Cashgame $runningGame = null){
			parent::__construct($user, $homegame, $runningGame);
			if($cashgame != null){
				$this->location = $cashgame->getLocation();
			}
			$this->locationSelectModel = $this->getLocationSelectModel($locations, $this->location);
		}

		private function getLocationSelectModel($locations, $location){
			return new LocationFieldModel('location', 'location', $location, $locations);
		}

	}

}