namespace tests\AppTests\Cashgame\Facts{

	use app\Cashgame\Facts\CashgameFactsController;
	use entities\CashgameSuite;
	use entities\Homegame;
	use entities\GameStatus;
	use core\ClassNames;
	use tests\TestHelper;
	use tests\UnitTestCase;

	class CashgameFactsControllerTests extends UnitTestCase {

		/** @var CashgameFactsController */
		private $sut;
		private $userContext;

		function setUp(){
			$this->userContext = TestHelper::getFake(ClassNames::$UserContext);
			$this->homegameRepositoryMock = $this->getFakeHomegameRepository();
			$this->cashgameRepositoryMock = $this->getFakeCashgameRepository();
			$this->sut = new CashgameFactsController($this->userContext, $this->homegameRepositoryMock, $this->cashgameRepositoryMock);
		}

		function test_ActionFacts_NotAuthorized_ThrowsException(){
			$this->homegameRepositoryMock->returns('getByName', new Homegame());
			$this->userContext->throwOn('requirePlayer');
			$this->expectException();

			$this->sut->action_facts("homegame1");
		}

		function test_ActionFacts_ReturnsCorrectModel(){
			$this->homegameRepositoryMock->returns('getByName', new Homegame());
			TestHelper::setupUserWithPlayerRights($this->userContext);
			$suite = new CashgameSuite();
			$this->cashgameRepositoryMock->returns('getSuite', $suite);

			$viewResult = $this->sut->action_facts("homegame1");

			$this->assertIsA($viewResult->model, 'app\Cashgame\Facts\CashgameFactsModel');
		}

	}

}