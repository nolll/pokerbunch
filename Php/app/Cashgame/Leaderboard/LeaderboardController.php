namespace app\Cashgame\Leaderboard{

	use core\PageController;
	use Domain\Interfaces\HomegameRepository;
	use Domain\Interfaces\CashgameRepository;
	use core\UserContext;

	class LeaderboardController extends PageController {

		private $userContext;
		private $homegameRepository;
		private $cashgameRepository;

		public function __construct(UserContext $userContext,
									HomegameRepository $homegameRepository,
									CashgameRepository $cashgameRepository){
			$this->userContext = $userContext;
			$this->homegameRepository = $homegameRepository;
			$this->cashgameRepository = $cashgameRepository;
		}

		public function action_leaderboard($gameName, $year = null){
			$homegame = $this->homegameRepository->getByName($gameName);
			$this->userContext->requirePlayer($homegame);
			$suite = $this->cashgameRepository->getSuite($homegame, $year);
			$runningGame = $this->cashgameRepository->getRunning($homegame);
			$years = $this->cashgameRepository->getYears($homegame);
			$model = new LeaderboardModel($this->userContext->getUser(), $homegame, $suite, $years, $year, $runningGame);
			return $this->view('app/Cashgame/Leaderboard/Leaderboard', $model);
		}

	}

}