<?php
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
			$this->userContext = $userContext;
			$this->homegameRepository = $homegameRepository;
			$this->playerRepository = $playerRepository;
			$this->cashgameRepository = $cashgameRepository;
			$this->playerValidatorFactory = $playerValidatorFactory;
			$this->invitationSender = $invitationSender;
			$this->request = $request;
		}

		public function action_invite($gameName, $playerName){
			$homegame = $this->homegameRepository->getByName($gameName);
			$this->userContext->requireManager($homegame);
			$player = $this->playerRepository->getByName($homegame, $playerName);
			return $this->showForm($homegame, $player);
		}

		public function action_invite_post($gameName, $playerName){
			$homegame = $this->homegameRepository->getByName($gameName);
			$this->userContext->requireManager($homegame);
			$player = $this->playerRepository->getByName($homegame, $playerName);
			$email = $this->getPostedEmail();
			$validator = $this->playerValidatorFactory->getInvitePlayerValidator($email);
			if($validator->isValid()){
				$this->invitationSender->send($homegame, $player, $email);
				return $this->redirect(new PlayerInviteConfirmationUrlModel($homegame, $player));
			} else {
				return $this->showForm($homegame, $player, $validator->getErrors());
			}
		}

		public function action_invited($gameName, $playerName){
			$homegame = $this->homegameRepository->getByName($gameName);
			$runningGame = $this->cashgameRepository->getRunning($homegame);
			$model = new HomegamePageModel($this->userContext->getUser(), $homegame, $runningGame);
			return $this->view('app/Player/Invite/Confirmation', $model);
		}

		private function showForm(Homegame $homegame, Player $player, array $validationErrors = null){
			$runningGame = $this->cashgameRepository->getRunning($homegame);
			$model = new HomegamePageModel($this->userContext->getUser(), $homegame, $runningGame);
			if($validationErrors != null){
				$model->setValidationErrors($validationErrors);
			}
			return $this->view('app/Player/Invite/invite', $model);
		}

		private function getPostedEmail(){
			return $this->request->getParamPost('email');
		}

	}

}