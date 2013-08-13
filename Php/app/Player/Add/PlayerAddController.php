namespace app\Player\Add{

	use core\HomegamePageModel;
	use Domain\Interfaces\CashgameRepository;
	use Domain\Interfaces\PlayerRepository;
	use entities\PlayerFactory;
	use entities\Role;
	use Mishiin\Request;
	use app\Urls\PlayerAddConfirmationUrlModel;
	use core\PageController;
	use Domain\Interfaces\HomegameRepository;
	use core\UserContext;
	use entities\Homegame;
	use app\Player\PlayerValidatorFactory;

	class PlayerAddController extends PageController {

		private $userContext;
		private $playerRepository;
		private $homegameRepository;
		private $cashgameRepository;
		private $playerValidatorFactory;
		private $request;
		private $playerFactory;

		public function __construct(UserContext $userContext,
									PlayerRepository $playerRepository,
									HomegameRepository $homegameRepository,
									CashgameRepository $cashgameRepository,
									PlayerValidatorFactory $playerValidatorFactory,
									Request $request,
									PlayerFactory $playerFactory){
			userContext = $userContext;
			playerRepository = $playerRepository;
			homegameRepository = $homegameRepository;
			cashgameRepository = $cashgameRepository;
			playerValidatorFactory = $playerValidatorFactory;
			request = $request;
			playerFactory = $playerFactory;
		}

		public function action_add($gameName){
			$homegame = homegameRepository.getByName($gameName);
			userContext.requireManager($homegame);
			return showForm($homegame);
		}

		public function action_add_post($gameName){
			$homegame = homegameRepository.getByName($gameName);
			userContext.requireManager($homegame);
			$player = getPostedPlayer();
			$validator = playerValidatorFactory.getAddPlayerValidator($player, $homegame);
			if($validator.isValid()){
				playerRepository.addPlayer($homegame, $player.getDisplayName());
				return redirect(new PlayerAddConfirmationUrlModel($homegame));
			} else {
				return showForm($homegame, $validator.getErrors());
			}
		}

		public function action_created($gameName){
			$homegame = homegameRepository.getByName($gameName);
			$runningGame = cashgameRepository.getRunning($homegame);
			$model = new PlayerAddConfirmationModel(userContext.getUser(), $homegame, $runningGame);
			return view('app/Player/Add/Confirmation', $model);
		}

		private function showForm(Homegame $homegame, array $validationErrors = null){
			$runningGame = cashgameRepository.getRunning($homegame);
			$model = new HomegamePageModel(userContext.getUser(), $homegame, $runningGame);
			if($validationErrors != null){
				$model.setValidationErrors($validationErrors);
			}
			return view('app/Player/Add/Add', $model);
		}

		private function getPostedPlayer(){
			$displayName = request.getParamPost('name');
			return playerFactory.create($displayName, Role::$player);
		}

	}

}