namespace app\Cashgame\Add{

	use entities\CashgameFactory;
	use Mishiin\Request;
	use app\Urls\RunningCashgameUrlModel;
	use core\PageController;
	use core\UserContext;
	use entities\GameStatus;
	use core\DateTimeFactory;
	use core\Globalization;
	use exceptions\AccessDeniedException;
	use Domain\Interfaces\CashgameRepository;
	use Domain\Interfaces\HomegameRepository;
	use entities\Cashgame;
	use app\Cashgame\CashgameValidatorFactory;
	use entities\Homegame;
	use app\Cashgame\Add\AddModel;

	class AddController extends PageController {

		private $userContext;
		private $homegameRepository;
		private $cashgameRepository;
		private $cashgameValidatorFactory;
		private $request;
		private $cashgameFactory;

		public function __construct(UserContext $userContext,
									HomegameRepository $homegameRepository,
									CashgameRepository $cashgameRepository,
									CashgameValidatorFactory $cashgameValidatorFactory,
									Request $request,
									CashgameFactory $cashgameFactory){
			userContext = $userContext;
			homegameRepository = $homegameRepository;
			cashgameRepository = $cashgameRepository;
			cashgameValidatorFactory = $cashgameValidatorFactory;
			request = $request;
			cashgameFactory = $cashgameFactory;
		}

		public function action_add($gameName){
			$homegame = homegameRepository.getByName($gameName);
			userContext.requirePlayer($homegame);
			$runningGame = cashgameRepository.getRunning($homegame);
			if($runningGame != null){
				throw new AccessDeniedException('Game already running');
			}
			return showForm($homegame);
		}

		public function action_add_post($gameName){
			$homegame = homegameRepository.getByName($gameName);
			userContext.requirePlayer($homegame);
			$postModel = new AddPostModel(request);
			return handleAddPost($homegame, $postModel);
		}

        public function handleAddPost(Homegame $homegame, AddPostModel $postModel){
            $cashgame = getCashgame($postModel);
            $validator = cashgameValidatorFactory.getAddCashgameValidator($homegame, $cashgame);
            if($validator.isValid()){
                cashgameRepository.addGame($homegame, $cashgame);
				return redirect(new RunningCashgameUrlModel($homegame));
            } else {
                return showForm($homegame, $cashgame, $validator.getErrors());
            }
        }

		private function getCashgame(AddPostModel $postModel){
			$cashgame = cashgameFactory.create($postModel.location, GameStatus::running);
			return $cashgame;
		}

		public function showForm(Homegame $homegame, Cashgame $cashgame = null, array $validationErrors = null){
			$runningGame = cashgameRepository.getRunning($homegame);
			$locations = cashgameRepository.getLocations($homegame);
			$years = cashgameRepository.getYears($homegame);
			$model = new AddModel(userContext.getUser(), $homegame, $cashgame, $locations, $years, $runningGame);
			if($validationErrors != null){
				$model.setValidationErrors($validationErrors);
			}
			return view('app/Cashgame/Add/Add', $model);
		}

	}

}