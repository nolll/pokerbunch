<?php
namespace app\Cashgame\Details{

	use app\Cashgame\Action\GameChartData;
	use core\PageController;
	use app\Error\HttpNotFoundError;
	use core\UserContext;
	use Domain\Classes\User;
	use core\DateTimeFactory;
	use core\Globalization;
	use Domain\Interfaces\CashgameRepository;
	use Domain\Interfaces\HomegameRepository;
	use entities\Homegame;
	use entities\Cashgame;
	use entities\Role;
	use DateTime;
	use integration\Sharing\ResultSharer;
	use Domain\Interfaces\PlayerRepository;

	class DetailsController extends PageController {

		private $userContext;
		private $homegameRepository;
		private $cashgameRepository;
		private $playerRepository;
		private $resultSharer;

		public function __construct(UserContext $userContext,
									HomegameRepository $homegameRepository,
									CashgameRepository $cashgameRepository,
									PlayerRepository $playerRepository,
									ResultSharer $resultSharer){
			$this->userContext = $userContext;
			$this->homegameRepository = $homegameRepository;
			$this->cashgameRepository = $cashgameRepository;
			$this->playerRepository = $playerRepository;
			$this->resultSharer = $resultSharer;
		}

		public function action_details($gameName, $dateStr){
			$homegame = $this->homegameRepository->getByName($gameName);
			$this->userContext->requirePlayer($homegame);
			$date = DateTimeFactory::create($dateStr, $homegame->getTimezone());
			$cashgame = $this->cashgameRepository->getByDate($homegame, $date);
			if($cashgame == null){
				return $this->error(new HttpNotFoundError());
			}
			$user = $this->userContext->getUser();
			$model = $this->getModel($user, $homegame, $cashgame);
			return $this->view('app/Cashgame/Details/Details', $model);
		}

		public function action_detailschartjson($gameName, $dateStr){
			$homegame = $this->homegameRepository->getByName($gameName);
			$this->userContext->requirePlayer($homegame);
			$date = DateTimeFactory::create($dateStr, $homegame->getTimezone());
			$cashgame = $this->cashgameRepository->getByDate($homegame, $date);
			if($cashgame == null){
				return $this->error(new HttpNotFoundError());
			}
			$model = new GameChartData($homegame, $cashgame);
			return $this->json($model);
		}

		public function getModel(User $user, Homegame $homegame, Cashgame $cashgame){
			$player = $this->playerRepository->getByUserName($homegame, $user->getUserName());
			$isManager = $this->userContext->isInRole($homegame, Role::$manager);
			$runningGame = $this->cashgameRepository->getRunning($homegame);
			$years = $this->cashgameRepository->getYears($homegame);
			return new DetailsModel($user, $homegame, $cashgame, $player, $years, $isManager, $runningGame);
		}

	}

}