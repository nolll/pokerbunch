namespace tests\AppTests\Player{

	use app\Player\InvitationSenderImpl;
	use app\Urls\HomegameJoinUrlModel;
	use entities\Homegame;
	use entities\Player;
	use tests\SharbatUnitTestCase;
	use app\Urls\UserAddUrlModel;
	use core\ClassNames;
	use tests\TestHelper;

	class InvitationSenderTests extends SharbatUnitTestCase {

		/** @var InvitationSenderImpl */
		private $invitationSender;
		private $messageSenderFactory;
		private $invitationCodeCreator;
		private $settings;

		function setUp(){
			parent::setUp();
			$this->invitationCodeCreator = $this->registerFake(ClassNames::$InvitationCodeCreator);
			$this->messageSenderFactory = $this->registerFake(ClassNames::$MessageSenderFactory);
			$this->settings = $this->registerFake(ClassNames::$Settings);
			$this->invitationSender = $this->getInstance('app\Player\InvitationSenderImpl');
		}

		function test_GetSubject_ReturnsSubjectThatIncludesHomegameDisplayName(){
			$homegame = new Homegame();
			$homegame->setDisplayName('abcdefgh');
			$subject = $this->invitationSender->getSubject($homegame);

			$this->assertTrue(strpos($subject, 'abcdefgh'));
		}

		function test_GetBody_ReturnsBodyThatIncludesHomegameDisplayName(){
			$homegame = new Homegame();
			$homegame->setDisplayName('abcdefgh');
			$player = new Player();
			$body = $this->invitationSender->getBody($homegame, $player);

			$this->assertTrue(strpos($body, 'abcdefgh'));
		}

		function test_GetBody_ReturnsBodyThatIncludesSiteUrl(){
			$homegame = new Homegame();
			$player = new Player();
			$this->settings->returns('getSiteUrl', 'site-url');
			$body = $this->invitationSender->getBody($homegame, $player);

			$this->assertTrue(strpos($body, 'site-url'));
		}

		function test_GetBody_ReturnsBodyThatIncludesJoinUrl(){
			$homegame = new Homegame();
			$homegame->setSlug('abc');

			$player = new Player();
			$joinUrl = new HomegameJoinUrlModel($homegame);

			$body = $this->invitationSender->getBody($homegame, $player);

			$this->assertTrue(strpos($body, $joinUrl->url));
		}

		function test_GetBody_ReturnsBodyThatIncludesUserAddUrl(){
			$homegame = new Homegame();
			$player = new Player();
			$userAddUrl = new UserAddUrlModel();
			$body = $this->invitationSender->getBody($homegame, $player);

			$this->assertTrue(strpos($body, $userAddUrl->url));
		}

		function test_GetBody_ReturnsBodyThatIncludesInvitationCode(){
			$homegame = new Homegame();
			$player = new Player();
			$this->invitationCodeCreator->returns('getCode', 'invitation-code');
			$body = $this->invitationSender->getBody($homegame, $player);

			$this->assertTrue(strpos($body, 'invitation-code'));
		}

		function test_Send_CallsSendOnMessageSender(){
			$homegame = new Homegame();
			$player = new Player();
			$messageSender = TestHelper::getFake('integration\Message\MessageSender');
			$this->messageSenderFactory->returns("getMessageSender", $messageSender);
			$messageSender->expectOnce("send");

			$this->invitationSender->send($homegame, $player, "anyemail@example.com");
		}

	}

}