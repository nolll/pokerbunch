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
			userContext = $userContext;
			sharingStorage = $sharingStorage;
			twitterStorage = $twitterStorage;
			twitterService = $twitterService;
			response = $response;
			request = $request;
		}

		public function action_twitter(){
			userContext.requireUser();
			$user = userContext.getUser();
			return showTwitterSharing($user);
		}

		public function showTwitterSharing(User $user){
			$isSharing = sharingStorage.isSharing($user, SocialServiceProvider::twitter);
			$credentials = twitterStorage.getCredentials($user);
			$model = new SharingTwitterModel($user, $isSharing, $credentials);
			return view('app/Sharing/Twitter/Twitter', $model);
		}

		public function action_twitterstart(){
			userContext.requireUser();
			$requestToken = twitterService.getRequestToken();
			if($requestToken != null){
				twitterService.saveRequestTokenToSession($requestToken);
				$url = twitterService.getAuthUrl($requestToken);
				response.redirect($url);
			} else {
				echo('Could not connect to Twitter. Refresh the page or try again later.');
			}
		}

		public function action_twitterstop(){
			userContext.requireUser();
			$user = userContext.getUser();
			sharingStorage.removeSharing($user, SocialServiceProvider::twitter);
			return redirect(new TwitterSettingsUrlModel());
		}

		public function action_twittercallback(){
			userContext.requireUser();
			$user = userContext.getUser();

			$oAuthToken = request.getParamGet('oauth_token');
			$tokenVerified = twitterService.verifyTempTokenOrClearSession($oAuthToken);
			if($tokenVerified){
				$oAuthVerifier = request.getParamGet('oauth_verifier');
				$accessToken = twitterService.getAccessToken($oAuthVerifier);

				saveCredentials($user, $accessToken);
				twitterService.clearSavedRequestTokens();
				if($accessToken != null){
					$_SESSION['status'] = 'verified';
					sharingStorage.addSharing($user, SocialServiceProvider::twitter);
				} else {
					twitterService.clearAuthSession();
				}
			}

			return redirect(new TwitterSettingsUrlModel());
		}

		public function saveCredentials(User $user, $accessToken){
			$credentials = new TwitterCredentials();
			$credentials.key = $accessToken['oauth_token'];
			$credentials.secret = $accessToken['oauth_token_secret'];
			twitterStorage.addCredentials($user, $credentials);
		}

	}

}