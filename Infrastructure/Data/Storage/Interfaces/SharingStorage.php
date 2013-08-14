namespace Infrastructure\Data\Interfaces {

	use Domain\Classes\User;
	use string;

	interface SharingStorage{

		/**
		 * @param User $user
		 * @return string[]
		 */
		public function getServices(User $user);

		/**
		 * @param User $user
		 * @param string $sharingProvider
		 * @return void
		 */
		public function addSharing(User $user, $sharingProvider);

		/**
		 * @param User $user
		 * @param string $sharingProvider
		 * @return void
		 */
		public function removeSharing(User $user, $sharingProvider);

		/**
		 * @param User $user
		 * @param string $sharingProvider
		 * @return bool
		 */
		public function isSharing(User $user, $sharingProvider);

	}

}