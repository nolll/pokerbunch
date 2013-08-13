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
			userContext = $userContext;
			userStorage = $userStorage;
			registrationConfirmationSender = $registrationConfirmationSender;
			passwordGenerator = $passwordGenerator;
			userValidatorFactory = $userValidatorFactory;
			encryption = $encryption;
			userFactory = $userFactory;
			saltGenerator = $saltGenerator;
			request = $request;
		}

		public function action_add(){
			return showForm();
		}

		public function action_add_post(){
			$userName = request.getParamPost('username');
			$displayName = request.getParamPost('displayname');
			$email = request.getParamPost('email');
			$realName = '';
			$user = userFactory.createUser(null, $userName, $displayName, $realName, $email, Role::$player);
			$validator = userValidatorFactory.getAddUserValidator($user);
			if($validator.isValid()){
				$password = passwordGenerator.createPassword();
				$salt = saltGenerator.createSalt();
				$encryptedPassword = encryption.encrypt($password, $salt);
				userStorage.addUser($user);
				$token = encryption.encrypt($userName, $salt);
				userStorage.setToken($user, $token);
				userStorage.setEncryptedPassword($user, $encryptedPassword);
				userStorage.setSalt($user, $salt);
				registrationConfirmationSender.send($user, $password);
				return redirect(new UserAddConfirmationUrlModel());
			} else {
				return showForm($validator.getErrors());
			}
		}

		public function action_created(){
			$model = new PageModel(userContext.getUser());
			return view('app/User/Add/Confirmation', $model);
		}

		private function showForm(array $validationErrors = null){
			$model = new PageModel(userContext.getUser());
			if($validationErrors != null){
				$model.setValidationErrors($validationErrors);
			}
			return view('app/User/Add/Add', $model);
		}

	}

}