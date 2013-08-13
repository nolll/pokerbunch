namespace app\User{

	use Domain\Classes\User;

	interface UserFactory{

		/**
		 * @param $id
		 * @param $userName
		 * @param $displayName
		 * @param $realName
		 * @param $email
		 * @param $globalRole
		 * @return User
		 */
		public function createUser($id, $userName, $displayName, $realName, $email, $globalRole);

	}

}