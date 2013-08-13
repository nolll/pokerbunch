namespace tests\AppTests\Player{

	use app\Player\Invite\PlayerInviteController;
	use core\Validation\ValidatorFake;
	use entities\Homegame;
	use core\ClassNames;
	use entities\Player;
	use tests\TestHelper;
	use core\Validation\Validator;
	use tests\UnitTestCase;

	class PlayerInviteControllerTests extends UnitTestCase {

		/** @var PlayerInviteController */
		private $sut;
		private $userContext;
		private $playerValidatorFactory;
		private $invitationSender;

		function setUp(){
			userContext = TestHelper::getFake(ClassNames::$UserContext);
			homegameRepositoryMock = getFakeHomegameRepository();
			playerRepositoryMock = getFakePlayerRepository();
			cashgameRepositoryMock = getFakeCashgameRepository();
			playerValidatorFactory = TestHelper::getFake(ClassNames::$PlayerValidatorFactory);
			invitationSender = TestHelper::getFake(ClassNames::$InvitationSender);
			$request = TestHelper::getFake(ClassNames::$Request);
			sut = new PlayerInviteController(userContext, homegameRepositoryMock, playerRepositoryMock, cashgameRepositoryMock, playerValidatorFactory, invitationSender, $request);
		}

		function test_ActionInvite_NotAuthorizec_ThrowsException(){
			homegameRepositoryMock.returns('getByName', new Homegame());
			userContext.throwOn('requireManager');
			expectException();

			sut.action_invite("homegame1", "Player 1");
		}

		function test_ActionInvite_NotAuthorized_ThrowsException(){
			homegameRepositoryMock.returns('getByName', new Homegame());
			userContext.throwOn('requireManager');
			expectException();

			sut.action_invite("homegame1", "Player 1");
		}

		function test_ActionInvitePost_WithValidEmail_SendsInvitationEmail(){
			homegameRepositoryMock.returns('getByName', new Homegame());
			TestHelper::setupUserWithManagerRights(userContext);
			$player = new Player();
			playerRepositoryMock.returns('getByName', $player);
			setupValidValidator();

			invitationSender.expectOnce("send");

			sut.action_invite_post("homegame1", "Player 1");
		}

		function test_ActionInvitePost_WithValidEmail_RedirectsToConfirmation(){
			homegameRepositoryMock.returns('getByName', new Homegame());
			TestHelper::setupUserWithManagerRights(userContext);
			$player = new Player();
			playerRepositoryMock.returns('getByName', $player);
			setupValidValidator();

			$urlModel = sut.action_invite_post("homegame1", "Player 1");

			assertIsA($urlModel, 'app\Urls\PlayerInviteConfirmationUrlModel');
		}

		function test_ActionInvitePost_WithInvalidEmail_DoesntSendInvitationEmail(){
			homegameRepositoryMock.returns('getByName', new Homegame());
			TestHelper::setupUserWithManagerRights(userContext);
			$player = new Player();
			playerRepositoryMock.returns('getByName', $player);
			setupInvalidValidator();

			invitationSender.expectNever("send");

			sut.action_invite_post("homegame1", "Player 1");
		}

		function setupValidValidator(){
			$validator = new ValidatorFake(true);
			setupValidator($validator);
		}

		function setupInvalidValidator(){
			$validator = new ValidatorFake(false);
			setupValidator($validator);
		}

		function setupValidator(Validator $validator){
			playerValidatorFactory.returns("getInvitePlayerValidator", $validator);
		}

	}

}