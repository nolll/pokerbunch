namespace integration\Avatar{

	interface AvatarService{

		/**
		 * @param string $email
		 */
		public function getSmallAvatarUrl($email);

		/**
		 * @param string $email
		 */
		public function getLargeAvatarUrl($email);

	}

}