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
			userContext = TestHelper::getFake(ClassNames::$UserContext);
			homegameRepositoryMock = getFakeHomegameRepository();
			cashgameRepositoryMock = getFakeCashgameRepository();
			sut = new CashgameFactsController(userContext, homegameRepositoryMock, cashgameRepositoryMock);
		}

		function test_ActionFacts_NotAuthorized_ThrowsException(){
			homegameRepositoryMock.returns('getByName', new Homegame());
			userContext.throwOn('requirePlayer');
			expectException();

			sut.action_facts("homegame1");
		}

		function test_ActionFacts_ReturnsCorrectModel(){
			homegameRepositoryMock.returns('getByName', new Homegame());
			TestHelper::setupUserWithPlayerRights(userContext);
			$suite = new CashgameSuite();
			cashgameRepositoryMock.returns('getSuite', $suite);

			$viewResult = sut.action_facts("homegame1");

			assertIsA($viewResult.model, 'app\Cashgame\Facts\CashgameFactsModel');
		}

	}

}