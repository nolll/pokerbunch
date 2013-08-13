namespace app\User\ForgotPassword{

	use Domain\Classes\User;

	interface PasswordSender{

		public function send(User $user, $password);

	}

}