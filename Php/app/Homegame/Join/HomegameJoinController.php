namespace app\Homegame\Join{

	use entities\Player;
	use Mishiin\Request;
	use core\PageController;
	use core\PageModel;
	use app\Urls\HomegameJoinConfirmationUrlModel;
	use Domain\Interfaces\HomegameRepository;
	use Domain\Interfaces\PlayerRepository;
	use core\UserContext;
	use entities\Homegame;
	use app\Player\InvitationCodeCreator;

	class HomegameJoinController extends PageController {

		private $userContext;
		private $homegameRepository;
		private $playerRepository;
		private $invitationCodeCreator;
		private $request;

		public function __construct(UserContext $userContext,
									HomegameRepository $homegameRepository,
									PlayerRepository $playerRepository,
									InvitationCodeCreator $invitationCodeCreator,
									Request $request){
			userContext = $userContext;
			homegameRepository = $homegameRepository;
			playerRepository = $playerRepository;
			invitationCodeCreator = $invitationCodeCreator;
			request = $request;
		}

		public function action_join($gameName){
			userContext.requireUser();
			return showForm();
		}

		public function action_join_post($gameName){
			userContext.requireUser();
			$homegame = homegameRepository.getByName($gameName);
			$postedCode = request.getParamPost('invitationcode');
			$player = getMatchedPlayer($homegame, $postedCode);
			if($player != null && $player.getUserName() == null){
				$user = userContext.getUser();
				playerRepository.joinHomegame($player, $homegame, $user);
				return redirect(new HomegameJoinConfirmationUrlModel($homegame));
			} else {
				$error = 'That code didn\'t work. Please check for errors and try again';
				return showForm($postedCode, $error);
			}
		}

		public function action_joined($gameName){
			$model = new PageModel(userContext.getUser());
			return view('app/Homegame/Join/Confirmation', $model);
		}

		/**
		 * @param Homegame $homegame
		 * @param $postedCode
		 * @return Player
		 */
		private function getMatchedPlayer(Homegame $homegame, $postedCode){
			$players = playerRepository.getAll($homegame);
			foreach ($players as $player) {
				$playerCode = invitationCodeCreator.getCode($player);
				if($playerCode == $postedCode){
					return $player;
				}
			}
			return null;
		}

		private function showForm($postedCode = '', $error = null){
			$model = new HomegameJoinModel(userContext.getUser(), $postedCode);
			if($error != null){
				$model.setValidationErrors(array($error));
			}
			return view('app/Homegame/Join/Join', $model);
		}

	}

}