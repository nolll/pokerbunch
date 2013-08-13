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
			$this->encryption = $this->registerFake(ClassNames::$Encryption);
			$this->invitationCodeCreator = $this->getInstance('app\Player\InvitationCodeCreatorImpl');
		}

		function test_GetCode_ReturnsEncryptedPlayerName(){
			$player = new Player();
			$this->encryption->returns('encrypt', 'encrypted-player-name');
			$code = $this->invitationCodeCreator->getCode($player);

			$this->assertIdentical($code, 'encrypted-player-name');
		}

	}

}