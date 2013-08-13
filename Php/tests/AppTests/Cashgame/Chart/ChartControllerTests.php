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
			userContext = TestHelper::getFake(ClassNames::$UserContext);
			homegameRepositoryMock = getFakeHomegameRepository();
			cashgameRepositoryMock = getFakeCashgameRepository();
			sut = new ChartController(userContext, homegameRepositoryMock, cashgameRepositoryMock);
		}

		function test_ActionChart_NotAuthorized_ThrowsException(){
			homegameRepositoryMock.returns('getByName', new Homegame());
			userContext.throwOn('requirePlayer');
			expectException();

			sut.action_chart("homegame1");
		}

		function test_ActionChart_ModelIsNotNull(){
			homegameRepositoryMock.returns('getByName', new Homegame());
			TestHelper::setupUserWithPlayerRights(userContext);
			cashgameRepositoryMock.returns('getSuite', new CashgameSuite());

			$viewResult = sut.action_chart("homegame1");

			$chartData = $viewResult.model;
			assertNotNull($chartData);
		}

	}

}