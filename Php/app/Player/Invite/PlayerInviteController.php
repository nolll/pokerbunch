namespace app\Player\Invite{

	use core\HomegamePageModel;
	use Domain\Interfaces\CashgameRepository;
	use Mishiin\Request;
	use core\PageController;
	use app\Urls\PlayerInviteConfirmationUrlModel;
	use Domain\Interfaces\HomegameRepository;
	use core\UserContext;
	use entities\Homegame;
	use entities\Player;
	use app\Player\PlayerValidatorFactory;
	use app\Player\InvitationSender;
	use Domain\Interfaces\PlayerRepository;

	class PlayerInviteController extends PageController {

		private $userContext;
		private $homegameRepository;
		private $playerRepository;
		private $cashgameRepository;
		private $playerValidatorFactory;
		private $invitationSender;
		private $request;

		public function __construct(UserContext $userContext,
									HomegameRepository $homegameRepository,
									PlayerRepository $playerRepository,
									CashgameRepository $cashgameRepository,
									PlayerValidatorFactory $playerValidatorFactory,
									InvitationSender $invitationSender,
									Request $request){
			userContext = $userContext;
			homegameRepository = $homegameRepository;
			playerRepository = $playerRepository;
			cashgameRepository = $cashgameRepository;
			playerValidatorFactory = $playerValidatorFactory;
			invitationSender = $invitationSender;
			request = $request;
		}

		public function action_invite($gameName, $playerName){
			$homegame = homegameRepository.getByName($gameName);
			userContext.requireManager($homegame);
			$player = playerRepository.getByName($homegame, $playerName);
			return showForm($homegame, $player);
		}

		public function action_invite_post($gameName, $playerName){
			$homegame = homegameRepository.getByName($gameName);
			userContext.requireManager($homegame);
			$player = playerRepository.getByName($homegame, $playerName);
			$email = getPostedEmail();
			$validator = playerValidatorFactory.getInvitePlayerValidator($email);
			if($validator.isValid()){
				invitationSender.send($homegame, $player, $email);
				return redirect(new PlayerInviteConfirmationUrlModel($homegame, $player));
			} else {
				return showForm($homegame, $player, $validator.getErrors());
			}
		}

		public function action_invited($gameName, $playerName){
			$homegame = homegameRepository.getByName($gameName);
			$runningGame = cashgameRepository.getRunning($homegame);
			$model = new HomegamePageModel(userContext.getUser(), $homegame, $runningGame);
			return view('app/Player/Invite/Confirmation', $model);
		}

		private function showForm(Homegame $homegame, Player $player, array $validationErrors = null){
			$runningGame = cashgameRepository.getRunning($homegame);
			$model = new HomegamePageModel(userContext.getUser(), $homegame, $runningGame);
			if($validationErrors != null){
				$model.setValidationErrors($validationErrors);
			}
			return view('app/Player/Invite/invite', $model);
		}

		private function getPostedEmail(){
			return request.getParamPost('email');
		}

	}

}