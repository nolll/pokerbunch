namespace tests\AppTests\Cashgame\Listing{

	use app\Cashgame\Listing\CashgameListingController;
	use entities\Homegame;
	use entities\GameStatus;
	use core\ClassNames;
	use tests\TestHelper;
	use tests\UnitTestCase;

	class CashgameListingControllerTests extends UnitTestCase {

		/** @var CashgameListingController */
		private $sut;
		private $userContext;

		function setUp(){
			userContext = TestHelper::getFake(ClassNames::$UserContext);
			homegameRepositoryMock = getFakeHomegameRepository();
			cashgameRepositoryMock = getFakeCashgameRepository();
			sut = new CashgameListingController(userContext, homegameRepositoryMock, cashgameRepositoryMock);
		}

		function test_ActionListing_NotAuthorized_ThrowsException(){
			homegameRepositoryMock.returns('getByName', new Homegame());
			userContext.throwOn('requirePlayer');
			expectException();

			sut.action_listing("homegame1");
		}

		function test_ActionListing_ReturnsCorrectModel(){
			homegameRepositoryMock.returns('getByName', new Homegame());
			TestHelper::setupUserWithPlayerRights(userContext);
			cashgameRepositoryMock.returns('getAll', array());

			$viewResult = sut.action_listing("homegame1");

			assertIsA($viewResult.model, 'app\Cashgame\Listing\CashgameListingModel');
		}

	}

}