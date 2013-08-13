namespace app\Cashgame\Facts{

	use Domain\Interfaces\HomegameRepository;
	use Domain\Interfaces\CashgameRepository;
	use core\PageController;
	use core\UserContext;

	class CashgameFactsController extends PageController {

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

		public function action_facts($gameName, $year = null){
			$homegame = homegameRepository.getByName($gameName);
			userContext.requirePlayer($homegame);
			$suite = cashgameRepository.getSuite($homegame, $year);
			$runningGame = cashgameRepository.getRunning($homegame);
			$years = cashgameRepository.getYears($homegame);
			$model = new CashgameFactsModel(userContext.getUser(), $homegame, $suite, $years, $year, $runningGame);
			return view('app/Cashgame/Facts/Facts', $model);
		}

	}

}