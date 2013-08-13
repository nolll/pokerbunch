<?php
namespace app\Cashgame\Action{

	use core\PageController;
	use core\UserContext;
	use Domain\Classes\User;
	use entities\Homegame;
	use app\Urls\CashgameIndexUrlModel;
	use core\DateTimeFactory;
	use Domain\Interfaces\HomegameRepository;
	use Domain\Interfaces\CashgameRepository;

	class EndGameController extends PageController {

		private $userContext;
		private $homegameRepository;
		private $cashgameRepository;

		public function __construct(UserContext $userContext,
									HomegameRepository $homegameRepository,
									CashgameRepository $cashgameRepository){
			$this->userContext = $userContext;
			$this->homegameRepository = $homegameRepository;
			$this->cashgameRepository = $cashgameRepository;
		}

		public function action_end($gameName){
			$homegame = $this->homegameRepository->getByName($gameName);
			$this->userContext->requirePlayer($homegame);
			$user = $this->userContext->getUser();
			return $this->renderEndGame($user, $homegame);
		}

		public function renderEndGame(User $user, Homegame $homegame){
			$runningGame = $this->cashgameRepository->getRunning($homegame);
			$years = $this->cashgameRepository->getYears($homegame);
			$model = new EndGameModel($user, $homegame, $years, $runningGame);
			return $this->view('app/Cashgame/Action/EndGame', $model);
		}

		public function action_end_post($gameName){
			$homegame = $this->homegameRepository->getByName($gameName);
			$cashgame = $this->cashgameRepository->getRunning($homegame);
			$this->userContext->requirePlayer($homegame);
			$this->cashgameRepository->endGame($cashgame);
			$indexUrl = new CashgameIndexUrlModel($homegame);
			return $this->redirect($indexUrl);
		}

	}

}