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
			userContext = $userContext;
			userStorage = $userStorage;
			passwordSender = $passwordSender;
			passwordGenerator = $passwordGenerator;
			userValidatorFactory = $userValidatorFactory;
			encryption = $encryption;
			saltGenerator = $saltGenerator;
			request = $request;
		}

		public function action_forgot(){
			return showForm();
		}

		public function action_forgot_post(){
			$email = request.getParamPost('email');
			$validator = userValidatorFactory.getForgotPasswordValidator($email);
			if($validator.isValid()){
				$user = userStorage.getUserByEmail($email);
				if($user != null){
					$password = passwordGenerator.createPassword();
					saveNewPassword($user, $password);
					passwordSender.send($user, $password);
				}
				return redirect(new ForgotPasswordConfirmationUrlModel());
			} else {
				return showForm($validator.getErrors());
			}
		}

		public function action_sent(){
			$model = new PageModel(userContext.getUser());
			view('app/User/ForgotPassword/Confirmation', $model);
		}

		private function saveNewPassword(User $user, $password){
			$salt = saltGenerator.createSalt();
			$encryptedPassword = encryption.encrypt($password, $salt);
			userStorage.setEncryptedPassword($user, $encryptedPassword);
			userStorage.setSalt($user, $salt);
		}

		private function showForm(array $validationErrors = null){
			$model = new PageModel(userContext.getUser());
			if($validationErrors != null){
				$model.setValidationErrors($validationErrors);
			}
			return view('app/User/ForgotPassword/Form', $model);
		}

	}

}