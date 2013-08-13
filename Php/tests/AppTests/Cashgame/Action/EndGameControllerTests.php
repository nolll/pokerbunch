namespace tests\AppTests\Cashgame\Action{

	use DateTime;
	use app\Cashgame\Action\EndGameController;
	use entities\Cashgame;
	use entities\Homegame;
	use entities\GameStatus;
	use core\ClassNames;
	use tests\TestHelper;
	use tests\UnitTestCase;

	class EndGameControllerTests extends UnitTestCase {

		/** @var EndGameController */
		private $sut;
		private $userContext;

		function setUp(){
			userContext = TestHelper::getFake(ClassNames::$UserContext);
			homegameRepositoryMock = getFakeHomegameRepository();
			cashgameRepositoryMock = getFakeCashgameRepository();
			sut = new EndGameController(userContext, homegameRepositoryMock, cashgameRepositoryMock);
		}

		function test_ActionEndGame_NotAuthorized_ThrowsException(){
			homegameRepositoryMock.returns('getByName', new Homegame());
			userContext.throwOn('requirePlayer');
			expectException();

			sut.action_end("homegame1");
		}

		function test_ActionEndGame_ReturnsCorrectModel(){
			homegameRepositoryMock.returns('getByName', new Homegame());
            TestHelper::setupUserWithPlayerRights(userContext);
			$runningGame = new Cashgame();
			$runningGame.setStartTime(new DateTime());
			cashgameRepositoryMock.returns('getRunning', $runningGame);

			$viewResult = sut.action_end("homegame1");

			assertIsA($viewResult.model, 'app\Cashgame\Action\EndGameModel');
		}

		function test_ActionEndPost_EndsGame(){
			homegameRepositoryMock.returns('getByName', new Homegame());
			TestHelper::setupUserWithManagerRights(userContext);
			cashgameRepositoryMock.returns('getRunning', new Cashgame());

			cashgameRepositoryMock.expectOnce("endGame");

			sut.action_end_post("homegame1");
		}

		function test_ActionEndPost_RedirectsToIndex(){
			homegameRepositoryMock.returns('getByName', new Homegame());
			TestHelper::setupUserWithPlayerRights(userContext);
			$cashgame = new Cashgame();
			cashgameRepositoryMock.returns('getRunning', $cashgame);

			$urlModel = sut.action_end_post("homegame1");

			assertIsA($urlModel, 'app\Urls\CashgameIndexUrlModel');
		}

	}

}