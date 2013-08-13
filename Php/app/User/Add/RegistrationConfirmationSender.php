namespace app\User\Add{

	use Domain\Classes\User;

	interface RegistrationConfirmationSender{

		public function send(User $user, $password);

	}

}