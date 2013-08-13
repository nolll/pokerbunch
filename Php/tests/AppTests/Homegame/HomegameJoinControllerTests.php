namespace tests\AppTests\Homegame{

	use app\Homegame\Join\HomegameJoinController;
	use entities\Homegame;
	use core\ClassNames;
	use entities\Player;
	use tests\TestHelper;
	use tests\UnitTestCase;

	class HomegameJoinControllerTests extends UnitTestCase {

		/** @var HomegameJoinController */
		private $sut;
		private $userContext;
		private $request;
		private $invitationCodeCreator;

		function setUp(){
			$this->homegameRepositoryMock = $this->getFakeHomegameRepository();
			$this->playerRepositoryMock = $this->getFakePlayerRepository();
			$this->invitationCodeCreator = TestHelper::getFake(ClassNames::$InvitationCodeCreator);
			$this->userContext = TestHelper::getFake(ClassNames::$UserContext);
			$this->request = TestHelper::getFake(ClassNames::$Request);
			$this->sut = new HomegameJoinController($this->userContext, $this->homegameRepositoryMock, $this->playerRepositoryMock, $this->invitationCodeCreator, $this->request);
		}

		function test_ActionJoin_NotAuthorized_ThrowsException(){
			$this->userContext->throwOn('requireUser');
			$this->expectException();

			$this->sut->action_join('homegame1');
		}

		function test_ActionJoinPost_NoPlayerMatchesCode_JoinHomegameNeverCalled(){
			TestHelper::setupUser($this->userContext);
			$this->homegameRepositoryMock->returns('getByName', new Homegame());
			$player = new Player();
			$this->playerRepositoryMock->returns('getAll', array($player));
			$this->request->returns('getParamPost', 'posted-code', array('invitationcode'));
			$this->invitationCodeCreator->returns('getCode', 'any-code');

			$this->playerRepositoryMock->expectNever("joinHomegame");

			$this->sut->action_join_post('homegame1');
		}

		function test_ActionJoinPost_NoPlayerMatchesCode_ShowsForm(){
			TestHelper::setupUser($this->userContext);
			$this->homegameRepositoryMock->returns('getByName', new Homegame());
			$player = new Player();
			$this->playerRepositoryMock->returns('getAll', array($player));
			$this->request->returns('getParamPost', 'posted-code', array('invitationcode'));
			$this->invitationCodeCreator->returns('getCode', 'any-code');

			$viewResult = $this->sut->action_join_post('homegame1');

			$this->assertIsA($viewResult->model, 'app\Homegame\Join\HomegameJoinModel');
		}

		function test_ActionJoinPost_NoPlayerMatchesCode_PostedCodeIsSet(){
			TestHelper::setupUser($this->userContext);
			$this->homegameRepositoryMock->returns('getByName', new Homegame());
			$player = new Player();
			$this->playerRepositoryMock->returns('getAll', array($player));
			$this->request->returns('getParamPost', 'posted-code', array('invitationcode'));
			$this->invitationCodeCreator->returns('getCode', 'any-code');

			$viewResult = $this->sut->action_join_post('homegame1');

			$this->assertIdentical($viewResult->model->code, 'posted-code');
		}

		function test_ActionJoinPost_NoPlayerMatchesCode_ShowsError(){
			TestHelper::setupUser($this->userContext);
			$this->homegameRepositoryMock->returns('getByName', new Homegame());
			$player = new Player();
			$this->playerRepositoryMock->returns('getAll', array($player));
			$this->request->returns('getParamPost', 'posted-code', array('invitationcode'));
			$this->invitationCodeCreator->returns('getCode', 'any-code');

			$viewResult = $this->sut->action_join_post('homegame1');

			$this->assertIdentical(count($viewResult->model->validationErrors), 1);
		}

		function test_ActionJoinPost_OnePlayerMatchesCode_JoinHomegameCalled(){
			TestHelper::setupUser($this->userContext);
			$this->homegameRepositoryMock->returns('getByName', new Homegame());
			$player = new Player();
			$this->playerRepositoryMock->returns('getAll', array($player));
			$this->request->returns('getParamPost', 'posted-code', array('invitationcode'));
			$this->invitationCodeCreator->returns('getCode', 'posted-code');

			$this->playerRepositoryMock->expectOnce("joinHomegame");

			$this->sut->action_join_post('homegame1');
		}

		function test_ActionJoinPost_WithValidHomegame_RedirectsToConfirmation(){
			TestHelper::setupUser($this->userContext);
			$this->homegameRepositoryMock->returns('getByName', new Homegame());
			$player = new Player();
			$this->playerRepositoryMock->returns('getAll', array($player));
			$this->request->returns('getParamPost', 'posted-code', array('invitationcode'));
			$this->invitationCodeCreator->returns('getCode', 'posted-code');

			$urlModel = $this->sut->action_join_post('homegame1');

			$this->assertIsA($urlModel, 'app\Urls\HomegameJoinConfirmationUrlModel');
		}

		function test_ActionJoinPost_OnePlayerMatchesCodeButIsAlreadyAUser_JoinHomegameNeverCalled(){
			TestHelper::setupUser($this->userContext);
			$this->homegameRepositoryMock->returns('getByName', new Homegame());
			$player = new Player();
			$this->playerRepositoryMock->returns('getAll', array($player));
			$player->setUserName('username');
			$this->request->returns('getParamPost', 'posted-code', array('invitationcode'));
			$this->invitationCodeCreator->returns('getCode', 'posted-code');

			$this->playerRepositoryMock->expectNever("joinHomegame");

			$this->sut->action_join_post('homegame1');
		}

	}

}