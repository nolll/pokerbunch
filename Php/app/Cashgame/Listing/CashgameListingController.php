<?php
namespace app\Cashgame\Listing{

	use Domain\Interfaces\HomegameRepository;
	use Domain\Interfaces\CashgameRepository;
	use core\PageController;
	use core\UserContext;

	class CashgameListingController extends PageController {

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

		public function action_listing($gameName, $year = null){
			$homegame = $this->homegameRepository->getByName($gameName);
			$this->userContext->requirePlayer($homegame);
			$games = $this->cashgameRepository->getAll($homegame, $year);
			$runningGame = $this->cashgameRepository->getRunning($homegame);
			$years = $this->cashgameRepository->getYears($homegame);
			$model = new CashgameListingModel($this->userContext->getUser(), $homegame, $games, $years, $year, $runningGame);
			return $this->view('app/Cashgame/Listing/Listing', $model);
		}

	}

}