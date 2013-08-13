namespace app\User\Listing{

	use core\PageController;
	use core\UserContext;
	use Infrastructure\Data\Interfaces\UserStorage;

	class UserListingController extends PageController {

		private $userContext;
		private $userStorage;

		public function __construct(UserContext $userContext,
									UserStorage $userStorage){
			userContext = $userContext;
			userStorage = $userStorage;
		}

		public function action_listing(){
			userContext.requireAdmin();
			$users = userStorage.getUsers();
			$model = new UserListingModel(userContext.getUser(), $users);

			return view('app/User/Listing/Listing', $model);
		}

	}

}