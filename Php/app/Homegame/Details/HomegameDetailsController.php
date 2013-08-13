namespace app\Homegame\Details{

	use core\PageController;
	use core\UserContext;
	use Domain\Interfaces\CashgameRepository;
	use Domain\Interfaces\HomegameRepository;
	use entities\Homegame;
    use entities\Role;
	use app\Homegame\Details\HomegameDetailsModel;

	class HomegameDetailsController extends PageController {

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

		public function action_details($gameName){
			$homegame = homegameRepository.getByName($gameName);
			userContext.requirePlayer($homegame);
			$isInManagerMode = userContext.isInRole($homegame, Role::$manager);
			$model = new HomegameDetailsModel(userContext.getUser(), $homegame, $isInManagerMode);
			return view('app/Homegame/Details/Details', $model);
		}

	}

}