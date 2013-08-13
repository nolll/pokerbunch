<?php
namespace app\Cashgame\Chart{

	use core\PageController;
	use Domain\Interfaces\HomegameRepository;
	use Domain\Interfaces\CashgameRepository;
	use core\UserContext;

	class ChartController extends PageController {

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

		public function action_chart($gameName, $year = null){
			$homegame = $this->homegameRepository->getByName($gameName);
			$this->userContext->requirePlayer($homegame);
			$suite = $this->cashgameRepository->getSuite($homegame, $year);
			$runningGame = $this->cashgameRepository->getRunning($homegame);
			$years = $this->cashgameRepository->getYears($homegame);
			$model = new ChartModel($this->userContext->getUser(), $homegame, $suite, $year, $years, $runningGame);
			return $this->view('app/Cashgame/Chart/Chart', $model);
		}

		public function action_chartjson($gameName, $year = null){
			$homegame = $this->homegameRepository->getByName($gameName);
			$this->userContext->requirePlayer($homegame);
			$suite = $this->cashgameRepository->getSuite($homegame, $year);
			$model = new ChartData($homegame, $suite, $year);
			return $this->json($model);
		}

	}

}