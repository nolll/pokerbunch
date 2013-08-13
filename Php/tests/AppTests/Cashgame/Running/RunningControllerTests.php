namespace tests\AppTests\Cashgame\Running{

	use app\Cashgame\Running\RunningController;
	use entities\Cashgame;
	use entities\Player;
	use tests\Fakes\TimerFake;
	use entities\Homegame;
	use entities\GameStatus;
	use core\ClassNames;
	use tests\TestHelper;
	use tests\UnitTestCase;

	class RunningControllerTests extends UnitTestCase {

		/** @var RunningController */
		private $sut;
		private $userContext;

		function setUp(){
			$this->homegameRepositoryMock = $this->getFakeHomegameRepository();
			$this->cashgameRepositoryMock = $this->getFakeCashgameRepository();
			$this->playerRepositoryMock = $this->getFakePlayerRepository();
			$resultSharer = TestHelper::getFake(ClassNames::$ResultSharer);
			$this->userContext = TestHelper::getFake(ClassNames::$UserContext);
			$this->sut = new RunningController($this->userContext, $this->homegameRepositoryMock, $this->cashgameRepositoryMock, $this->playerRepositoryMock, $resultSharer, new TimerFake());
		}

		function test_ActionRunning_NotAuthorized_ThrowsException(){
			$this->homegameRepositoryMock->returns('getByName', new Homegame());
			$this->userContext->throwOn('requirePlayer');
			$this->expectException();

			$this->sut->action_running('any');
		}

		function test_ActionRunning_WithRunningGame_ReturnsCorrectModel(){
			TestHelper::setupUserWithPlayerRights($this->userContext);
			$cashgame = $this->getRunningCashgame();
			$this->homegameRepositoryMock->returns('getByName', new Homegame());
			$this->cashgameRepositoryMock->returns('getRunning', $cashgame);
			$this->playerRepositoryMock->returns('getByUserName', new Player());

			$viewResult = $this->sut->action_running('any');

			$this->assertIsA($viewResult->model, 'app\Cashgame\Running\RunningModel');
		}

		function test_ActionRunning_WithoutRunningGame_RedirectsToCashgameIndex(){
			TestHelper::setupUserWithPlayerRights($this->userContext);
			$this->homegameRepositoryMock->returns('getByName', new Homegame());
			$this->cashgameRepositoryMock->returns('getRunning', null);
			$this->playerRepositoryMock->returns('getByUserName', new Player());

			$urlModel = $this->sut->action_running('any');

			$this->assertIsA($urlModel, 'app\Urls\CashgameIndexUrlModel');
		}

		function getRunningCashgame(){
			$cashgame = new Cashgame();
			$cashgame->setStatus(GameStatus::running);
			return $cashgame;
		}

	}

}