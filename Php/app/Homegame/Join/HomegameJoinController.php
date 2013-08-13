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
			$this->userContext = $userContext;
			$this->homegameRepository = $homegameRepository;
			$this->playerRepository = $playerRepository;
			$this->invitationCodeCreator = $invitationCodeCreator;
			$this->request = $request;
		}

		public function action_join($gameName){
			$this->userContext->requireUser();
			return $this->showForm();
		}

		public function action_join_post($gameName){
			$this->userContext->requireUser();
			$homegame = $this->homegameRepository->getByName($gameName);
			$postedCode = $this->request->getParamPost('invitationcode');
			$player = $this->getMatchedPlayer($homegame, $postedCode);
			if($player != null && $player->getUserName() == null){
				$user = $this->userContext->getUser();
				$this->playerRepository->joinHomegame($player, $homegame, $user);
				return $this->redirect(new HomegameJoinConfirmationUrlModel($homegame));
			} else {
				$error = 'That code didn\'t work. Please check for errors and try again';
				return $this->showForm($postedCode, $error);
			}
		}

		public function action_joined($gameName){
			$model = new PageModel($this->userContext->getUser());
			return $this->view('app/Homegame/Join/Confirmation', $model);
		}

		/**
		 * @param Homegame $homegame
		 * @param $postedCode
		 * @return Player
		 */
		private function getMatchedPlayer(Homegame $homegame, $postedCode){
			$players = $this->playerRepository->getAll($homegame);
			foreach ($players as $player) {
				$playerCode = $this->invitationCodeCreator->getCode($player);
				if($playerCode == $postedCode){
					return $player;
				}
			}
			return null;
		}

		private function showForm($postedCode = '', $error = null){
			$model = new HomegameJoinModel($this->userContext->getUser(), $postedCode);
			if($error != null){
				$model->setValidationErrors(array($error));
			}
			return $this->view('app/Homegame/Join/Join', $model);
		}

	}

}