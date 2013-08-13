namespace app\Cashgame\Edit{

	use core\DateTimeFactory;
	use app\Urls\CashgameListingUrlModel;
	use app\Urls\CashgameDetailsUrlModel;
	use core\Globalization;
	use Domain\Interfaces\CashgameRepository;
	use Domain\Interfaces\HomegameRepository;
	use app\Cashgame\CashgameValidatorFactory;
	use core\PageController;
	use core\UserContext;

	class CashgameEditController extends PageController {

		private $userContext;
		private $homegameRepository;
		private $cashgameRepository;
		private $cashgameValidatorFactory;
		private $editPostModel;

		public function __construct(UserContext $userContext,
									HomegameRepository $homegameRepository,
									CashgameRepository $cashgameRepository,
									CashgameValidatorFactory $cashgameValidatorFactory,
									CashgameEditPostModel $editPostModel){
			userContext = $userContext;
			homegameRepository = $homegameRepository;
			cashgameRepository = $cashgameRepository;
			cashgameValidatorFactory = $cashgameValidatorFactory;
			editPostModel = $editPostModel;
		}

		public function action_edit($gameName, $dateStr){
			$homegame = homegameRepository.getByName($gameName);
			userContext.requireManager($homegame);
			$date = DateTimeFactory::create($dateStr, $homegame.getTimezone());
			$cashgame = cashgameRepository.getByDate($homegame, $date);
			$runningGame = cashgameRepository.getRunning($homegame);
			$locations = cashgameRepository.getLocations($homegame);
			$years = cashgameRepository.getYears($homegame);
			$model = new CashgameEditModel(userContext.getUser(), $homegame, $cashgame, $locations, $years, $runningGame, null);
			return showForm($model);
		}

		private function showForm(CashgameEditModel $model){
			return view('app/Cashgame/Edit/Edit', $model);
		}

		public function action_edit_post($gameName, $dateStr){
			$homegame = homegameRepository.getByName($gameName);
			userContext.requireManager($homegame);
			$date = DateTimeFactory::create($dateStr, $homegame.getTimezone());
			$cashgame = cashgameRepository.getByDate($homegame, $date);
			$validator = cashgameValidatorFactory.getEditCashgameValidator(editPostModel);
			if($validator.isValid()){
				$cashgame = editPostModel.getCashgame($cashgame);
				cashgameRepository.updateGame($cashgame);
				$detailsUrl = new CashgameDetailsUrlModel($homegame, $cashgame);
				return redirect($detailsUrl);
			}
			$runningGame = cashgameRepository.getRunning($homegame);
			$locations = cashgameRepository.getLocations($homegame);
			$model = new CashgameEditModel(userContext.getUser(), $homegame, $cashgame, $locations, null, $runningGame, editPostModel);
			$model.setValidationErrors($validator.getErrors());
			return showForm($model);
		}

		public function action_delete($gameName, $dateStr){
			$homegame = homegameRepository.getByName($gameName);
			userContext.requireManager($homegame);
			$date = DateTimeFactory::create($dateStr, $homegame.getTimezone());
			$cashgame = cashgameRepository.getByDate($homegame, $date);
			cashgameRepository.deleteGame($cashgame);
			$year = $date.format('Y');
			$listUrl = new CashgameListingUrlModel($homegame, $year);
			return redirect($listUrl);
		}

	}

}