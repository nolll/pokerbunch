namespace app\Cashgame\Details{

	use app\Cashgame\Action\GameChartData;
	use core\PageController;
	use app\Error\HttpNotFoundError;
	use core\UserContext;
	use Domain\Classes\User;
	use core\DateTimeFactory;
	use core\Globalization;
	use Domain\Interfaces\CashgameRepository;
	use Domain\Interfaces\HomegameRepository;
	use entities\Homegame;
	use entities\Cashgame;
	use entities\Role;
	use DateTime;
	use integration\Sharing\ResultSharer;
	use Domain\Interfaces\PlayerRepository;

	class DetailsController extends PageController {

		private $userContext;
		private $homegameRepository;
		private $cashgameRepository;
		private $playerRepository;
		private $resultSharer;

		public function __construct(UserContext $userContext,
									HomegameRepository $homegameRepository,
									CashgameRepository $cashgameRepository,
									PlayerRepository $playerRepository,
									ResultSharer $resultSharer){
			userContext = $userContext;
			homegameRepository = $homegameRepository;
			cashgameRepository = $cashgameRepository;
			playerRepository = $playerRepository;
			resultSharer = $resultSharer;
		}

		public function action_details($gameName, $dateStr){
			$homegame = homegameRepository.getByName($gameName);
			userContext.requirePlayer($homegame);
			$date = DateTimeFactory::create($dateStr, $homegame.getTimezone());
			$cashgame = cashgameRepository.getByDate($homegame, $date);
			if($cashgame == null){
				return error(new HttpNotFoundError());
			}
			$user = userContext.getUser();
			$model = getModel($user, $homegame, $cashgame);
			return view('app/Cashgame/Details/Details', $model);
		}

		public function action_detailschartjson($gameName, $dateStr){
			$homegame = homegameRepository.getByName($gameName);
			userContext.requirePlayer($homegame);
			$date = DateTimeFactory::create($dateStr, $homegame.getTimezone());
			$cashgame = cashgameRepository.getByDate($homegame, $date);
			if($cashgame == null){
				return error(new HttpNotFoundError());
			}
			$model = new GameChartData($homegame, $cashgame);
			return json($model);
		}

		public function getModel(User $user, Homegame $homegame, Cashgame $cashgame){
			$player = playerRepository.getByUserName($homegame, $user.getUserName());
			$isManager = userContext.isInRole($homegame, Role::$manager);
			$runningGame = cashgameRepository.getRunning($homegame);
			$years = cashgameRepository.getYears($homegame);
			return new DetailsModel($user, $homegame, $cashgame, $player, $years, $isManager, $runningGame);
		}

	}

}