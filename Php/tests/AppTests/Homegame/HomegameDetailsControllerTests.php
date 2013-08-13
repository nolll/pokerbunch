namespace tests\AppTests\Homegame{

	use app\Homegame\Details\HomegameDetailsController;
	use entities\Homegame;
	use core\ClassNames;
	use tests\TestHelper;
	use tests\UnitTestCase;

	class HomegameDetailsControllerTests extends UnitTestCase {

		/** @var HomegameDetailsController */
		private $sut;
		private $userContext;

		function setUp(){
			userContext = TestHelper::getFake(ClassNames::$UserContext);
			cashgameRepositoryMock = getFakeCashgameRepository();
			homegameRepositoryMock = getFakeHomegameRepository();
			sut = new HomegameDetailsController(userContext, cashgameRepositoryMock, homegameRepositoryMock);
		}

		function test_ActionDetails_NotAuthorized_ThrowsException(){
			homegameRepositoryMock.returns('getByName', new Homegame());
			userContext.throwOn('requirePlayer');
			expectException();

			sut.action_details('any');
		}

	}

}