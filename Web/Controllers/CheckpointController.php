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
			userContext = $userContext;
			homegameRepository = $homegameRepository;
			cashgameRepository = $cashgameRepository;
			playerRepository = $playerRepository;
		}

		public function action_deletecheckpoint($gameName, $dateStr, $playerName, $checkpointId){
			$homegame = homegameRepository.getByName($gameName);
			userContext.requireManager($homegame);
			$cashgame = cashgameRepository.getByDateString($homegame, $dateStr);
			$player = playerRepository.getByName($homegame, $playerName);
			cashgameRepository.deleteCheckpoint($checkpointId);
			return redirect(new CashgameActionUrlModel($homegame, $cashgame, $player));
		}

	}

}