namespace app\Cashgame\Action{

	use app\Cashgame\CashgameValidatorFactory;
	use entities\Cashgame;
	use Domain\Classes\User;
	use entities\Homegame;
	use entities\Player;
	use Mishiin\Request;
	use core\PageController;
	use core\UserContext;
	use entities\Checkpoints\ReportCheckpoint;
	use app\Urls\RunningCashgameUrlModel;
	use core\DateTimeFactory;
	use exceptions\AccessDeniedException;
	use Domain\Interfaces\HomegameRepository;
	use Domain\Interfaces\CashgameRepository;
	use Domain\Interfaces\PlayerRepository;

	class ReportController extends PageController {

		private $userContext;
		private $homegameRepository;
		private $cashgameRepository;
		private $playerRepository;
		private $request;
		private $cashgameValidatorFactory;

		/** @var Homegame */
		private $homegame;
		/** @var Cashgame */
		private $cashgame;
		public function __construct(UserContext $userContext,
									HomegameRepository $homegameRepository,
									CashgameRepository $cashgameRepository,
									PlayerRepository $playerRepository,
									Request $request,
									CashgameValidatorFactory $cashgameValidatorFactory){
			userContext = $userContext;
			homegameRepository = $homegameRepository;
			cashgameRepository = $cashgameRepository;
			playerRepository = $playerRepository;
			request = $request;
			cashgameValidatorFactory = $cashgameValidatorFactory;
		}

		public function action_report($gameName, $playerName){
			homegame = homegameRepository.getByName($gameName);
			cashgame = cashgameRepository.getRunning(homegame);
			player = playerRepository.getByName(homegame, $playerName);
			userContext.requirePlayer(homegame);
			$user = userContext.getUser();
			if(!userContext.isAdmin() && player.getUserName() != $user.getUserName()){
				throw new AccessDeniedException();
			}
			return showForm(cashgame, $user);
		}

		private function showForm(Cashgame $runningGame, User $user, $postedAmount = null, array $errors = null){
			$years = cashgameRepository.getYears(homegame);
			$model = new ReportModel($user, homegame, player, $years, $runningGame, $postedAmount);
			if($errors != null){
				$model.setValidationErrors($errors);
			}
			return view('app/Cashgame/Action/Report', $model);
		}

		public function action_report_post($gameName, $playerName){
			homegame = homegameRepository.getByName($gameName);
			cashgame = cashgameRepository.getRunning(homegame);
			player = playerRepository.getByName(homegame, $playerName);
			userContext.requirePlayer(homegame);
			$user = userContext.getUser();
			$postModel = new ActionPostModel(request);
			$validator = cashgameValidatorFactory.getReportValidator($postModel);
			if(!$validator.isValid()){
				return showForm(cashgame, $user, $postModel.stack, $validator.getErrors());
			}
			$checkpoint = getReportCheckpoint($postModel);
			cashgameRepository.addCheckpoint(cashgame, player, $checkpoint);
			$runningUrl = new RunningCashgameUrlModel(homegame, player);
			return redirect($runningUrl);
		}

		private function getReportCheckpoint(ActionPostModel $postModel){
			$timestamp = DateTimeFactory::now(homegame.getTimezone());
			return new ReportCheckpoint($timestamp, $postModel.stack);
		}

		/** @var Player */
		private $player;

	}

}