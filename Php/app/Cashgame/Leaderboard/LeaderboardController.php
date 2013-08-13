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
			userContext = $userContext;
			homegameRepository = $homegameRepository;
			cashgameRepository = $cashgameRepository;
		}

		public function action_leaderboard($gameName, $year = null){
			$homegame = homegameRepository.getByName($gameName);
			userContext.requirePlayer($homegame);
			$suite = cashgameRepository.getSuite($homegame, $year);
			$runningGame = cashgameRepository.getRunning($homegame);
			$years = cashgameRepository.getYears($homegame);
			$model = new LeaderboardModel(userContext.getUser(), $homegame, $suite, $years, $year, $runningGame);
			return view('app/Cashgame/Leaderboard/Leaderboard', $model);
		}

	}

}