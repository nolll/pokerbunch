namespace integration\Sharing{

	use config\Settings;
	use app\RouteFormats;
	use Infrastructure\Data\Interfaces\TwitterStorage;
	use Domain\Classes\User;
	use TwitterOAuth;

	importlib('/TwitterOAuth/twitteroauth.php');

	class TwitterServiceImpl implements SocialService, TwitterService{

		private $requestConnection;
		private $accessConnection;
		private $twitterStorage;
		private $settings;

		public function __construct(TwitterStorage $twitterStorage,
									Settings $settings){
			$this->twitterStorage = $twitterStorage;
			$this->settings = $settings;
		}

		public function shareResult(User $user, $amount){
			$message = $this->getMessage($amount);
			$this->postToTwitter($user, $message);
		}

		public function getKey(){
			return $this->settings->getTwitterKey();
		}

		public function getSecret(){
			return $this->settings->getTwitterSecret();
		}

		public function getCallbackUrl(){
			return sprintf(RouteFormats::twitterCallback, $this->settings->getSiteUrl());
		}

		public function clearAuthSession(){
			session_destroy();
		}

		public function getRequestToken(){
			session_start();
			$conn = $this->getRequestConnection();
			$requestToken = $conn->getRequestToken($this->getCallbackUrl());
			if($conn->http_code != 200){
				return null;
			}
			return $requestToken;
		}

		public function saveRequestTokenToSession($requestToken){
			$_SESSION['oauth_token'] = $requestToken['oauth_token'];
			$_SESSION['oauth_token_secret'] = $requestToken['oauth_token_secret'];
		}

		public function getAuthUrl($token){
			$conn = $this->getRequestConnection();
			return $conn->getAuthorizeURL($token);
		}

		public function verifyTempTokenOrClearSession($tempToken){
			session_start();
			/* If the oauth_token is old redirect to the connect page. */
			if (isset($tempToken) && $_SESSION['oauth_token'] !== $tempToken) {
				$_SESSION['oauth_status'] = 'oldtoken';
				$this->clearAuthSession();
				return false;
			}
			return true;
		}

		public function getAccessToken($verifyer){
			$conn = $this->getAccessConnection();
			$accessToken = $conn->getAccessToken($verifyer);
			if($conn->http_code != 200){
				return null;
			}
			return $accessToken;
		}

		public function clearSavedRequestTokens(){
			unset($_SESSION['oauth_token']);
			unset($_SESSION['oauth_token_secret']);
		}

		private function getMessage($amount){
			$formattedAmount = abs($amount) . ' kr';
			$wonOrLost = $amount < 0 ? 'lost' : 'won';
			return 'I just ' . $wonOrLost . ' ' . $formattedAmount . ' playing poker. #pokerbunch';
		}

		private function postToTwitter(User $user, $message){
			$credentials = $this->twitterStorage->getCredentials($user);
			if($credentials != null){
				$connection = new TwitterOAuth($this->getKey(), $this->getSecret(), $credentials->key, $credentials->secret);
				$connection->post('statuses/update', array('status' => $message));
			}
		}

		/** @return TwitterOAuth; */
		private function getRequestConnection(){
			if($this->requestConnection == null){
				$this->requestConnection = new TwitterOAuth($this->getKey(), $this->getSecret());
			}
			return $this->requestConnection;
		}

		private function getAccessConnection(){
			if($this->accessConnection == null){
				$this->accessConnection = new TwitterOAuth($this->getKey(), $this->getSecret(), $_SESSION['oauth_token'], $_SESSION['oauth_token_secret']);
			}
			return $this->accessConnection;
		}

	}

}