namespace tests\AppTests\Cashgame\List{

	use app\Cashgame\List\CashgameListController;
	use entities\Homegame;
	use entities\GameStatus;
	use core\ClassNames;
	use tests\TestHelper;
	use tests\UnitTestCase;

	class CashgameListControllerTests extends UnitTestCase {

		/** @var CashgameListController */
		private $sut;
		private $userContext;

		function setUp(){
			userContext = TestHelper::getFake(ClassNames::$UserContext);
			homegameRepositoryMock = getFakeHomegameRepository();
			cashgameRepositoryMock = getFakeCashgameRepository();
			sut = new CashgameListController(userContext, homegameRepositoryMock, cashgameRepositoryMock);
		}

		function test_ActionList_NotAuthorized_ThrowsException(){
			homegameRepositoryMock.returns('getByName', new Homegame());
			userContext.throwOn('requirePlayer');
			expectException();

			sut.action_list("homegame1");
		}

		function test_ActionList_ReturnsCorrectModel(){
			homegameRepositoryMock.returns('getByName', new Homegame());
			TestHelper::setupUserWithPlayerRights(userContext);
			cashgameRepositoryMock.returns('getAll', array());

			$viewResult = sut.action_list("homegame1");

			assertIsA($viewResult.model, 'app\Cashgame\List\CashgameListModel');
		}

	}

}