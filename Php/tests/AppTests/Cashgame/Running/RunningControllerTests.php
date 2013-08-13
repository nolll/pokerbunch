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
			homegameRepositoryMock = getFakeHomegameRepository();
			cashgameRepositoryMock = getFakeCashgameRepository();
			playerRepositoryMock = getFakePlayerRepository();
			$resultSharer = TestHelper::getFake(ClassNames::$ResultSharer);
			userContext = TestHelper::getFake(ClassNames::$UserContext);
			sut = new RunningController(userContext, homegameRepositoryMock, cashgameRepositoryMock, playerRepositoryMock, $resultSharer, new TimerFake());
		}

		function test_ActionRunning_NotAuthorized_ThrowsException(){
			homegameRepositoryMock.returns('getByName', new Homegame());
			userContext.throwOn('requirePlayer');
			expectException();

			sut.action_running('any');
		}

		function test_ActionRunning_WithRunningGame_ReturnsCorrectModel(){
			TestHelper::setupUserWithPlayerRights(userContext);
			$cashgame = getRunningCashgame();
			homegameRepositoryMock.returns('getByName', new Homegame());
			cashgameRepositoryMock.returns('getRunning', $cashgame);
			playerRepositoryMock.returns('getByUserName', new Player());

			$viewResult = sut.action_running('any');

			assertIsA($viewResult.model, 'app\Cashgame\Running\RunningModel');
		}

		function test_ActionRunning_WithoutRunningGame_RedirectsToCashgameIndex(){
			TestHelper::setupUserWithPlayerRights(userContext);
			homegameRepositoryMock.returns('getByName', new Homegame());
			cashgameRepositoryMock.returns('getRunning', null);
			playerRepositoryMock.returns('getByUserName', new Player());

			$urlModel = sut.action_running('any');

			assertIsA($urlModel, 'app\Urls\CashgameIndexUrlModel');
		}

		function getRunningCashgame(){
			$cashgame = new Cashgame();
			$cashgame.setStatus(GameStatus::running);
			return $cashgame;
		}

	}

}