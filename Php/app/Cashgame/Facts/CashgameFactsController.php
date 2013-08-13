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
			$this->userContext = $userContext;
			$this->homegameRepository = $homegameRepository;
			$this->cashgameRepository = $cashgameRepository;
		}

		public function action_facts($gameName, $year = null){
			$homegame = $this->homegameRepository->getByName($gameName);
			$this->userContext->requirePlayer($homegame);
			$suite = $this->cashgameRepository->getSuite($homegame, $year);
			$runningGame = $this->cashgameRepository->getRunning($homegame);
			$years = $this->cashgameRepository->getYears($homegame);
			$model = new CashgameFactsModel($this->userContext->getUser(), $homegame, $suite, $years, $year, $runningGame);
			return $this->view('app/Cashgame/Facts/Facts', $model);
		}

	}

}