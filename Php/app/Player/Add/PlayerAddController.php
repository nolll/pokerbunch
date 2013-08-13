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
			$this->userContext = $userContext;
			$this->playerRepository = $playerRepository;
			$this->homegameRepository = $homegameRepository;
			$this->cashgameRepository = $cashgameRepository;
			$this->playerValidatorFactory = $playerValidatorFactory;
			$this->request = $request;
			$this->playerFactory = $playerFactory;
		}

		public function action_add($gameName){
			$homegame = $this->homegameRepository->getByName($gameName);
			$this->userContext->requireManager($homegame);
			return $this->showForm($homegame);
		}

		public function action_add_post($gameName){
			$homegame = $this->homegameRepository->getByName($gameName);
			$this->userContext->requireManager($homegame);
			$player = $this->getPostedPlayer();
			$validator = $this->playerValidatorFactory->getAddPlayerValidator($player, $homegame);
			if($validator->isValid()){
				$this->playerRepository->addPlayer($homegame, $player->getDisplayName());
				return $this->redirect(new PlayerAddConfirmationUrlModel($homegame));
			} else {
				return $this->showForm($homegame, $validator->getErrors());
			}
		}

		public function action_created($gameName){
			$homegame = $this->homegameRepository->getByName($gameName);
			$runningGame = $this->cashgameRepository->getRunning($homegame);
			$model = new PlayerAddConfirmationModel($this->userContext->getUser(), $homegame, $runningGame);
			return $this->view('app/Player/Add/Confirmation', $model);
		}

		private function showForm(Homegame $homegame, array $validationErrors = null){
			$runningGame = $this->cashgameRepository->getRunning($homegame);
			$model = new HomegamePageModel($this->userContext->getUser(), $homegame, $runningGame);
			if($validationErrors != null){
				$model->setValidationErrors($validationErrors);
			}
			return $this->view('app/Player/Add/Add', $model);
		}

		private function getPostedPlayer(){
			$displayName = $this->request->getParamPost('name');
			return $this->playerFactory->create($displayName, Role::$player);
		}

	}

}