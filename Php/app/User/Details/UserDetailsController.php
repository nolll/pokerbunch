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
			$this->userContext = $userContext;
			$this->userStorage = $userStorage;
			$this->avatarService = $avatarService;
		}

		public function action_details($userName){
			$this->userContext->requireUser();
			$user = $this->userStorage->getUserByName($userName);
			if($user == null){
				throw new UserNotFoundException();
			}
			$model = new UserDetailsModel($this->userContext->getUser(), $user, $this->avatarService);
			return $this->view('app/User/Details/Details', $model);
		}

	}

}