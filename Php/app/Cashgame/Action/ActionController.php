namespace app\Cashgame\Action{

	use app\Cashgame\Action\ActionChartData;
	use core\DateTimeFactory;
	use app\Cashgame\Action\ActionModel;
	use Domain\Interfaces\HomegameRepository;
	use Domain\Interfaces\CashgameRepository;
	use Domain\Interfaces\PlayerRepository;
	use core\Globalization;
	use core\PageController;
	use core\UserContext;
	use entities\Cashgame;
	use entities\Homegame;
	use entities\Player;

	class ActionController extends PageController {

		private $userContext;
		private $homegameRepository;
		private $cashgameRepository;
		private $playerRepository;

		/** @var Homegame */
		private $homegame;
		/** @var Cashgame */
		private $cashgame;
		/** @var Player */
		private $player;

		public function __construct(UserContext $userContext,
									HomegameRepository $homegameRepository,
									CashgameRepository $cashgameRepository,
									PlayerRepository $playerRepository){
			userContext = $userContext;
			homegameRepository = $homegameRepository;
			cashgameRepository = $cashgameRepository;
			playerRepository = $playerRepository;
		}

		public function action_action($gameName, $dateStr, $playerName){
			homegame = homegameRepository.getByName($gameName);
			cashgame = cashgameRepository.getByDateString(homegame, $dateStr);
			player = playerRepository.getByName(homegame, $playerName);
			userContext.requirePlayer(homegame);
			$role = userContext.getRole(homegame);
			$runningGame = cashgameRepository.getRunning(homegame);
			$years = cashgameRepository.getYears(homegame);
			$result = cashgame.getResult(player);
			$model = new ActionModel(userContext.getUser(), homegame, cashgame, player, $result, $role, $years, $runningGame);
			return view('app/Cashgame/Action/Action', $model);
		}

		public function action_actionchartjson($gameName, $dateStr, $playerName){
			homegame = homegameRepository.getByName($gameName);
			cashgame = cashgameRepository.getByDateString(homegame, $dateStr);
			player = playerRepository.getByName(homegame, $playerName);
			$result = cashgame.getResult(player);
			$model = new ActionChartData(homegame, cashgame, $result);
			return json($model);
		}

	}

}