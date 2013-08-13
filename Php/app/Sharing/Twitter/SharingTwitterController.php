<?php
namespace app\Sharing\Twitter{

	use Mishiin\Request;
	use Mishiin\Response;
	use core\PageController;
	use app\Urls\TwitterSettingsUrlModel;
	use core\UserContext;
	use Infrastructure\Data\Interfaces\SharingStorage;
	use Infrastructure\Data\Interfaces\TwitterStorage;
	use integration\Sharing\SocialServiceProvider;
	use integration\Sharing\TwitterService;
	use Domain\Classes\User;
	use integration\Sharing\TwitterCredentials;

	class SharingTwitterController extends PageController {

		private $userContext;
		private $sharingStorage;
		private $twitterStorage;
		private $twitterService;
		private $response;
		private $request;

		public function __construct(UserContext $userContext,
									SharingStorage $sharingStorage,
									\Infrastructure\Data\Interfaces\TwitterStorage $twitterStorage,
									TwitterService $twitterService,
									Response $response,
									Request $request){
			$this->userContext = $userContext;
			$this->sharingStorage = $sharingStorage;
			$this->twitterStorage = $twitterStorage;
			$this->twitterService = $twitterService;
			$this->response = $response;
			$this->request = $request;
		}

		public function action_twitter(){
			$this->userContext->requireUser();
			$user = $this->userContext->getUser();
			return $this->showTwitterSharing($user);
		}

		public function showTwitterSharing(User $user){
			$isSharing = $this->sharingStorage->isSharing($user, SocialServiceProvider::twitter);
			$credentials = $this->twitterStorage->getCredentials($user);
			$model = new SharingTwitterModel($user, $isSharing, $credentials);
			return $this->view('app/Sharing/Twitter/Twitter', $model);
		}

		public function action_twitterstart(){
			$this->userContext->requireUser();
			$requestToken = $this->twitterService->getRequestToken();
			if($requestToken != null){
				$this->twitterService->saveRequestTokenToSession($requestToken);
				$url = $this->twitterService->getAuthUrl($requestToken);
				$this->response->redirect($url);
			} else {
				echo('Could not connect to Twitter. Refresh the page or try again later.');
			}
		}

		public function action_twitterstop(){
			$this->userContext->requireUser();
			$user = $this->userContext->getUser();
			$this->sharingStorage->removeSharing($user, SocialServiceProvider::twitter);
			return $this->redirect(new TwitterSettingsUrlModel());
		}

		public function action_twittercallback(){
			$this->userContext->requireUser();
			$user = $this->userContext->getUser();

			$oAuthToken = $this->request->getParamGet('oauth_token');
			$tokenVerified = $this->twitterService->verifyTempTokenOrClearSession($oAuthToken);
			if($tokenVerified){
				$oAuthVerifier = $this->request->getParamGet('oauth_verifier');
				$accessToken = $this->twitterService->getAccessToken($oAuthVerifier);

				$this->saveCredentials($user, $accessToken);
				$this->twitterService->clearSavedRequestTokens();
				if($accessToken != null){
					$_SESSION['status'] = 'verified';
					$this->sharingStorage->addSharing($user, SocialServiceProvider::twitter);
				} else {
					$this->twitterService->clearAuthSession();
				}
			}

			return $this->redirect(new TwitterSettingsUrlModel());
		}

		public function saveCredentials(User $user, $accessToken){
			$credentials = new TwitterCredentials();
			$credentials->key = $accessToken['oauth_token'];
			$credentials->secret = $accessToken['oauth_token_secret'];
			$this->twitterStorage->addCredentials($user, $credentials);
		}

	}

}