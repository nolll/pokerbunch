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
			$this->userContext = $userContext;
			$this->userStorage = $userStorage;
			$this->userValidatorFactory = $userValidatorFactory;
			$this->request = $request;
		}

		public function action_edit($userName){
			$this->userContext->requireUser();
			$user = $this->userStorage->getUserByName($userName);
			if($user == null){
				throw new UserNotFoundException();
			}
			return $this->showForm($user);
		}

		public function action_edit_post($userName){
			$this->userContext->requireUser();
			$user = $this->getPostedUser($userName);
			$validator = $this->userValidatorFactory->getEditUserValidator($user);
			if($validator->isValid()){
				$this->userStorage->updateUser($user);
				return $this->redirect(new UserDetailsUrlModel($user));
			} else {
				return $this->showForm($user, $validator->getErrors());
			}
		}

		public function getPostedUser($userName){
			$user = $this->userStorage->getUserByName($userName);
			$user->setDisplayName($this->request->getParamPost('displayname'));
			$user->setRealName($this->request->getParamPost('realname'));
			$user->setEmail($this->request->getParamPost('email'));
			return $user;
		}

		private function showForm(User $user, array $validationErrors = null){
			$currentUser = $this->userContext->getUser();
			$model = new UserEditModel($currentUser, $user);
			if($validationErrors != null){
				$model->setValidationErrors($validationErrors);
			}
			return $this->view('app/User/Edit/Edit', $model);
		}

	}

}