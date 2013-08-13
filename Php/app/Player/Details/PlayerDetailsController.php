<?php
namespace app\Player\Details{

	use app\Urls\PlayerDetailsUrlModel;
	use core\PageController;
	use app\Player\Facts\PlayerFactsModel;
	use app\User\Avatar\AvatarModelBuilder;
	use app\Urls\PlayerIndexUrlModel;
	use Domain\Interfaces\HomegameRepository;
	use Domain\Interfaces\CashgameRepository;
	use Domain\Interfaces\PlayerRepository;
	use core\UserContext;
	use Infrastructure\Data\Interfaces\UserStorage;
	use app\Player\Details\PlayerDetailsModel;
	use entities\Role;

	class PlayerDetailsController extends PageController {

		private $userContext;
		private $homegameRepository;
		private $cashgameRepository;
		private $playerRepository;
		private $userStorage;
		private $avatarModelBuilder;

		public function __construct(UserContext $userContext,
									HomegameRepository $homegameRepository,
									CashgameRepository $cashgameRepository,
									PlayerRepository $playerRepository,
									UserStorage $userStorage,
									AvatarModelBuilder $avatarModelBuilder){
			$this->userContext = $userContext;
			$this->homegameRepository = $homegameRepository;
			$this->cashgameRepository = $cashgameRepository;
			$this->playerRepository = $playerRepository;
			$this->userStorage = $userStorage;
			$this->avatarModelBuilder = $avatarModelBuilder;
		}

		public function action_details($gameName, $playerName){
			$homegame = $this->homegameRepository->getByName($gameName);
			$this->userContext->requirePlayer($homegame);
			$currentUser = $this->userContext->getUser();
			$player = $this->playerRepository->getByName($homegame, $playerName);
			$user = $this->userStorage->getUserByName($player->getUserName());
			$cashgames = $this->cashgameRepository->getPublished($homegame);
			$isManager = $this->userContext->isInRole($homegame, Role::$manager);
			$hasPlayed = $this->cashgameRepository->hasPlayed($player);
			$runningGame = $this->cashgameRepository->getRunning($homegame);
			$model = new PlayerDetailsModel($currentUser, $homegame, $player, $user, $cashgames, $isManager, $hasPlayed, $this->avatarModelBuilder, $runningGame);
			return $this->view('app/Player/Details/Details', $model);
		}

		public function action_delete($gameName, $playerName){
			$homegame = $this->homegameRepository->getByName($gameName);
			$this->userContext->requireManager($homegame);
			$player = $this->playerRepository->getByName($homegame, $playerName);
			$hasPlayed = $this->cashgameRepository->hasPlayed($player);
			if($hasPlayed){
				return $this->redirect(new PlayerDetailsUrlModel($homegame, $player));
			} else {
				$this->playerRepository->deletePlayer($player);
				return $this->redirect(new PlayerIndexUrlModel($homegame));
			}
		}

	}

}