namespace app\User\Edit{

	use Mishiin\Request;
	use core\PageController;
	use app\Urls\UserDetailsUrlModel;
	use core\UserContext;
	use Infrastructure\Data\Interfaces\UserStorage;
	use Domain\Classes\User;
	use app\User\UserValidatorFactory;
	use exceptions\UserNotFoundException;

	class UserEditController extends PageController {

		private $userContext;
		private $userStorage;
		private $userValidatorFactory;
		private $request;

		public function __construct(UserContext $userContext,
									UserStorage $userStorage,
									UserValidatorFactory $userValidatorFactory,
									Request $request){
			userContext = $userContext;
			userStorage = $userStorage;
			userValidatorFactory = $userValidatorFactory;
			request = $request;
		}

		public function action_edit($userName){
			userContext.requireUser();
			$user = userStorage.getUserByName($userName);
			if($user == null){
				throw new UserNotFoundException();
			}
			return showForm($user);
		}

		public function action_edit_post($userName){
			userContext.requireUser();
			$user = getPostedUser($userName);
			$validator = userValidatorFactory.getEditUserValidator($user);
			if($validator.isValid()){
				userStorage.updateUser($user);
				return redirect(new UserDetailsUrlModel($user));
			} else {
				return showForm($user, $validator.getErrors());
			}
		}

		public function getPostedUser($userName){
			$user = userStorage.getUserByName($userName);
			$user.setDisplayName(request.getParamPost('displayname'));
			$user.setRealName(request.getParamPost('realname'));
			$user.setEmail(request.getParamPost('email'));
			return $user;
		}

		private function showForm(User $user, array $validationErrors = null){
			$currentUser = userContext.getUser();
			$model = new UserEditModel($currentUser, $user);
			if($validationErrors != null){
				$model.setValidationErrors($validationErrors);
			}
			return view('app/User/Edit/Edit', $model);
		}

	}

}