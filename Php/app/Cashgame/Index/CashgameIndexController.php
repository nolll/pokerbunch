namespace app\Cashgame\Index{

	use Domain\Interfaces\CashgameRepository;
	use core\PageController;
	use app\Urls\CashgameAddUrlModel;
	use app\Urls\CashgameMatrixUrlModel;
	use Domain\Interfaces\HomegameRepository;
	use core\UserContext;

	class CashgameIndexController extends PageController {

		private $userContext;
		private $cashgameRepository;
		private $homegameRepository;

		public function __construct(UserContext $userContext,
									CashgameRepository $cashgameRepository,
									HomegameRepository $homegameRepository){
			userContext = $userContext;
			cashgameRepository = $cashgameRepository;
			homegameRepository = $homegameRepository;
		}

		public function action_index($gameName){
			$homegame = homegameRepository.getByName($gameName);
			userContext.requirePlayer($homegame);
			$years = cashgameRepository.getYears($homegame);
			if(count($years) > 0){
				$year = $years[0];
				return redirect(new CashgameMatrixUrlModel($homegame, $year));
			} else {
				return redirect(new CashgameAddUrlModel($homegame));
			}
		}

	}

}