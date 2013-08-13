<?php
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
			$this->messageSenderFactory = $messageSenderFactory;
			$this->settings = $settings;
		}

		public function send(User $user, $password){
			$subject = $this->getSubject();
			$body = $this->getBody($password);
			$messageSender = $this->messageSenderFactory->getMessageSender();
			$messageSender->send($user->getEmail(), $subject, $body);
		}

		public function getSubject(){
			return 'Poker Bunch password recovery';
		}

		public function getBody($password){
			$siteUrl = $this->settings->getSiteUrl();
			$loginUrl = new AuthLoginUrlModel();
			$loginUrlStr = $siteUrl . $loginUrl->url;
			$body = "Here is your new password for Poker Bunch:\r\n" .
				$password . "\r\n\r\n" .
				"Please sign in here: " . $loginUrlStr;

			return $body;
		}

	}

}