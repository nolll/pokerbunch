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
			userContext = $userContext;
			homegameRepository = $homegameRepository;
			playerRepository = $playerRepository;
			cashgameRepository = $cashgameRepository;
		}

		public function action_index($gameName){
			$homegame = homegameRepository.getByName($gameName);
			userContext.requirePlayer($homegame);
			$isInManagerMode = userContext.isInRole($homegame, Role::$manager);
			$players = playerRepository.getAll($homegame);
			$runningGame = cashgameRepository.getRunning($homegame);
			$model = new PlayerListingModel(userContext.getUser(), $homegame, $players, $isInManagerMode, $runningGame);
			return view('app/Player/Listing/Listing', $model);
		}

    }

}