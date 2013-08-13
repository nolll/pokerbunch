namespace app\Player\Listing{

	use core\PageController;
	use Domain\Interfaces\CashgameRepository;
	use Domain\Interfaces\HomegameRepository;
	use core\UserContext;
	use entities\Role;
	use Domain\Interfaces\PlayerRepository;

	class PlayerListingController extends PageController {

		private $userContext;
		private $homegameRepository;
		private $playerRepository;
		private $cashgameRepository;

		public function __construct(UserContext $userContext,
									HomegameRepository $homegameRepository,
									PlayerRepository $playerRepository,
									CashgameRepository $cashgameRepository){
			$this->userContext = $userContext;
			$this->homegameRepository = $homegameRepository;
			$this->playerRepository = $playerRepository;
			$this->cashgameRepository = $cashgameRepository;
		}

		public function action_index($gameName){
			$homegame = $this->homegameRepository->getByName($gameName);
			$this->userContext->requirePlayer($homegame);
			$isInManagerMode = $this->userContext->isInRole($homegame, Role::$manager);
			$players = $this->playerRepository->getAll($homegame);
			$runningGame = $this->cashgameRepository->getRunning($homegame);
			$model = new PlayerListingModel($this->userContext->getUser(), $homegame, $players, $isInManagerMode, $runningGame);
			return $this->view('app/Player/Listing/Listing', $model);
		}

    }

}