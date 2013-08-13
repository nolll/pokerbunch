<?php
namespace app\Cashgame\Running{

	use core\PageController;
	use app\Urls\CashgameIndexUrlModel;
	use core\DateTimeFactory;
	use core\Globalization;
	use core\Timer;
	use core\UserContext;
	use entities\Player;
	use Domain\Interfaces\CashgameRepository;
	use Domain\Interfaces\PlayerRepository;
	use Domain\Interfaces\HomegameRepository;
	use entities\Homegame;
	use entities\Cashgame;
	use entities\Role;
	use integration\Sharing\ResultSharer;

	class RunningController extends PageController {

		private $userContext;
		private $homegameRepository;
		private $cashgameRepository;
		private $playerRepository;
		private $resultSharer;
		private $timer;

		public function __construct(UserContext $userContext,
									HomegameRepository $homegameRepository,
									CashgameRepository $cashgameRepository,
									PlayerRepository $playerRepository,
									ResultSharer $resultSharer,
									Timer $timer){
			$this->userContext = $userContext;
			$this->homegameRepository = $homegameRepository;
			$this->cashgameRepository = $cashgameRepository;
			$this->resultSharer = $resultSharer;
			$this->playerRepository = $playerRepository;
			$this->timer = $timer;
		}

		public function action_running($gameName){
			$homegame = $this->homegameRepository->getByName($gameName);
			$this->userContext->requirePlayer($homegame);
			$cashgame = $this->cashgameRepository->getRunning($homegame);
			if($cashgame == null){
				return $this->redirect(new CashgameIndexUrlModel($homegame));
			}
			$user = $this->userContext->getUser();
			$player = $this->playerRepository->getByUserName($homegame, $user->getUserName());
			$model = $this->getModel($homegame, $cashgame, $player);
			return $this->view('app/Cashgame/Running/Running', $model);
		}

		public function getModel(Homegame $homegame, Cashgame $cashgame, Player $player){
			$isManager = $this->userContext->isInRole($homegame, Role::$manager);
			$runningGame = $this->cashgameRepository->getRunning($homegame);
			$years = $this->cashgameRepository->getYears($homegame);
			return new RunningModel($this->userContext->getUser(), $homegame, $cashgame, $player, $years, $isManager, $this->timer, $runningGame);
		}

	}

}