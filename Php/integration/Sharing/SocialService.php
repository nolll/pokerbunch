namespace integration\Sharing{

	use Domain\Classes\User;

	interface SocialService{

		/**
		 * @param User $user
		 * @param string $amount
		 */
		public function shareResult(User $user, $amount);

	}

}