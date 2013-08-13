namespace core{

	use entities\Cashgame;
	use entities\Homegame;
	use app\Homegame\HomegameNavigationModel;
	use Domain\Classes\User;

	class HomegamePageModel extends PageModel{

		public $homegameNavigationModel;

		public function __construct(User $user = null,
									Homegame $homegame = null,
									Cashgame $runningGame = null){
			parent::__construct($user);
			if($homegame != null){
				$this->homegameNavigationModel = new HomegameNavigationModel($homegame, $runningGame);
			}
		}

	}

}