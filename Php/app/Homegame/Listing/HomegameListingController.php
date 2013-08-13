namespace app\Homegame\Listing{

	use core\PageController;
	use core\UserContext;
	use Infrastructure\Data\Interfaces\HomegameStorage;

	class HomegameListingController extends PageController {

		private $userContext;
		private $homegameStorage;

		public function __construct(UserContext $userContext,
									HomegameStorage $homegameStorage){
			userContext = $userContext;
			homegameStorage = $homegameStorage;
		}

		public function action_listing(){
			userContext.requireAdmin();
			$homegames = homegameStorage.getHomegames();
			$model = new HomegameListingModel(userContext.getUser(), $homegames);
			return view('app/Homegame/Listing/Listing', $model);
		}

    }

}