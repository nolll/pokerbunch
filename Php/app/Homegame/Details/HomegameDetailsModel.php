namespace app\Homegame\Details{

	use core\HomegamePageModel;
	use entities\Cashgame;
	use entities\Homegame;
	use Domain\Classes\User;
	use app\Urls\HomegameEditUrlModel;

	class HomegameDetailsModel extends HomegamePageModel {

		public $displayName;
		public $description;
		public $houseRules;
		public $editUrl;
		public $showEditLink;

		public function __construct(User $user,
									Homegame $homegame,
									$isInManagerMode,
									Cashgame $runningGame = null){
			parent::__construct($user, $homegame, $runningGame);
			$this->displayName = $homegame->getDisplayName();
			$this->description = $homegame->getDescription();
			$this->houseRules = $this->formatHouseRules($homegame->getHouseRules());
			$this->editUrl = new HomegameEditUrlModel($homegame);
			$this->showEditLink = $isInManagerMode;
		}

		private function formatHouseRules($houseRules){
			return nl2br($houseRules, false);
		}

	}

}