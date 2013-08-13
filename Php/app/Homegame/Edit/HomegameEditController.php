namespace app\Homegame\Edit{

	use Domain\Interfaces\CashgameRepository;
	use Mishiin\Request;
	use core\PageController;
	use DateTimeZone;
	use core\Globalization;
	use app\Urls\HomegameDetailsUrlModel;
	use core\UserContext;
	use entities\CurrencySettings;
	use Infrastructure\Data\Interfaces\HomegameStorage;
	use Domain\Interfaces\HomegameRepository;
	use entities\Homegame;
	use app\Homegame\HomegameValidatorFactory;
	use app\Homegame\Edit\HomegameEditModel;

	class HomegameEditController extends PageController {

		private $userContext;
		private $homegameStorage;
		private $homegameRepository;
		private $homegameValidatorFactory;
		private $cashgameRepository;
		private $request;

		public function __construct(UserContext $userContext,
									HomegameStorage $homegameStorage,
									HomegameRepository $homegameRepository,
									HomegameValidatorFactory $homegameValidatorFactory,
									CashgameRepository $cashgameRepository,
									Request $request){
			userContext = $userContext;
			homegameStorage = $homegameStorage;
			homegameRepository = $homegameRepository;
			homegameValidatorFactory = $homegameValidatorFactory;
			cashgameRepository = $cashgameRepository;
			request = $request;
		}

		public function action_edit($gameName){
			$homegame = homegameRepository.getByName($gameName);
			userContext.requireManager($homegame);
			return showForm($homegame);
		}

		public function action_edit_post($gameName){
			$homegame = homegameRepository.getByName($gameName);
			userContext.requireManager($homegame);
			$homegame = getPostedHomegame($homegame);
			$validator = homegameValidatorFactory.getEditHomegameValidator($homegame);
			if($validator.isValid()){
				homegameStorage.updateHomegame($homegame);
				return redirect(new HomegameDetailsUrlModel($homegame));
			} else {
				return showForm($homegame, $validator.getErrors());
			}
		}

		private function getPostedHomegame(Homegame $homegame){
			$homegame.setDescription(request.getParamPost('description'));
			$homegame.setHouseRules(request.getParamPost('houserules'));
			$currencySymbol = request.getParamPost('currencysymbol');
			$currencyLayout = request.getParamPost('currencylayout');
			$currency = new CurrencySettings($currencySymbol, $currencyLayout);
			$homegame.setCurrency($currency);
			$timezoneName = request.getParamPost('timezone');
			if($timezoneName != null){
				$homegame.setTimezone(new DateTimeZone($timezoneName));
			}
			$homegame.setDefaultBuyin(request.getParamPost('defaultbuyin'));
			$homegame.cashgamesEnabled = request.getParamPost('cashgames') != null;
			$homegame.tournamentsEnabled = request.getParamPost('tournaments') != null;
			$homegame.videosEnabled = request.getParamPost('videos') != null;
			return $homegame;
		}

		private function showForm(Homegame $homegame, array $validationErrors = null){
			$runningGame = cashgameRepository.getRunning($homegame);
			$model = new HomegameEditModel(userContext.getUser(), $homegame, $runningGame);
			if($validationErrors != null){
				$model.setValidationErrors($validationErrors);
			}
			return view('app/Homegame/Edit/Edit', $model);
		}

	}

}