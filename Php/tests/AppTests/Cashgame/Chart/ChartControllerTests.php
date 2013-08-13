namespace tests\AppTests\Cashgame\Chart{

	use app\Cashgame\Chart\ChartController;
	use entities\CashgameSuite;
	use entities\Homegame;
	use core\ClassNames;
	use tests\TestHelper;
	use tests\UnitTestCase;

	class ChartControllerTests extends UnitTestCase {

		/** @var ChartController */
		private $sut;
		private $userContext;

		function setUp(){
			$this->userContext = TestHelper::getFake(ClassNames::$UserContext);
			$this->homegameRepositoryMock = $this->getFakeHomegameRepository();
			$this->cashgameRepositoryMock = $this->getFakeCashgameRepository();
			$this->sut = new ChartController($this->userContext, $this->homegameRepositoryMock, $this->cashgameRepositoryMock);
		}

		function test_ActionChart_NotAuthorized_ThrowsException(){
			$this->homegameRepositoryMock->returns('getByName', new Homegame());
			$this->userContext->throwOn('requirePlayer');
			$this->expectException();

			$this->sut->action_chart("homegame1");
		}

		function test_ActionChart_ModelIsNotNull(){
			$this->homegameRepositoryMock->returns('getByName', new Homegame());
			TestHelper::setupUserWithPlayerRights($this->userContext);
			$this->cashgameRepositoryMock->returns('getSuite', new CashgameSuite());

			$viewResult = $this->sut->action_chart("homegame1");

			$chartData = $viewResult->model;
			$this->assertNotNull($chartData);
		}

	}

}