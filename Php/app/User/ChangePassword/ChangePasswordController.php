namespace app\User\ChangePassword{

	use Mishiin\Request;
	use core\PageController;
	use core\PageModel;
	use app\Urls\ChangePasswordConfirmationUrlModel;
	use core\UserContext;
	use Infrastructure\Data\Interfaces\UserStorage;
	use app\User\UserValidatorFactory;
	use app\User\Encryption;
	use app\User\SaltGenerator;

	class ChangePasswordController extends PageController {

		private $userContext;
		private $userStorage;
		private $userValidatorFactory;
		private $encryption;
		private $saltGenerator;
		private $request;

		public function __construct(UserContext $userContext,
									UserStorage $userStorage,
									UserValidatorFactory $userValidatorFactory,
									Encryption $encryption,
									SaltGenerator $saltGenerator,
									Request $request){
			$this->userContext = $userContext;
			$this->userStorage = $userStorage;
			$this->userValidatorFactory = $userValidatorFactory;
			$this->encryption = $encryption;
			$this->saltGenerator = $saltGenerator;
			$this->request = $request;
		}

		public function action_change(){
			$this->userContext->requireUser();
			return $this->showForm();
		}

		public function action_change_post(){
			$this->userContext->requireUser();
			$user = $this->userContext->getUser();
			$password = $this->request->getParamPost('password');
			$repeatPassword = $this->request->getParamPost('repeat');
			$validator = $this->userValidatorFactory->getChangePasswordValidator($password, $repeatPassword);
			if($validator->isValid()){
				$salt = $this->saltGenerator->createSalt();
				$encryptedPassword = $this->encryption->encrypt($password, $salt);
				$this->userStorage->setEncryptedPassword($user, $encryptedPassword);
				$this->userStorage->setSalt($user, $salt);
				return $this->redirect(new ChangePasswordConfirmationUrlModel());
			} else {
				return $this->showForm($validator->getErrors());
			}
		}

		public function action_changed(){
			$model = new PageModel($this->userContext->getUser());
			$this->view('app/User/ChangePassword/Confirmation', $model);
		}

		private function showForm(array $validationErrors = null){
			$model = new PageModel($this->userContext->getUser());
			if($validationErrors != null){
				$model->setValidationErrors($validationErrors);
			}
			return $this->view('app/User/ChangePassword/Form', $model);
		}

	}

}