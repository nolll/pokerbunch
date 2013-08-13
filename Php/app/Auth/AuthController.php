<?php
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
			$this->userStorage = $userStorage;
			$this->encryption = $encryption;
			$this->userValidatorFactory = $userValidatorFactory;
			$this->response = $response;
			$this->request = $request;
		}

		public function action_login(){
			return $this->showForm();
		}

		public function action_login_post(){
			$loginName = $this->request->getParamPost('ln');
			$password = $this->request->getParamPost('pw');

			$user = $this->getLoggedInUser($loginName, $password);

			$validator = $this->userValidatorFactory->getLoginValidator($user);
			if($validator->isValid()){
				$remember = $this->request->getParamPost('remember');
				$returnUrl = $this->request->getParamPost('return');
				$this->setCookies($user, $remember);
				return $this->redirect($this->getReturnUrl($returnUrl));
			} else {
				return $this->showForm($loginName, $validator->getErrors());
			}
		}

		private function getLoggedInUser($loginName, $password){
			$salt = $this->userStorage->getSalt($loginName);
			$encryptedPassword = $this->encryption->encrypt($password, $salt);
			return $this->userStorage->getUserByCredentials($loginName, $encryptedPassword);
		}

		public function action_logout(){
			$this->clearCookies();
			return $this->redirect(new HomeUrlModel());
		}

		public function showForm($loginName = null, array $validationErrors = null){
			$returnUrl = $this->request->getParamGet('return');
			$model = new AuthLoginModel($returnUrl, $loginName);
			if($validationErrors != null){
				$model->setValidationErrors($validationErrors);
			}
			return $this->view('app/Auth/Login', $model);
		}

		private function setCookies(User $user, $remember){
			if($remember){
				$this->setPersistentCookies($user);
			} else {
				$this->setSessionCookies($user);
			}
		}

		private function setSessionCookies(User $user){
			$token = $this->userStorage->getToken($user);
			$this->response->setSessionCookie('token', $token);
		}

		private function setPersistentCookies(User $user){
			$token = $this->userStorage->getToken($user);
			$this->response->setPersistentCookie('token', $token);
		}

		private function getReturnUrl($returnUrl){
			if($returnUrl == null || strlen($returnUrl) == 0){
				return new HomeUrlModel();
			}
			return new UrlModel($returnUrl);
		}

		private function clearCookies(){
			$this->response->clearCookie('token');
		}

	}

}