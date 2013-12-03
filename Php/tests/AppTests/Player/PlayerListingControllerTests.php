namespace tests\AppTests\Player{

	use app\Player\List\PlayerListController;
	use entities\Homegame;
	use core\ClassNames;
	use tests\TestHelper;
	use tests\UnitTestCase;

	class PlayerListControllerTests extends UnitTestCase {

		/** @var PlayerListController */
		private $sut;
		private $userContext;

		function setUp(){
			userContext = TestHelper::getFake(ClassNames::$UserContext);
			homegameRepositoryMock = getFakeHomegameRepository();
			playerRepositoryMock = getFakePlayerRepository();
			cashgameRepositoryMock = getFakeCashgameRepository();
			sut = new PlayerListController(userContext, homegameRepositoryMock, playerRepositoryMock, cashgameRepositoryMock);
		}

		function test_ActionIndex_NotAuthorized_ThrowsException(){
			homegameRepositoryMock.returns('getByName', new Homegame());
			userContext.throwOn('requirePlayer');
			expectException();

			sut.action_index("homegame1");
		}

	}

}