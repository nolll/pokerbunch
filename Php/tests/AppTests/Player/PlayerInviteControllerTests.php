<?php
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
			$this->userContext = TestHelper::getFake(ClassNames::$UserContext);
			$this->homegameRepositoryMock = $this->getFakeHomegameRepository();
			$this->playerRepositoryMock = $this->getFakePlayerRepository();
			$this->cashgameRepositoryMock = $this->getFakeCashgameRepository();
			$this->playerValidatorFactory = TestHelper::getFake(ClassNames::$PlayerValidatorFactory);
			$this->invitationSender = TestHelper::getFake(ClassNames::$InvitationSender);
			$request = TestHelper::getFake(ClassNames::$Request);
			$this->sut = new PlayerInviteController($this->userContext, $this->homegameRepositoryMock, $this->playerRepositoryMock, $this->cashgameRepositoryMock, $this->playerValidatorFactory, $this->invitationSender, $request);
		}

		function test_ActionInvite_NotAuthorizec_ThrowsException(){
			$this->homegameRepositoryMock->returns('getByName', new Homegame());
			$this->userContext->throwOn('requireManager');
			$this->expectException();

			$this->sut->action_invite("homegame1", "Player 1");
		}

		function test_ActionInvite_NotAuthorized_ThrowsException(){
			$this->homegameRepositoryMock->returns('getByName', new Homegame());
			$this->userContext->throwOn('requireManager');
			$this->expectException();

			$this->sut->action_invite("homegame1", "Player 1");
		}

		function test_ActionInvitePost_WithValidEmail_SendsInvitationEmail(){
			$this->homegameRepositoryMock->returns('getByName', new Homegame());
			TestHelper::setupUserWithManagerRights($this->userContext);
			$player = new Player();
			$this->playerRepositoryMock->returns('getByName', $player);
			$this->setupValidValidator();

			$this->invitationSender->expectOnce("send");

			$this->sut->action_invite_post("homegame1", "Player 1");
		}

		function test_ActionInvitePost_WithValidEmail_RedirectsToConfirmation(){
			$this->homegameRepositoryMock->returns('getByName', new Homegame());
			TestHelper::setupUserWithManagerRights($this->userContext);
			$player = new Player();
			$this->playerRepositoryMock->returns('getByName', $player);
			$this->setupValidValidator();

			$urlModel = $this->sut->action_invite_post("homegame1", "Player 1");

			$this->assertIsA($urlModel, 'app\Urls\PlayerInviteConfirmationUrlModel');
		}

		function test_ActionInvitePost_WithInvalidEmail_DoesntSendInvitationEmail(){
			$this->homegameRepositoryMock->returns('getByName', new Homegame());
			TestHelper::setupUserWithManagerRights($this->userContext);
			$player = new Player();
			$this->playerRepositoryMock->returns('getByName', $player);
			$this->setupInvalidValidator();

			$this->invitationSender->expectNever("send");

			$this->sut->action_invite_post("homegame1", "Player 1");
		}

		function setupValidValidator(){
			$validator = new ValidatorFake(true);
			$this->setupValidator($validator);
		}

		function setupInvalidValidator(){
			$validator = new ValidatorFake(false);
			$this->setupValidator($validator);
		}

		function setupValidator(Validator $validator){
			$this->playerValidatorFactory->returns("getInvitePlayerValidator", $validator);
		}

	}

}