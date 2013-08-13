namespace tests\AppTests\Cashgame\Action{

	use app\Cashgame\Action\ReportController;
	use core\Validation\Validator;
	use core\Validation\ValidatorFake;
	use entities\Cashgame;
	use entities\Homegame;
	use entities\GameStatus;
	use entities\Player;
	use exceptions\AccessDeniedException;
	use core\ClassNames;
	use tests\TestHelper;
	use tests\UnitTestCase;

	class ReportControllerTests extends UnitTestCase {

		/** @var ReportController */
		private $sut;
		private $userContext;
		private $cashgameValidatorFactory;

		function setUp(){
			userContext = TestHelper::getFake(ClassNames::$UserContext);
			homegameRepositoryMock = getFakeHomegameRepository();
			cashgameRepositoryMock = getFakeCashgameRepository();
			playerRepositoryMock = getFakePlayerRepository();
			$request = TestHelper::getFake(ClassNames::$Request);
			cashgameValidatorFactory = TestHelper::getFake(ClassNames::$CashgameValidatorFactory);
			sut = new ReportController(userContext, homegameRepositoryMock, cashgameRepositoryMock, playerRepositoryMock, $request, cashgameValidatorFactory);
		}

		function test_ActionReport_NotAuthorized_ThrowsException(){
			homegameRepositoryMock.returns('getByName', new Homegame());
			userContext.throwOn('requirePlayer');
			expectException();

			sut.action_report("homegame1", "Player 1");
		}

		function test_ActionReport_WithPlayerRightsAndIsAnotherPlayer_ThrowsException(){
			homegameRepositoryMock.returns('getByName', new Homegame());
			TestHelper::setupUserWithPlayerRights(userContext);
			$player = new Player();
			$player.setUserName('otherUser');
			playerRepositoryMock.returns('getByName', $player);

			expectException(new AccessDeniedException());

			sut.action_report("homegame1", "Player 1");
		}

		function test_ActionReport_ReturnsCorrectModel(){
			homegameRepositoryMock.returns('getByName', new Homegame());
            TestHelper::setupUserWithPlayerRights(userContext);
            $cashgame = new Cashgame();
			$cashgame.setStatus(GameStatus::running);
			cashgameRepositoryMock.returns('getRunning', $cashgame);
			$player = new Player();
			$player.setUserName('user1');
			playerRepositoryMock.returns('getByName', $player);

			$viewResult = sut.action_report("homegame1", "Player 1");

			assertIsA($viewResult.model, 'app\Cashgame\Action\ReportModel');
		}

		function test_ActionReportPost_AddsCheckpoint(){
			homegameRepositoryMock.returns('getByName', new Homegame());
			TestHelper::setupUserWithManagerRights(userContext);
			$cashgame = new Cashgame();
			$cashgame.setStatus(GameStatus::running);
			cashgameRepositoryMock.returns('getRunning', $cashgame);
			$player = new Player();
			playerRepositoryMock.returns('getByName', $player);
			setupValidReportValidator();

			cashgameRepositoryMock.expectOnce("addCheckpoint");

			sut.action_report_post("homegame1", "2010-01-01", "Player 1");
		}

		function test_ActionReportPost_RedirectsToRunningCashgame(){
			homegameRepositoryMock.returns('getByName', new Homegame());
			TestHelper::setupUserWithManagerRights(userContext);
			$cashgame = new Cashgame();
			$cashgame.setStatus(GameStatus::running);
			cashgameRepositoryMock.returns('getRunning', $cashgame);
			$player = new Player();
			playerRepositoryMock.returns('getByName', $player);
			setupValidReportValidator();

			$urlModel = sut.action_report_post("homegame1", "Player 1");

			assertIsA($urlModel, 'app\Urls\RunningCashgameUrlModel');
		}

		private function setupValidReportValidator(){
			$validator = new ValidatorFake(true);
			setupReportValidator($validator);
		}

		private function setupReportValidator(Validator $validator){
			cashgameValidatorFactory.returns("getReportValidator", $validator);
		}

	}

}