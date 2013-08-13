namespace app\User\Add{

	use Mishiin\Request;
	use core\PageController;
	use core\PageModel;
	use app\Urls\UserAddConfirmationUrlModel;
	use core\UserContext;
	use Infrastructure\Data\Interfaces\UserStorage;
	use app\User\Add\RegistrationConfirmationSender;
	use app\User\PasswordGenerator;
	use app\User\UserValidatorFactory;
	use app\User\Encryption;
	use app\User\UserFactory;
	use app\User\Add\UserAddModel;
	use app\User\SaltGenerator;
	use entities\Role;

	class UserAddController extends PageController {

		private $userContext;
		private $userStorage;
		private $registrationConfirmationSender;
		private $passwordGenerator;
		private $userValidatorFactory;
		private $encryption;
		private $userFactory;
		private $saltGenerator;
		private $request;

		public function __construct(UserContext $userContext,
									UserStorage $userStorage,
									RegistrationConfirmationSender $registrationConfirmationSender,
									PasswordGenerator $passwordGenerator,
									UserValidatorFactory $userValidatorFactory,
									Encryption $encryption,
									UserFactory $userFactory,
									SaltGenerator $saltGenerator,
									Request $request){
			$this->userContext = $userContext;
			$this->userStorage = $userStorage;
			$this->registrationConfirmationSender = $registrationConfirmationSender;
			$this->passwordGenerator = $passwordGenerator;
			$this->userValidatorFactory = $userValidatorFactory;
			$this->encryption = $encryption;
			$this->userFactory = $userFactory;
			$this->saltGenerator = $saltGenerator;
			$this->request = $request;
		}

		public function action_add(){
			return $this->showForm();
		}

		public function action_add_post(){
			$userName = $this->request->getParamPost('username');
			$displayName = $this->request->getParamPost('displayname');
			$email = $this->request->getParamPost('email');
			$realName = '';
			$user = $this->userFactory->createUser(null, $userName, $displayName, $realName, $email, Role::$player);
			$validator = $this->userValidatorFactory->getAddUserValidator($user);
			if($validator->isValid()){
				$password = $this->passwordGenerator->createPassword();
				$salt = $this->saltGenerator->createSalt();
				$encryptedPassword = $this->encryption->encrypt($password, $salt);
				$this->userStorage->addUser($user);
				$token = $this->encryption->encrypt($userName, $salt);
				$this->userStorage->setToken($user, $token);
				$this->userStorage->setEncryptedPassword($user, $encryptedPassword);
				$this->userStorage->setSalt($user, $salt);
				$this->registrationConfirmationSender->send($user, $password);
				return $this->redirect(new UserAddConfirmationUrlModel());
			} else {
				return $this->showForm($validator->getErrors());
			}
		}

		public function action_created(){
			$model = new PageModel($this->userContext->getUser());
			return $this->view('app/User/Add/Confirmation', $model);
		}

		private function showForm(array $validationErrors = null){
			$model = new PageModel($this->userContext->getUser());
			if($validationErrors != null){
				$model->setValidationErrors($validationErrors);
			}
			return $this->view('app/User/Add/Add', $model);
		}

	}

}