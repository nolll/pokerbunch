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
			userContext = $userContext;
			userStorage = $userStorage;
			userValidatorFactory = $userValidatorFactory;
			encryption = $encryption;
			saltGenerator = $saltGenerator;
			request = $request;
		}

		public function action_change(){
			userContext.requireUser();
			return showForm();
		}

		public function action_change_post(){
			userContext.requireUser();
			$user = userContext.getUser();
			$password = request.getParamPost('password');
			$repeatPassword = request.getParamPost('repeat');
			$validator = userValidatorFactory.getChangePasswordValidator($password, $repeatPassword);
			if($validator.isValid()){
				$salt = saltGenerator.createSalt();
				$encryptedPassword = encryption.encrypt($password, $salt);
				userStorage.setEncryptedPassword($user, $encryptedPassword);
				userStorage.setSalt($user, $salt);
				return redirect(new ChangePasswordConfirmationUrlModel());
			} else {
				return showForm($validator.getErrors());
			}
		}

		public function action_changed(){
			$model = new PageModel(userContext.getUser());
			view('app/User/ChangePassword/Confirmation', $model);
		}

		private function showForm(array $validationErrors = null){
			$model = new PageModel(userContext.getUser());
			if($validationErrors != null){
				$model.setValidationErrors($validationErrors);
			}
			return view('app/User/ChangePassword/Form', $model);
		}

	}

}