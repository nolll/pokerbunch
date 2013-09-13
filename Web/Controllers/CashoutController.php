namespace app\Cashgame\Action{

	use app\Cashgame\CashgameValidatorFactory;
	use entities\Cashgame;
	use Domain\Classes\User;
	use entities\Homegame;
	use entities\Player;
	use Mishiin\Request;
	use core\PageController;
	use core\UserContext;
	use entities\Checkpoints\CashoutCheckpoint;
	use app\Urls\RunningCashgameUrlModel;
	use core\DateTimeFactory;
	use exceptions\AccessDeniedException;
	use Domain\Interfaces\HomegameRepository;
	use Domain\Interfaces\CashgameRepository;
	use Domain\Interfaces\PlayerRepository;

	class CashoutController extends PageController {

		private $userContext;
		private $homegameRepository;
		private $cashgameRepository;
		private $playerRepository;
		private $request;
		private $cashgameValidatorFactory;

		/** @var Homegame */
		private $homegame;
		/** @var Player */
		private $player;

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

		public function action_cashout($gameName, $playerName){
			homegame = homegameRepository.getByName($gameName);
			player = playerRepository.getByName(homegame, $playerName);
			userContext.requirePlayer(homegame);
			$user = userContext.getUser();
			if(!userContext.isAdmin() && player.getUserName() != $user.getUserName()){
				throw new AccessDeniedException();
			}
			$runningGame = cashgameRepository.getRunning(homegame);
			return showForm($runningGame, $user);
		}

		private function showForm(Cashgame $runningGame, User $user, $postedAmount = null, array $errors = null){
			$years = cashgameRepository.getYears(homegame);
			$model = new CashoutModel($user, homegame, player, $years, $runningGame, $postedAmount);
			if($errors != null){
				$model.setValidationErrors($errors);
			}
			return view('app/Cashgame/Action/Cashout', $model);
		}

		public function action_cashout_post($gameName, $playerName){
			homegame = homegameRepository.getByName($gameName);
			player = playerRepository.getByName(homegame, $playerName);
			userContext.requirePlayer(homegame);
			$user = userContext.getUser();
			if(!userContext.isAdmin() && player.getUserName() != $user.getUserName()){
				throw new AccessDeniedException();
			}
			$postModel = new ActionPostModel(request);
			$postedCheckpoint = getCashoutCheckpoint($postModel);
			$runningGame = cashgameRepository.getRunning(homegame);
			$result = $runningGame.getResult(player);
			$validator = cashgameValidatorFactory.getCashoutValidator($postModel);
			if($validator.isValid()){
				$existingCheckpoint = $result.getCashoutCheckpoint();
				if($existingCheckpoint != null){
					$existingCheckpoint.setStack($postedCheckpoint.getStack());
					cashgameRepository.updateCheckpoint($existingCheckpoint);
				} else {
					cashgameRepository.addCheckpoint($runningGame, player, $postedCheckpoint);
				}
			} else {
				return showForm($runningGame, $user, $postModel.stack, $validator.getErrors());
			}
			$runningUrl = new RunningCashgameUrlModel(homegame);
			return redirect($runningUrl);
		}

		private function getCashoutCheckpoint(ActionPostModel $postModel){
			$timestamp = DateTimeFactory::now(homegame.getTimezone());
			return new CashoutCheckpoint($timestamp, $postModel.stack);
		}

	}

}