<?php
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
			$this->userContext = $userContext;
			$this->homegameRepository = $homegameRepository;
			$this->cashgameRepository = $cashgameRepository;
			$this->playerRepository = $playerRepository;
			$this->request = $request;
			$this->cashgameValidatorFactory = $cashgameValidatorFactory;
		}

		public function action_buyin($gameName, $playerName){
			$this->homegame = $this->homegameRepository->getByName($gameName);
			$this->player = $this->playerRepository->getByName($this->homegame, $playerName);
			$this->userContext->requirePlayer($this->homegame);
			$runningGame = $this->cashgameRepository->getRunning($this->homegame);
			return $this->showForm($runningGame);
		}

		private function showForm(Cashgame $cashgame, $postedAmount = null, array $errors = null){
			$user = $this->userContext->getUser();
			if(!$this->userContext->isAdmin() && $this->player->getUserName() != $user->getUserName()){
				throw new AccessDeniedException();
			}
			$years = $this->cashgameRepository->getYears($this->homegame);
			$model = new BuyinModel($user, $this->homegame, $this->player, $years, $cashgame, $postedAmount);
			if($errors != null){
				$model->setValidationErrors($errors);
			}
			return $this->view('app/Cashgame/Action/Buyin', $model);
		}

		public function action_buyin_post($gameName, $playerName){
			$this->homegame = $this->homegameRepository->getByName($gameName);
			$this->player = $this->playerRepository->getByName($this->homegame, $playerName);
			$this->userContext->requirePlayer($this->homegame);
			$postModel = new BuyinPostModel($this->request);
			$runningGame = $this->cashgameRepository->getRunning($this->homegame);
			$validator = $this->cashgameValidatorFactory->getBuyinValidator($postModel);
			if($validator->isValid()){
				$checkpoint = $this->getBuyinCheckpoint($postModel);
				$this->cashgameRepository->addCheckpoint($runningGame, $this->player, $checkpoint);
			} else {
				return $this->showForm($runningGame, $postModel->amount, $validator->getErrors());
			}
			if(!$runningGame->isStarted()){
				$this->cashgameRepository->startGame($runningGame);
			}
			$runningUrl = new RunningCashgameUrlModel($this->homegame, $runningGame, $this->player);
			return $this->redirect($runningUrl);
		}

		private function getBuyinCheckpoint(BuyinPostModel $postModel){
			$timestamp = DateTimeFactory::now($this->homegame->getTimezone());
			return new BuyinCheckpoint($timestamp, $postModel->stack + $postModel->amount, $postModel->amount);
		}

	}

}