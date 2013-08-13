namespace app\Cashgame\Edit{
	use core\HomegamePageModel;
	use entities\Homegame;
	use Domain\Classes\User;
	use core\FormFields\LocationFieldModel;
	use entities\GameStatus;
	use app\Urls\CashgameDeleteUrlModel;
	use app\Urls\CashgameDetailsUrlModel;
	use core\Globalization;
	use entities\Cashgame;

	class CashgameEditModel extends HomegamePageModel {

		private $homegame;
		public $isoDate;
		public $startTime;
		public $endTime;
		public $enableDelete;
		public $cancelUrl;
		public $deleteUrl;
		public $locationSelectModel;

		public function __construct(User $user,
									Homegame $homegame,
									Cashgame $cashgame,
									array $locations,
									array $years = null,
									Cashgame $runningGame = null,
									CashgameEditPostModel $postModel = null){
			parent::__construct($user, $homegame, $runningGame);
			homegame = $homegame;
			isoDate = Globalization::formatIsoDate($cashgame.getStartTime());
			cancelUrl = new CashgameDetailsUrlModel($homegame, $cashgame);
			deleteUrl = new CashgameDeleteUrlModel($homegame, $cashgame);
			enableDelete = $cashgame.getStatus() != GameStatus::published;
			$location = $postModel != null ? $postModel.location : $cashgame.getLocation();
			locationSelectModel = getLocationSelectModel($locations, $location);
		}

		private function getLocationSelectModel($locations, $location){
			return new LocationFieldModel('location', 'location', $location, $locations);
		}

	}

}