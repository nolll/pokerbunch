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
			$this->userContext = $userContext;
			$this->homegameRepository = $homegameRepository;
			$this->cashgameRepository = $cashgameRepository;
			$this->playerRepository = $playerRepository;
		}

		public function action_action($gameName, $dateStr, $playerName){
			$this->homegame = $this->homegameRepository->getByName($gameName);
			$this->cashgame = $this->cashgameRepository->getByDateString($this->homegame, $dateStr);
			$this->player = $this->playerRepository->getByName($this->homegame, $playerName);
			$this->userContext->requirePlayer($this->homegame);
			$role = $this->userContext->getRole($this->homegame);
			$runningGame = $this->cashgameRepository->getRunning($this->homegame);
			$years = $this->cashgameRepository->getYears($this->homegame);
			$result = $this->cashgame->getResult($this->player);
			$model = new ActionModel($this->userContext->getUser(), $this->homegame, $this->cashgame, $this->player, $result, $role, $years, $runningGame);
			return $this->view('app/Cashgame/Action/Action', $model);
		}

		public function action_actionchartjson($gameName, $dateStr, $playerName){
			$this->homegame = $this->homegameRepository->getByName($gameName);
			$this->cashgame = $this->cashgameRepository->getByDateString($this->homegame, $dateStr);
			$this->player = $this->playerRepository->getByName($this->homegame, $playerName);
			$result = $this->cashgame->getResult($this->player);
			$model = new ActionChartData($this->homegame, $this->cashgame, $result);
			return $this->json($model);
		}

	}

}