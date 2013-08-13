namespace app\Home{

	use app\Urls\HomegameAddUrlModel;
	use app\Admin\AdminNavModel;
	use core\HomegamePageModel;
	use entities\Cashgame;
	use entities\Homegame;
	use Domain\Classes\User;
	use app\Urls\UserAddUrlModel;
	use app\Urls\AuthLoginUrlModel;

	class HomeModel extends HomegamePageModel {

		public $isLoggedIn;
		public $addHomegameUrl;
		public $loginUrl;
		public $registerUrl;
		public $adminNav;

		public function __construct(User $user = null,
									Homegame $homegame = null,
									Cashgame $runningGame = null){
			parent::__construct($user, $homegame, $runningGame);
			$this->isLoggedIn = $user != null;
			$this->addHomegameUrl = new HomegameAddUrlModel();
			$this->loginUrl = new AuthLoginUrlModel();
			$this->registerUrl = new UserAddUrlModel();
			$this->adminNav = new AdminNavModel($user);
		}

	}

}