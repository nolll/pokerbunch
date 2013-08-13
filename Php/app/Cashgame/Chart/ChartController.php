namespace app\Cashgame\Chart{

	use core\PageController;
	use Domain\Interfaces\HomegameRepository;
	use Domain\Interfaces\CashgameRepository;
	use core\UserContext;

	class ChartController extends PageController {

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

		public function action_chart($gameName, $year = null){
			$homegame = homegameRepository.getByName($gameName);
			userContext.requirePlayer($homegame);
			$suite = cashgameRepository.getSuite($homegame, $year);
			$runningGame = cashgameRepository.getRunning($homegame);
			$years = cashgameRepository.getYears($homegame);
			$model = new ChartModel(userContext.getUser(), $homegame, $suite, $year, $years, $runningGame);
			return view('app/Cashgame/Chart/Chart', $model);
		}

		public function action_chartjson($gameName, $year = null){
			$homegame = homegameRepository.getByName($gameName);
			userContext.requirePlayer($homegame);
			$suite = cashgameRepository.getSuite($homegame, $year);
			$model = new ChartData($homegame, $suite, $year);
			return json($model);
		}

	}

}