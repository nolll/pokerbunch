namespace tests\AppTests\Player{

	use app\Player\InvitationCodeCreatorImpl;
	use entities\Player;
	use tests\SharbatUnitTestCase;
	use core\ClassNames;
	use tests\TestHelper;

	class InvitationCodeCreatorTests extends SharbatUnitTestCase {

		/** @var InvitationCodeCreatorImpl */
		private $invitationCodeCreator;
		private $encryption;

		function setUp(){
			parent::setUp();
			encryption = registerFake(ClassNames::$Encryption);
			invitationCodeCreator = getInstance('app\Player\InvitationCodeCreatorImpl');
		}

		function test_GetCode_ReturnsEncryptedPlayerName(){
			$player = new Player();
			encryption.returns('encrypt', 'encrypted-player-name');
			$code = invitationCodeCreator.getCode($player);

			assertIdentical($code, 'encrypted-player-name');
		}

	}

}