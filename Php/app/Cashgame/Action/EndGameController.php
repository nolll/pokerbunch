namespace app\Cashgame\Action{

	use core\PageController;
	use core\UserContext;
	use Domain\Classes\User;
	use entities\Homegame;
	use app\Urls\CashgameIndexUrlModel;
	use core\DateTimeFactory;
	use Domain\Interfaces\HomegameRepository;
	use Domain\Interfaces\CashgameRepository;

	class EndGameController extends PageController {

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

		public function action_end($gameName){
			$homegame = homegameRepository.getByName($gameName);
			userContext.requirePlayer($homegame);
			$user = userContext.getUser();
			return renderEndGame($user, $homegame);
		}

		public function renderEndGame(User $user, Homegame $homegame){
			$runningGame = cashgameRepository.getRunning($homegame);
			$years = cashgameRepository.getYears($homegame);
			$model = new EndGameModel($user, $homegame, $years, $runningGame);
			return view('app/Cashgame/Action/EndGame', $model);
		}

		public function action_end_post($gameName){
			$homegame = homegameRepository.getByName($gameName);
			$cashgame = cashgameRepository.getRunning($homegame);
			userContext.requirePlayer($homegame);
			cashgameRepository.endGame($cashgame);
			$indexUrl = new CashgameIndexUrlModel($homegame);
			return redirect($indexUrl);
		}

	}

}