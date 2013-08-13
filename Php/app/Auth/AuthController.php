namespace app\Auth{

	use Mishiin\Request;
	use Mishiin\Response;
	use core\PageController;
	use app\Urls\BaseClasses\UrlModel;
	use app\Urls\HomeUrlModel;
	use Infrastructure\Data\Interfaces\UserStorage;
	use Domain\Classes\User;
	use app\User\Encryption;
	use app\User\UserValidatorFactory;

	class AuthController extends PageController {

		private $userStorage;
		private $encryption;
		private $userValidatorFactory;
		private $response;
		private $request;

		public function __construct(UserStorage $userStorage,
									Encryption $encryption,
									UserValidatorFactory $userValidatorFactory,
									Response $response,
									Request $request){
			userStorage = $userStorage;
			encryption = $encryption;
			userValidatorFactory = $userValidatorFactory;
			response = $response;
			request = $request;
		}

		public function action_login(){
			return showForm();
		}

		public function action_login_post(){
			$loginName = request.getParamPost('ln');
			$password = request.getParamPost('pw');

			$user = getLoggedInUser($loginName, $password);

			$validator = userValidatorFactory.getLoginValidator($user);
			if($validator.isValid()){
				$remember = request.getParamPost('remember');
				$returnUrl = request.getParamPost('return');
				setCookies($user, $remember);
				return redirect(getReturnUrl($returnUrl));
			} else {
				return showForm($loginName, $validator.getErrors());
			}
		}

		private function getLoggedInUser($loginName, $password){
			$salt = userStorage.getSalt($loginName);
			$encryptedPassword = encryption.encrypt($password, $salt);
			return userStorage.getUserByCredentials($loginName, $encryptedPassword);
		}

		public function action_logout(){
			clearCookies();
			return redirect(new HomeUrlModel());
		}

		public function showForm($loginName = null, array $validationErrors = null){
			$returnUrl = request.getParamGet('return');
			$model = new AuthLoginModel($returnUrl, $loginName);
			if($validationErrors != null){
				$model.setValidationErrors($validationErrors);
			}
			return view('app/Auth/Login', $model);
		}

		private function setCookies(User $user, $remember){
			if($remember){
				setPersistentCookies($user);
			} else {
				setSessionCookies($user);
			}
		}

		private function setSessionCookies(User $user){
			$token = userStorage.getToken($user);
			response.setSessionCookie('token', $token);
		}

		private function setPersistentCookies(User $user){
			$token = userStorage.getToken($user);
			response.setPersistentCookie('token', $token);
		}

		private function getReturnUrl($returnUrl){
			if($returnUrl == null || strlen($returnUrl) == 0){
				return new HomeUrlModel();
			}
			return new UrlModel($returnUrl);
		}

		private function clearCookies(){
			response.clearCookie('token');
		}

	}

}