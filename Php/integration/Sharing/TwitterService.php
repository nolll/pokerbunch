namespace integration\Sharing{

	interface TwitterService{

		public function getKey();

		public function getSecret();

		public function getCallbackUrl();

		public function clearAuthSession();

		public function getRequestToken();

		public function saveRequestTokenToSession($requestToken);

		public function getAuthUrl($token);

		public function verifyTempTokenOrClearSession($tempToken);

		public function getAccessToken($verifyer);

		public function clearSavedRequestTokens();

	}

}