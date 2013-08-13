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
			homegameRepositoryMock = getFakeHomegameRepository();
			playerRepositoryMock = getFakePlayerRepository();
			invitationCodeCreator = TestHelper::getFake(ClassNames::$InvitationCodeCreator);
			userContext = TestHelper::getFake(ClassNames::$UserContext);
			request = TestHelper::getFake(ClassNames::$Request);
			sut = new HomegameJoinController(userContext, homegameRepositoryMock, playerRepositoryMock, invitationCodeCreator, request);
		}

		function test_ActionJoin_NotAuthorized_ThrowsException(){
			userContext.throwOn('requireUser');
			expectException();

			sut.action_join('homegame1');
		}

		function test_ActionJoinPost_NoPlayerMatchesCode_JoinHomegameNeverCalled(){
			TestHelper::setupUser(userContext);
			homegameRepositoryMock.returns('getByName', new Homegame());
			$player = new Player();
			playerRepositoryMock.returns('getAll', array($player));
			request.returns('getParamPost', 'posted-code', array('invitationcode'));
			invitationCodeCreator.returns('getCode', 'any-code');

			playerRepositoryMock.expectNever("joinHomegame");

			sut.action_join_post('homegame1');
		}

		function test_ActionJoinPost_NoPlayerMatchesCode_ShowsForm(){
			TestHelper::setupUser(userContext);
			homegameRepositoryMock.returns('getByName', new Homegame());
			$player = new Player();
			playerRepositoryMock.returns('getAll', array($player));
			request.returns('getParamPost', 'posted-code', array('invitationcode'));
			invitationCodeCreator.returns('getCode', 'any-code');

			$viewResult = sut.action_join_post('homegame1');

			assertIsA($viewResult.model, 'app\Homegame\Join\HomegameJoinModel');
		}

		function test_ActionJoinPost_NoPlayerMatchesCode_PostedCodeIsSet(){
			TestHelper::setupUser(userContext);
			homegameRepositoryMock.returns('getByName', new Homegame());
			$player = new Player();
			playerRepositoryMock.returns('getAll', array($player));
			request.returns('getParamPost', 'posted-code', array('invitationcode'));
			invitationCodeCreator.returns('getCode', 'any-code');

			$viewResult = sut.action_join_post('homegame1');

			assertIdentical($viewResult.model.code, 'posted-code');
		}

		function test_ActionJoinPost_NoPlayerMatchesCode_ShowsError(){
			TestHelper::setupUser(userContext);
			homegameRepositoryMock.returns('getByName', new Homegame());
			$player = new Player();
			playerRepositoryMock.returns('getAll', array($player));
			request.returns('getParamPost', 'posted-code', array('invitationcode'));
			invitationCodeCreator.returns('getCode', 'any-code');

			$viewResult = sut.action_join_post('homegame1');

			assertIdentical(count($viewResult.model.validationErrors), 1);
		}

		function test_ActionJoinPost_OnePlayerMatchesCode_JoinHomegameCalled(){
			TestHelper::setupUser(userContext);
			homegameRepositoryMock.returns('getByName', new Homegame());
			$player = new Player();
			playerRepositoryMock.returns('getAll', array($player));
			request.returns('getParamPost', 'posted-code', array('invitationcode'));
			invitationCodeCreator.returns('getCode', 'posted-code');

			playerRepositoryMock.expectOnce("joinHomegame");

			sut.action_join_post('homegame1');
		}

		function test_ActionJoinPost_WithValidHomegame_RedirectsToConfirmation(){
			TestHelper::setupUser(userContext);
			homegameRepositoryMock.returns('getByName', new Homegame());
			$player = new Player();
			playerRepositoryMock.returns('getAll', array($player));
			request.returns('getParamPost', 'posted-code', array('invitationcode'));
			invitationCodeCreator.returns('getCode', 'posted-code');

			$urlModel = sut.action_join_post('homegame1');

			assertIsA($urlModel, 'app\Urls\HomegameJoinConfirmationUrlModel');
		}

		function test_ActionJoinPost_OnePlayerMatchesCodeButIsAlreadyAUser_JoinHomegameNeverCalled(){
			TestHelper::setupUser(userContext);
			homegameRepositoryMock.returns('getByName', new Homegame());
			$player = new Player();
			playerRepositoryMock.returns('getAll', array($player));
			$player.setUserName('username');
			request.returns('getParamPost', 'posted-code', array('invitationcode'));
			invitationCodeCreator.returns('getCode', 'posted-code');

			playerRepositoryMock.expectNever("joinHomegame");

			sut.action_join_post('homegame1');
		}

	}

}