<?php
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
			$this->userContext = $userContext;
			$this->homegameRepository = $homegameRepository;
			$this->cashgameRepository = $cashgameRepository;
			$this->playerRepository = $playerRepository;
			$this->request = $request;
			$this->cashgameValidatorFactory = $cashgameValidatorFactory;
		}

		public function action_cashout($gameName, $playerName){
			$this->homegame = $this->homegameRepository->getByName($gameName);
			$this->player = $this->playerRepository->getByName($this->homegame, $playerName);
			$this->userContext->requirePlayer($this->homegame);
			$user = $this->userContext->getUser();
			if(!$this->userContext->isAdmin() && $this->player->getUserName() != $user->getUserName()){
				throw new AccessDeniedException();
			}
			$runningGame = $this->cashgameRepository->getRunning($this->homegame);
			return $this->showForm($runningGame, $user);
		}

		private function showForm(Cashgame $runningGame, User $user, $postedAmount = null, array $errors = null){
			$years = $this->cashgameRepository->getYears($this->homegame);
			$model = new CashoutModel($user, $this->homegame, $this->player, $years, $runningGame, $postedAmount);
			if($errors != null){
				$model->setValidationErrors($errors);
			}
			return $this->view('app/Cashgame/Action/Cashout', $model);
		}

		public function action_cashout_post($gameName, $playerName){
			$this->homegame = $this->homegameRepository->getByName($gameName);
			$this->player = $this->playerRepository->getByName($this->homegame, $playerName);
			$this->userContext->requirePlayer($this->homegame);
			$user = $this->userContext->getUser();
			if(!$this->userContext->isAdmin() && $this->player->getUserName() != $user->getUserName()){
				throw new AccessDeniedException();
			}
			$postModel = new ActionPostModel($this->request);
			$postedCheckpoint = $this->getCashoutCheckpoint($postModel);
			$runningGame = $this->cashgameRepository->getRunning($this->homegame);
			$result = $runningGame->getResult($this->player);
			$validator = $this->cashgameValidatorFactory->getCashoutValidator($postModel);
			if($validator->isValid()){
				$existingCheckpoint = $result->getCashoutCheckpoint();
				if($existingCheckpoint != null){
					$existingCheckpoint->setStack($postedCheckpoint->getStack());
					$this->cashgameRepository->updateCheckpoint($existingCheckpoint);
				} else {
					$this->cashgameRepository->addCheckpoint($runningGame, $this->player, $postedCheckpoint);
				}
			} else {
				return $this->showForm($runningGame, $user, $postModel->stack, $validator->getErrors());
			}
			$runningUrl = new RunningCashgameUrlModel($this->homegame);
			return $this->redirect($runningUrl);
		}

		private function getCashoutCheckpoint(ActionPostModel $postModel){
			$timestamp = DateTimeFactory::now($this->homegame->getTimezone());
			return new CashoutCheckpoint($timestamp, $postModel->stack);
		}

	}

}