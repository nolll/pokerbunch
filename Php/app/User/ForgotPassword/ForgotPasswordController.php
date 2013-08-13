namespace app\User\ForgotPassword{

	use Mishiin\Request;
	use core\PageController;
	use core\PageModel;
	use app\Urls\ForgotPasswordConfirmationUrlModel;
	use core\UserContext;
	use Infrastructure\Data\Interfaces\UserStorage;
	use app\User\ForgotPassword\PasswordSender;
	use Domain\Classes\User;
	use app\User\PasswordGenerator;
	use app\User\UserValidatorFactory;
	use app\User\Encryption;
	use app\User\SaltGenerator;

	class ForgotPasswordController extends PageController {

		private $userContext;
		private $userStorage;
		private $passwordSender;
		private $passwordGenerator;
		private $userValidatorFactory;
		private $encryption;
		private $saltGenerator;
		private $request;

		public function __construct(UserContext $userContext,
									UserStorage $userStorage,
									PasswordSender $passwordSender,
									PasswordGenerator $passwordGenerator,
									UserValidatorFactory $userValidatorFactory,
									Encryption $encryption,
									SaltGenerator $saltGenerator,
									Request $request){
			$this->userContext = $userContext;
			$this->userStorage = $userStorage;
			$this->passwordSender = $passwordSender;
			$this->passwordGenerator = $passwordGenerator;
			$this->userValidatorFactory = $userValidatorFactory;
			$this->encryption = $encryption;
			$this->saltGenerator = $saltGenerator;
			$this->request = $request;
		}

		public function action_forgot(){
			return $this->showForm();
		}

		public function action_forgot_post(){
			$email = $this->request->getParamPost('email');
			$validator = $this->userValidatorFactory->getForgotPasswordValidator($email);
			if($validator->isValid()){
				$user = $this->userStorage->getUserByEmail($email);
				if($user != null){
					$password = $this->passwordGenerator->createPassword();
					$this->saveNewPassword($user, $password);
					$this->passwordSender->send($user, $password);
				}
				return $this->redirect(new ForgotPasswordConfirmationUrlModel());
			} else {
				return $this->showForm($validator->getErrors());
			}
		}

		public function action_sent(){
			$model = new PageModel($this->userContext->getUser());
			$this->view('app/User/ForgotPassword/Confirmation', $model);
		}

		private function saveNewPassword(User $user, $password){
			$salt = $this->saltGenerator->createSalt();
			$encryptedPassword = $this->encryption->encrypt($password, $salt);
			$this->userStorage->setEncryptedPassword($user, $encryptedPassword);
			$this->userStorage->setSalt($user, $salt);
		}

		private function showForm(array $validationErrors = null){
			$model = new PageModel($this->userContext->getUser());
			if($validationErrors != null){
				$model->setValidationErrors($validationErrors);
			}
			return $this->view('app/User/ForgotPassword/Form', $model);
		}

	}

}