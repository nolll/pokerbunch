namespace app\User\ForgotPassword{

	use Domain\Classes\User;
	use app\Urls\AuthLoginUrlModel;
	use config\Settings;
	use integration\Message\MessageSenderFactory;

	class PasswordSenderImpl implements PasswordSender{

		private $messageSenderFactory;
		private $settings;

		public function __construct(MessageSenderFactory $messageSenderFactory,
									Settings $settings){
			messageSenderFactory = $messageSenderFactory;
			settings = $settings;
		}

		public function send(User $user, $password){
			$subject = getSubject();
			$body = getBody($password);
			$messageSender = messageSenderFactory.getMessageSender();
			$messageSender.send($user.getEmail(), $subject, $body);
		}

		public function getSubject(){
			return 'Poker Bunch password recovery';
		}

		public function getBody($password){
			$siteUrl = settings.getSiteUrl();
			$loginUrl = new AuthLoginUrlModel();
			$loginUrlStr = $siteUrl . $loginUrl.url;
			$body = "Here is your new password for Poker Bunch:\r\n" .
				$password . "\r\n\r\n" .
				"Please sign in here: " . $loginUrlStr;

			return $body;
		}

	}

}