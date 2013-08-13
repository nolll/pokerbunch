namespace Infrastructure\Data\Interfaces {

	use Domain\Classes\User;
	use integration\Sharing\TwitterCredentials;

	interface TwitterStorage{

		/**
		 * @param User $user
		 * @return TwitterCredentials
		 */
		public function getCredentials(User $user);

		/**
		 * @param User $user
		 * @param TwitterCredentials $credentials
		 * @return bool
		 */
		public function addCredentials(User $user, TwitterCredentials $credentials);

		/**
		 * @param User $user
		 * @return bool
		 */
		public function clearCredentials(User $user);

	}

}