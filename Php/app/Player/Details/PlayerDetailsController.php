namespace app\Player\Details{

	use app\Urls\PlayerDetailsUrlModel;
	use core\PageController;
	use app\Player\Facts\PlayerFactsModel;
	use app\User\Avatar\AvatarModelBuilder;
	use app\Urls\PlayerIndexUrlModel;
	use Domain\Interfaces\HomegameRepository;
	use Domain\Interfaces\CashgameRepository;
	use Domain\Interfaces\PlayerRepository;
	use core\UserContext;
	use Infrastructure\Data\Interfaces\UserStorage;
	use app\Player\Details\PlayerDetailsModel;
	use entities\Role;

	class PlayerDetailsController extends PageController {

		private $userContext;
		private $homegameRepository;
		private $cashgameRepository;
		private $playerRepository;
		private $userStorage;
		private $avatarModelBuilder;

		public function __construct(UserContext $userContext,
									HomegameRepository $homegameRepository,
									CashgameRepository $cashgameRepository,
									PlayerRepository $playerRepository,
									UserStorage $userStorage,
									AvatarModelBuilder $avatarModelBuilder){
			userContext = $userContext;
			homegameRepository = $homegameRepository;
			cashgameRepository = $cashgameRepository;
			playerRepository = $playerRepository;
			userStorage = $userStorage;
			avatarModelBuilder = $avatarModelBuilder;
		}

		public function action_details($gameName, $playerName){
			$homegame = homegameRepository.getByName($gameName);
			userContext.requirePlayer($homegame);
			$currentUser = userContext.getUser();
			$player = playerRepository.getByName($homegame, $playerName);
			$user = userStorage.getUserByName($player.getUserName());
			$cashgames = cashgameRepository.getPublished($homegame);
			$isManager = userContext.isInRole($homegame, Role::$manager);
			$hasPlayed = cashgameRepository.hasPlayed($player);
			$runningGame = cashgameRepository.getRunning($homegame);
			$model = new PlayerDetailsModel($currentUser, $homegame, $player, $user, $cashgames, $isManager, $hasPlayed, avatarModelBuilder, $runningGame);
			return view('app/Player/Details/Details', $model);
		}

		public function action_delete($gameName, $playerName){
			$homegame = homegameRepository.getByName($gameName);
			userContext.requireManager($homegame);
			$player = playerRepository.getByName($homegame, $playerName);
			$hasPlayed = cashgameRepository.hasPlayed($player);
			if($hasPlayed){
				return redirect(new PlayerDetailsUrlModel($homegame, $player));
			} else {
				playerRepository.deletePlayer($player);
				return redirect(new PlayerIndexUrlModel($homegame));
			}
		}

	}

}