namespace app\Cashgame\Listing{

	use Domain\Interfaces\HomegameRepository;
	use Domain\Interfaces\CashgameRepository;
	use core\PageController;
	use core\UserContext;

	class CashgameListingController extends PageController {

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

		public function action_listing($gameName, $year = null){
			$homegame = homegameRepository.getByName($gameName);
			userContext.requirePlayer($homegame);
			$games = cashgameRepository.getAll($homegame, $year);
			$runningGame = cashgameRepository.getRunning($homegame);
			$years = cashgameRepository.getYears($homegame);
			$model = new CashgameListingModel(userContext.getUser(), $homegame, $games, $years, $year, $runningGame);
			return view('app/Cashgame/Listing/Listing', $model);
		}

	}

}