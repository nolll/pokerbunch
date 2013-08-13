namespace app\Homegame\Add{

	use Domain\Interfaces\PlayerRepository;
	use Mishiin\Request;
	use core\PageController;
	use core\PageModel;
	use DateTimeZone;
	use core\Globalization;
	use app\Urls\HomegameAddConfirmationUrlModel;
	use core\UserContext;
	use entities\CurrencySettings;
	use Infrastructure\Data\Interfaces\HomegameStorage;
	use entities\Homegame;
	use app\Homegame\SlugGenerator;
	use app\Homegame\HomegameValidatorFactory;
	use entities\Role;
	use app\Homegame\Add\HomegameAddModel;

	class HomegameAddController extends PageController {

		private $userContext;
		private $homegameStorage;
		private $playerRepository;
		private $slugGenerator;
		private $homegameValidatorFactory;
		private $request;

		public function __construct(UserContext $userContext,
									HomegameStorage $homegameStorage,
									PlayerRepository $playerRepository,
									SlugGenerator $slugGenerator,
									HomegameValidatorFactory $homegameValidatorFactory,
									Request $request){
			userContext = $userContext;
			homegameStorage = $homegameStorage;
			playerRepository = $playerRepository;
			slugGenerator = $slugGenerator;
			homegameValidatorFactory = $homegameValidatorFactory;
			request = $request;
		}

		public function action_add(){
			userContext.requireUser();
			return showForm();
		}

		public function action_add_post(){
			userContext.requireUser();
			$homegame = getPostedHomegame();
			$validator = homegameValidatorFactory.getAddHomegameValidator($homegame);
			if($validator.isValid()){
				$homegame = homegameStorage.addHomegame($homegame);
				$user = userContext.getUser();
				playerRepository.addPlayerWithUser($homegame, $user, Role::$manager);
				return redirect(new HomegameAddConfirmationUrlModel());
			} else {
				return showForm($homegame, $validator.getErrors());
			}
		}

		public function action_created(){
			$model = new PageModel(userContext.getUser());
			return view('app/Homegame/Add/Confirmation', $model);
		}

		private function getPostedHomegame(){
			$homegame = new Homegame();
			$homegame.setDisplayName(request.getParamPost('displayname'));
			$currencySymbol = request.getParamPost('currencysymbol');
			$currencyLayout = request.getParamPost('currencylayout');
			$currency = new CurrencySettings($currencySymbol, $currencyLayout);
			$homegame.setCurrency($currency);
			$timezoneName = request.getParamPost('timezone');
			if($timezoneName != null){
				$homegame.setTimezone(new DateTimeZone($timezoneName));
			}
			$homegame.setDescription(request.getParamPost('description'));
			$homegame.setSlug(slugGenerator.getSlug($homegame.getDisplayName()));
			return $homegame;
		}

		private function showForm(Homegame $homegame = null, array $validationErrors = null){
			$model = new HomegameAddModel(userContext.getUser(), $homegame);
			if($validationErrors != null){
				$model.setValidationErrors($validationErrors);
			}
			return view('app/Homegame/Add/Add', $model);
		}

	}

}