namespace app\Cashgame\Add{

	use entities\CashgameFactory;
	use Mishiin\Request;
	use app\Urls\RunningCashgameUrlModel;
	use core\PageController;
	use core\UserContext;
	use entities\GameStatus;
	use core\DateTimeFactory;
	use core\Globalization;
	use exceptions\AccessDeniedException;
	use Domain\Interfaces\CashgameRepository;
	use Domain\Interfaces\HomegameRepository;
	use entities\Cashgame;
	use app\Cashgame\CashgameValidatorFactory;
	use entities\Homegame;
	use app\Cashgame\Add\AddModel;

	class AddController extends PageController {

		private $userContext;
		private $homegameRepository;
		private $cashgameRepository;
		private $cashgameValidatorFactory;
		private $request;
		private $cashgameFactory;

		public function __construct(UserContext $userContext,
									HomegameRepository $homegameRepository,
									CashgameRepository $cashgameRepository,
									CashgameValidatorFactory $cashgameValidatorFactory,
									Request $request,
									CashgameFactory $cashgameFactory){
			userContext = $userContext;
			homegameRepository = $homegameRepository;
			cashgameRepository = $cashgameRepository;
			cashgameValidatorFactory = $cashgameValidatorFactory;
			request = $request;
			cashgameFactory = $cashgameFactory;
		}

        

	}

}