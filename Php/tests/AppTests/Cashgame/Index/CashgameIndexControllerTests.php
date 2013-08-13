namespace tests\AppTests\Cashgame\Index{

	use app\Cashgame\Index\CashgameIndexController;
	use core\Util;
	use entities\Homegame;
	use core\ClassNames;
	use tests\TestHelper;
	use tests\UnitTestCase;

	class CashgameIndexControllerTests extends UnitTestCase {

		/** @var CashgameIndexController */
		private $sut;
		private $userContext;

		function setUp(){
			userContext = TestHelper::getFake(ClassNames::$UserContext);
			homegameRepositoryMock = getFakeHomegameRepository();
			cashgameRepositoryMock = getFakeCashgameRepository();
			sut = new CashgameIndexController(userContext, cashgameRepositoryMock, homegameRepositoryMock);
		}

		function test_ActionIndex_NotAuthorized_ThrowsException(){
			homegameRepositoryMock.returns('getByName', new Homegame());
			userContext.throwOn('requirePlayer');
			expectException();

			sut.action_index("homegame1");
		}

		function test_ActionAdd_NotAuthorized_ThrowsException(){
			homegameRepositoryMock.returns('getByName', new Homegame());
			userContext.throwOn('requirePlayer');
			expectException();

			sut.action_index("homegame1");
		}

		function test_ActionIndex_WithYears_RedirectsToMatrixWithLatestYearSelected(){
			homegameRepositoryMock.returns('getByName', new Homegame());
			TestHelper::setupUserWithPlayerRights(userContext);
			$years = array(2011, 2010, 2009);
			cashgameRepositoryMock.returns("getYears", $years);

			$urlModel = sut.action_index("homegame1");

			assertIsA($urlModel, 'app\Urls\CashgameMatrixUrlModel');
			assertTrue(Util::endsWith($urlModel.url, '2011'));
		}

		function test_ActionIndex_NoYears_RedirectsToAddCashgame(){
			homegameRepositoryMock.returns('getByName', new Homegame());
			TestHelper::setupUserWithPlayerRights(userContext);
			$years = array();
			cashgameRepositoryMock.returns("getYears", $years);

			$urlModel = sut.action_index("homegame1");

			assertIsA($urlModel, 'app\Urls\CashgameAddUrlModel');
		}

	}

}