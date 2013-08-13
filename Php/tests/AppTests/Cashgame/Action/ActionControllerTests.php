namespace tests\AppTests\Cashgame\Action{

	use app\Cashgame\Action\ActionController;
	use entities\Cashgame;
	use entities\CashgameResult;
	use entities\Player;
	use entities\Homegame;
	use core\ClassNames;
	use tests\TestHelper;
	use tests\UnitTestCase;

	class ActionControllerTests extends UnitTestCase {

		/** @var ActionController */
		private $sut;
		private $userContext;

		function setUp(){
			$this->userContext = TestHelper::getFake(ClassNames::$UserContext);
			$this->homegameRepositoryMock = $this->getFakeHomegameRepository();
			$this->cashgameRepositoryMock = $this->getFakeCashgameRepository();
			$this->playerRepositoryMock = $this->getFakePlayerRepository();
			$this->sut = new ActionController($this->userContext, $this->homegameRepositoryMock, $this->cashgameRepositoryMock, $this->playerRepositoryMock);
		}

		function test_ActionAction_NotAuthorized_ThrowsException(){
			$this->homegameRepositoryMock->returns('getByName', new Homegame());
			$this->userContext->throwOn('requirePlayer');
			$this->expectException();

			$this->sut->action_action("homegame1", "2010-01-01", "Player 1");
		}

		function test_ActionAction_ReturnsCorrectModel(){
			$this->homegameRepositoryMock->returns('getByName', new Homegame());
            TestHelper::setupUserWithPlayerRights($this->userContext);
            $cashgame = new Cashgame();
			$cashgameResult = new CashgameResult();
			$player = new Player();
			$player->setUserName('user1');
			$player->setId(1);
			$cashgameResult->setPlayer($player);
			$cashgame->setResults(array($cashgameResult));
			$this->cashgameRepositoryMock->returns('getByDateString', $cashgame);
			$this->playerRepositoryMock->returns('getByName', $player);

			$viewResult = $this->sut->action_action("homegame1", "2010-01-01", "Player 1");

			$this->assertIsA($viewResult->model, 'app\Cashgame\Action\ActionModel');
		}

	}

}