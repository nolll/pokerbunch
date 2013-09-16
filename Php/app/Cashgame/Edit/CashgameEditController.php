namespace app\Cashgame\Edit{

	use core\DateTimeFactory;
	use app\Urls\CashgameListingUrlModel;
	use app\Urls\CashgameDetailsUrlModel;
	use core\Globalization;
	use Domain\Interfaces\CashgameRepository;
	use Domain\Interfaces\HomegameRepository;
	use app\Cashgame\CashgameValidatorFactory;
	use core\PageController;
	use core\UserContext;

	class CashgameEditController extends PageController {

		private $userContext;
		private $homegameRepository;
		private $cashgameRepository;
		private $cashgameValidatorFactory;
		private $editPostModel;

		public function __construct(UserContext $userContext,
									HomegameRepository $homegameRepository,
									CashgameRepository $cashgameRepository,
									CashgameValidatorFactory $cashgameValidatorFactory,
									CashgameEditPostModel $editPostModel){
			userContext = $userContext;
			homegameRepository = $homegameRepository;
			cashgameRepository = $cashgameRepository;
			cashgameValidatorFactory = $cashgameValidatorFactory;
			editPostModel = $editPostModel;
		}

		public function action_delete($gameName, $dateStr){
			$homegame = homegameRepository.getByName($gameName);
			userContext.requireManager($homegame);
			$date = DateTimeFactory::create($dateStr, $homegame.getTimezone());
			$cashgame = cashgameRepository.getByDate($homegame, $date);
			cashgameRepository.deleteGame($cashgame);
			$year = $date.format('Y');
			$listUrl = new CashgameListingUrlModel($homegame, $year);
			return redirect($listUrl);
		}

	}

}