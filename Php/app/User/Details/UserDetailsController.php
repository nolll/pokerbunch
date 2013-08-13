namespace app\User\Details{

	use core\PageController;
	use core\UserContext;
	use integration\Avatar\AvatarService;
	use Infrastructure\Data\Interfaces\UserStorage;
	use exceptions\UserNotFoundException;

	class UserDetailsController extends PageController {

		private $userContext;
		private $userStorage;
		private $avatarService;

		public function __construct(UserContext $userContext,
									UserStorage $userStorage,
									AvatarService $avatarService){
			userContext = $userContext;
			userStorage = $userStorage;
			avatarService = $avatarService;
		}

		public function action_details($userName){
			userContext.requireUser();
			$user = userStorage.getUserByName($userName);
			if($user == null){
				throw new UserNotFoundException();
			}
			$model = new UserDetailsModel(userContext.getUser(), $user, avatarService);
			return view('app/User/Details/Details', $model);
		}

	}

}