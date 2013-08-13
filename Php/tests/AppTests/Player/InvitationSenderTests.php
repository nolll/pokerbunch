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
			invitationCodeCreator = registerFake(ClassNames::$InvitationCodeCreator);
			messageSenderFactory = registerFake(ClassNames::$MessageSenderFactory);
			settings = registerFake(ClassNames::$Settings);
			invitationSender = getInstance('app\Player\InvitationSenderImpl');
		}

		function test_GetSubject_ReturnsSubjectThatIncludesHomegameDisplayName(){
			$homegame = new Homegame();
			$homegame.setDisplayName('abcdefgh');
			$subject = invitationSender.getSubject($homegame);

			assertTrue(strpos($subject, 'abcdefgh'));
		}

		function test_GetBody_ReturnsBodyThatIncludesHomegameDisplayName(){
			$homegame = new Homegame();
			$homegame.setDisplayName('abcdefgh');
			$player = new Player();
			$body = invitationSender.getBody($homegame, $player);

			assertTrue(strpos($body, 'abcdefgh'));
		}

		function test_GetBody_ReturnsBodyThatIncludesSiteUrl(){
			$homegame = new Homegame();
			$player = new Player();
			settings.returns('getSiteUrl', 'site-url');
			$body = invitationSender.getBody($homegame, $player);

			assertTrue(strpos($body, 'site-url'));
		}

		function test_GetBody_ReturnsBodyThatIncludesJoinUrl(){
			$homegame = new Homegame();
			$homegame.setSlug('abc');

			$player = new Player();
			$joinUrl = new HomegameJoinUrlModel($homegame);

			$body = invitationSender.getBody($homegame, $player);

			assertTrue(strpos($body, $joinUrl.url));
		}

		function test_GetBody_ReturnsBodyThatIncludesUserAddUrl(){
			$homegame = new Homegame();
			$player = new Player();
			$userAddUrl = new UserAddUrlModel();
			$body = invitationSender.getBody($homegame, $player);

			assertTrue(strpos($body, $userAddUrl.url));
		}

		function test_GetBody_ReturnsBodyThatIncludesInvitationCode(){
			$homegame = new Homegame();
			$player = new Player();
			invitationCodeCreator.returns('getCode', 'invitation-code');
			$body = invitationSender.getBody($homegame, $player);

			assertTrue(strpos($body, 'invitation-code'));
		}

		function test_Send_CallsSendOnMessageSender(){
			$homegame = new Homegame();
			$player = new Player();
			$messageSender = TestHelper::getFake('integration\Message\MessageSender');
			messageSenderFactory.returns("getMessageSender", $messageSender);
			$messageSender.expectOnce("send");

			invitationSender.send($homegame, $player, "anyemail@example.com");
		}

	}

}