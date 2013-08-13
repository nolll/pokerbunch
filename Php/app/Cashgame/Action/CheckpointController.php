namespace app\Cashgame\Action{

	use app\Urls\CashgameActionUrlModel;
	use core\DateTimeFactory;
	use Domain\Interfaces\HomegameRepository;
	use Domain\Interfaces\CashgameRepository;
	use Domain\Interfaces\PlayerRepository;
	use core\Globalization;
	use core\PageController;
	use core\UserContext;

	class CheckpointController extends PageController {

		private $userContext;
		private $homegameRepository;
		private $cashgameRepository;
		private $playerRepository;

		public function __construct(UserContext $userContext,
									HomegameRepository $homegameRepository,
									CashgameRepository $cashgameRepository,
									PlayerRepository $playerRepository){
			$this->userContext = $userContext;
			$this->homegameRepository = $homegameRepository;
			$this->cashgameRepository = $cashgameRepository;
			$this->playerRepository = $playerRepository;
		}

		public function action_deletecheckpoint($gameName, $dateStr, $playerName, $checkpointId){
			$homegame = $this->homegameRepository->getByName($gameName);
			$this->userContext->requireManager($homegame);
			$cashgame = $this->cashgameRepository->getByDateString($homegame, $dateStr);
			$player = $this->playerRepository->getByName($homegame, $playerName);
			$this->cashgameRepository->deleteCheckpoint($checkpointId);
			return $this->redirect(new CashgameActionUrlModel($homegame, $cashgame, $player));
		}

	}

}