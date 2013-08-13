<?php
namespace app\Home{

	use core\PageController;
	use core\UserContext;
	use Domain\Interfaces\CashgameRepository;
	use entities\Homegame;
	use entities\Role;
	use Infrastructure\Data\Interfaces\HomegameStorage;

	class HomeController extends PageController {

		private $userContext;
		private $homegameStorage;
		private $cashgameRepository;

		public function __construct(UserContext $userContext,
									HomegameStorage $homegameStorage,
									CashgameRepository $cashgameRepository) {
			$this->userContext = $userContext;
			$this->homegameStorage = $homegameStorage;
			$this->cashgameRepository = $cashgameRepository;
		}

		public function action_index(){
			$homegame = $this->getHomegame();
			$runningGame = $this->getRunningGame($homegame);
			$model = new HomeModel($this->userContext->getUser(), $homegame, $runningGame);
			return $this->view('app/Home/Home', $model);
		}

		private function getHomegame(){
			$games = $this->homegameStorage->getHomegamesByRole($this->userContext->getToken(), Role::$player);
			if(count($games) == 1){
				return $games[0];
			}
			return null;
		}

		private function getRunningGame(Homegame $homegame = null){
			if($homegame == null){
				return null;
			}
			return $this->cashgameRepository->getRunning($homegame);
		}

	}

}