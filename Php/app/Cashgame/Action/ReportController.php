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
			$this->userContext = $userContext;
			$this->homegameRepository = $homegameRepository;
			$this->cashgameRepository = $cashgameRepository;
			$this->playerRepository = $playerRepository;
			$this->request = $request;
			$this->cashgameValidatorFactory = $cashgameValidatorFactory;
		}

		public function action_report($gameName, $playerName){
			$this->homegame = $this->homegameRepository->getByName($gameName);
			$this->cashgame = $this->cashgameRepository->getRunning($this->homegame);
			$this->player = $this->playerRepository->getByName($this->homegame, $playerName);
			$this->userContext->requirePlayer($this->homegame);
			$user = $this->userContext->getUser();
			if(!$this->userContext->isAdmin() && $this->player->getUserName() != $user->getUserName()){
				throw new AccessDeniedException();
			}
			return $this->showForm($this->cashgame, $user);
		}

		private function showForm(Cashgame $runningGame, User $user, $postedAmount = null, array $errors = null){
			$years = $this->cashgameRepository->getYears($this->homegame);
			$model = new ReportModel($user, $this->homegame, $this->player, $years, $runningGame, $postedAmount);
			if($errors != null){
				$model->setValidationErrors($errors);
			}
			return $this->view('app/Cashgame/Action/Report', $model);
		}

		public function action_report_post($gameName, $playerName){
			$this->homegame = $this->homegameRepository->getByName($gameName);
			$this->cashgame = $this->cashgameRepository->getRunning($this->homegame);
			$this->player = $this->playerRepository->getByName($this->homegame, $playerName);
			$this->userContext->requirePlayer($this->homegame);
			$user = $this->userContext->getUser();
			$postModel = new ActionPostModel($this->request);
			$validator = $this->cashgameValidatorFactory->getReportValidator($postModel);
			if(!$validator->isValid()){
				return $this->showForm($this->cashgame, $user, $postModel->stack, $validator->getErrors());
			}
			$checkpoint = $this->getReportCheckpoint($postModel);
			$this->cashgameRepository->addCheckpoint($this->cashgame, $this->player, $checkpoint);
			$runningUrl = new RunningCashgameUrlModel($this->homegame, $this->player);
			return $this->redirect($runningUrl);
		}

		private function getReportCheckpoint(ActionPostModel $postModel){
			$timestamp = DateTimeFactory::now($this->homegame->getTimezone());
			return new ReportCheckpoint($timestamp, $postModel->stack);
		}

		/** @var Player */
		private $player;

	}

}