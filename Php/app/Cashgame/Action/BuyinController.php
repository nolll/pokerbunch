namespace app\Cashgame\Action{

	use app\Cashgame\CashgameValidatorFactory;
	use entities\Cashgame;
	use entities\Homegame;
	use entities\Player;
	use Mishiin\Request;
	use app\Urls\RunningCashgameUrlModel;
	use core\DateTimeFactory;
	use core\PageController;
	use core\UserContext;
	use entities\GameStatus;
	use exceptions\AccessDeniedException;
	use app\Cashgame\Action\BuyinPostModel;
	use entities\Checkpoints\BuyinCheckpoint;
	use Domain\Interfaces\HomegameRepository;
	use Domain\Interfaces\CashgameRepository;
	use Domain\Interfaces\PlayerRepository;

	class BuyinController extends PageController {

		private $userContext;
		private $homegameRepository;
		private $cashgameRepository;
		private $playerRepository;
		private $request;

		/** @var Homegame */
		private $homegame;
		/** @var Player */
		private $player;
		private $cashgameValidatorFactory;

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

		public function action_buyin($gameName, $playerName){
			homegame = homegameRepository.getByName($gameName);
			player = playerRepository.getByName(homegame, $playerName);
			userContext.requirePlayer(homegame);
			$runningGame = cashgameRepository.getRunning(homegame);
			return showForm($runningGame);
		}

		private function showForm(Cashgame $cashgame, $postedAmount = null, array $errors = null){
			$user = userContext.getUser();
			if(!userContext.isAdmin() && player.getUserName() != $user.getUserName()){
				throw new AccessDeniedException();
			}
			$years = cashgameRepository.getYears(homegame);
			$model = new BuyinModel($user, homegame, player, $years, $cashgame, $postedAmount);
			if($errors != null){
				$model.setValidationErrors($errors);
			}
			return view('app/Cashgame/Action/Buyin', $model);
		}

		public function action_buyin_post($gameName, $playerName){
			homegame = homegameRepository.getByName($gameName);
			player = playerRepository.getByName(homegame, $playerName);
			userContext.requirePlayer(homegame);
			$postModel = new BuyinPostModel(request);
			$runningGame = cashgameRepository.getRunning(homegame);
			$validator = cashgameValidatorFactory.getBuyinValidator($postModel);
			if($validator.isValid()){
				$checkpoint = getBuyinCheckpoint($postModel);
				cashgameRepository.addCheckpoint($runningGame, player, $checkpoint);
			} else {
				return showForm($runningGame, $postModel.amount, $validator.getErrors());
			}
			if(!$runningGame.isStarted()){
				cashgameRepository.startGame($runningGame);
			}
			$runningUrl = new RunningCashgameUrlModel(homegame, $runningGame, player);
			return redirect($runningUrl);
		}

		private function getBuyinCheckpoint(BuyinPostModel $postModel){
			$timestamp = DateTimeFactory::now(homegame.getTimezone());
			return new BuyinCheckpoint($timestamp, $postModel.stack + $postModel.amount, $postModel.amount);
		}

	}

}