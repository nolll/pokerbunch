namespace core{

	use app\User\UserNavigationModel;
	use core\Analytics\GoogleAnalyticsModel;
	use entities\Homegame;
	use Domain\Classes\User;

	class PageModel {

		public $validationErrors;
		public $userNavigationModel;
		public $homegameNavigationModel;
		public $googleAnalyticsModel;

		public function __construct(User $user = null){
			userNavigationModel = new UserNavigationModel($user);
			googleAnalyticsModel = new GoogleAnalyticsModel();
		}

		public function setValidationErrors(array $errors){
			validationErrors = $errors;
		}

	}

}