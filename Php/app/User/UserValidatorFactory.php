namespace app\User{

	use core\Validation\Validator;
	use Domain\Classes\User;

	interface UserValidatorFactory{

		/**
		 * @param User $user
		 * @return Validator
		 */
		public function getLoginValidator(User $user = null);

		/**
		 * @param User $user
		 * @return Validator
		 */
		public function getAddUserValidator(User $user);

		/**
		 * @param User $user
		 * @return Validator
		 */
		public function getEditUserValidator(User $user);

		/**
		 * @param $password
		 * @param $repeatPassword
		 * @return Validator
		 */
		public function getChangePasswordValidator($password, $repeatPassword);

		/**
		 * @param $email
		 * @return Validator
		 */
		public function getForgotPasswordValidator($email);

	}

}