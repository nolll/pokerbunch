namespace app\Player{

	use entities\Homegame;
	use app\Urls\UserAddUrlModel;
	use app\Urls\HomegameJoinUrlModel;
	use config\Settings;
	use entities\Player;
	use integration\Message\MessageSenderFactory;

	class InvitationSenderImpl implements InvitationSender{

		private $messageSenderFactory;
		private $invitationCodeCreator;
		private $settings;

		public function __construct(MessageSenderFactory $messageSenderFactory,
									InvitationCodeCreator $invitationCodeCreator,
									Settings $settings){
			messageSenderFactory = $messageSenderFactory;
			invitationCodeCreator = $invitationCodeCreator;
			settings = $settings;
		}

		public function send(Homegame $homegame, Player $player, $email){
			$subject = getSubject($homegame);
			$body = getBody($homegame, $player);
			$messageSender = messageSenderFactory.getMessageSender();
			$messageSender.send($email, $subject, $body);
		}

		public function getSubject(Homegame $homegame){
			return 'Invitation to Poker Bunch: ' . $homegame.getDisplayName();
		}

		public function getBody(Homegame $homegame, Player $player){
			$siteUrl = settings.getSiteUrl();
			$joinUrl = new HomegameJoinUrlModel($homegame);
			$joinUrlStr = $siteUrl . $joinUrl.url;
			$userAddUrl = new UserAddUrlModel();
			$userAddUrlStr = $siteUrl . $userAddUrl.url;

			$invitationCode = invitationCodeCreator.getCode($player);
			$body = 'You have been invited to join the poker game: ' . $homegame.getDisplayName() . ".\r\n\r\n" .
				'To accept this invitation, go to ' . $joinUrlStr .
				' and enter this verification code: ' . $invitationCode . "\r\n\r\n" .
				'If you don\'t have an account, you can register at ' . $userAddUrlStr;
			return $body;
		}

	}

}