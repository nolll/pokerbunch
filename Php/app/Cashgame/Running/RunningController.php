namespace app\Cashgame\Running{

	use core\PageController;
	use app\Urls\CashgameIndexUrlModel;
	use core\DateTimeFactory;
	use core\Globalization;
	use core\Timer;
	use core\UserContext;
	use entities\Player;
	use Domain\Interfaces\CashgameRepository;
	use Domain\Interfaces\PlayerRepository;
	use Domain\Interfaces\HomegameRepository;
	use entities\Homegame;
	use entities\Cashgame;
	use entities\Role;
	use integration\Sharing\ResultSharer;

	class RunningController extends PageController {

		private $userContext;
		private $homegameRepository;
		private $cashgameRepository;
		private $playerRepository;
		private $resultSharer;
		private $timer;

		public function __construct(UserContext $userContext,
									HomegameRepository $homegameRepository,
									CashgameRepository $cashgameRepository,
									PlayerRepository $playerRepository,
									ResultSharer $resultSharer,
									Timer $timer){
			userContext = $userContext;
			homegameRepository = $homegameRepository;
			cashgameRepository = $cashgameRepository;
			resultSharer = $resultSharer;
			playerRepository = $playerRepository;
			timer = $timer;
		}

		public function action_running($gameName){
			$homegame = homegameRepository.getByName($gameName);
			userContext.requirePlayer($homegame);
			$cashgame = cashgameRepository.getRunning($homegame);
			if($cashgame == null){
				return redirect(new CashgameIndexUrlModel($homegame));
			}
			$user = userContext.getUser();
			$player = playerRepository.getByUserName($homegame, $user.getUserName());
			$model = getModel($homegame, $cashgame, $player);
			return view('app/Cashgame/Running/Running', $model);
		}

		public function getModel(Homegame $homegame, Cashgame $cashgame, Player $player){
			$isManager = userContext.isInRole($homegame, Role::$manager);
			$runningGame = cashgameRepository.getRunning($homegame);
			$years = cashgameRepository.getYears($homegame);
			return new RunningModel(userContext.getUser(), $homegame, $cashgame, $player, $years, $isManager, timer, $runningGame);
		}

	}

}