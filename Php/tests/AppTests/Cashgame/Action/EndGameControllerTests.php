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
			$this->userContext = TestHelper::getFake(ClassNames::$UserContext);
			$this->homegameRepositoryMock = $this->getFakeHomegameRepository();
			$this->cashgameRepositoryMock = $this->getFakeCashgameRepository();
			$this->sut = new EndGameController($this->userContext, $this->homegameRepositoryMock, $this->cashgameRepositoryMock);
		}

		function test_ActionEndGame_NotAuthorized_ThrowsException(){
			$this->homegameRepositoryMock->returns('getByName', new Homegame());
			$this->userContext->throwOn('requirePlayer');
			$this->expectException();

			$this->sut->action_end("homegame1");
		}

		function test_ActionEndGame_ReturnsCorrectModel(){
			$this->homegameRepositoryMock->returns('getByName', new Homegame());
            TestHelper::setupUserWithPlayerRights($this->userContext);
			$runningGame = new Cashgame();
			$runningGame->setStartTime(new DateTime());
			$this->cashgameRepositoryMock->returns('getRunning', $runningGame);

			$viewResult = $this->sut->action_end("homegame1");

			$this->assertIsA($viewResult->model, 'app\Cashgame\Action\EndGameModel');
		}

		function test_ActionEndPost_EndsGame(){
			$this->homegameRepositoryMock->returns('getByName', new Homegame());
			TestHelper::setupUserWithManagerRights($this->userContext);
			$this->cashgameRepositoryMock->returns('getRunning', new Cashgame());

			$this->cashgameRepositoryMock->expectOnce("endGame");

			$this->sut->action_end_post("homegame1");
		}

		function test_ActionEndPost_RedirectsToIndex(){
			$this->homegameRepositoryMock->returns('getByName', new Homegame());
			TestHelper::setupUserWithPlayerRights($this->userContext);
			$cashgame = new Cashgame();
			$this->cashgameRepositoryMock->returns('getRunning', $cashgame);

			$urlModel = $this->sut->action_end_post("homegame1");

			$this->assertIsA($urlModel, 'app\Urls\CashgameIndexUrlModel');
		}

	}

}