namespace app\Homegame\Listing{

	use core\PageModel;
	use Domain\Classes\User;

	class HomegameListingModel extends PageModel {

		public $homegameModels;

		public function __construct(User $user, array $homegames){
			parent::__construct($user);
			homegameModels = getHomegameModels($homegames);
		}

		private function getHomegameModels(array $homegames){
			$models = array();
			foreach($homegames as $homegame){
				$models[] = new HomegameItemModel($homegame);
			}
			return $models;
		}

	}

}