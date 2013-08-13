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
			$this->userContext = TestHelper::getFake(ClassNames::$UserContext);
			$this->homegameRepositoryMock = $this->getFakeHomegameRepository();
			$this->cashgameRepositoryMock = $this->getFakeCashgameRepository();
			$this->playerRepositoryMock = $this->getFakePlayerRepository();
			$this->sut = new CheckpointController($this->userContext, $this->homegameRepositoryMock, $this->cashgameRepositoryMock, $this->playerRepositoryMock);
		}

		function test_ActionDeleteCheckpoint_NotAuthorized_ThrowsException(){
			$this->homegameRepositoryMock->returns('getByName', new Homegame());
			$this->userContext->throwOn('requireManager');
			$this->expectException();

			$this->sut->action_deletecheckpoint("homegame1", "2010-01-01", "Player 1", "1");
		}

		function test_ActionDeleteCheckpoint_WithId_CallsDeleteCheckpoint(){
			$this->homegameRepositoryMock->returns('getByName', new Homegame());
			$cashgame = new Cashgame();
			$cashgame->setStartTime(new DateTime());
			$this->cashgameRepositoryMock->returns('getByDateString', $cashgame);
			$this->playerRepositoryMock->returns('getByName', new Player());
			$this->cashgameRepositoryMock->expectOnce('deleteCheckpoint');

			$this->sut->action_deletecheckpoint("homegame1", "2010-01-01", "Player 1", "1");
		}

		function test_ActionDeleteCheckpoint_WithId_RedirectsToAction(){
			$this->homegameRepositoryMock->returns('getByName', new Homegame());
			$cashgame = new Cashgame();
			$cashgame->setStartTime(new DateTime());
			$this->cashgameRepositoryMock->returns('getByDateString', $cashgame);
			$this->playerRepositoryMock->returns('getByName', new Player());

			$urlModel = $this->sut->action_deletecheckpoint("homegame1", "2010-01-01", "Player 1", "1");

			$this->assertIsA($urlModel, 'app\Urls\CashgameActionUrlModel');
		}

	}

}