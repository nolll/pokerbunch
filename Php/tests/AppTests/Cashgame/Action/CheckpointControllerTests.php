namespace tests\AppTests\Cashgame\Action{

	use DateTime;
	use app\Cashgame\Action\CheckpointController;
	use entities\Cashgame;
	use entities\Homegame;
	use core\ClassNames;
	use entities\Player;
	use tests\TestHelper;
	use tests\UnitTestCase;

	class CheckpointControllerTests extends UnitTestCase {

		/** @var CheckpointController */
		private $sut;
		private $userContext;

		function setUp(){
			userContext = TestHelper::getFake(ClassNames::$UserContext);
			homegameRepositoryMock = getFakeHomegameRepository();
			cashgameRepositoryMock = getFakeCashgameRepository();
			playerRepositoryMock = getFakePlayerRepository();
			sut = new CheckpointController(userContext, homegameRepositoryMock, cashgameRepositoryMock, playerRepositoryMock);
		}

		function test_ActionDeleteCheckpoint_NotAuthorized_ThrowsException(){
			homegameRepositoryMock.returns('getByName', new Homegame());
			userContext.throwOn('requireManager');
			expectException();

			sut.action_deletecheckpoint("homegame1", "2010-01-01", "Player 1", "1");
		}

		function test_ActionDeleteCheckpoint_WithId_CallsDeleteCheckpoint(){
			homegameRepositoryMock.returns('getByName', new Homegame());
			$cashgame = new Cashgame();
			$cashgame.setStartTime(new DateTime());
			cashgameRepositoryMock.returns('getByDateString', $cashgame);
			playerRepositoryMock.returns('getByName', new Player());
			cashgameRepositoryMock.expectOnce('deleteCheckpoint');

			sut.action_deletecheckpoint("homegame1", "2010-01-01", "Player 1", "1");
		}

		function test_ActionDeleteCheckpoint_WithId_RedirectsToAction(){
			homegameRepositoryMock.returns('getByName', new Homegame());
			$cashgame = new Cashgame();
			$cashgame.setStartTime(new DateTime());
			cashgameRepositoryMock.returns('getByDateString', $cashgame);
			playerRepositoryMock.returns('getByName', new Player());

			$urlModel = sut.action_deletecheckpoint("homegame1", "2010-01-01", "Player 1", "1");

			assertIsA($urlModel, 'app\Urls\CashgameActionUrlModel');
		}

	}

}