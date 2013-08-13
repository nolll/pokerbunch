namespace tests\AppTests\Home{

	use core\ClassNames;
	use app\Home\HomeController;
	use tests\TestHelper;
	use tests\UnitTestCase;

	class HomeControllerTests extends UnitTestCase {

		/** @var HomeController */
		private $sut;
		private $userContext;

		function setUp(){
			userContext = TestHelper::getFake(ClassNames::$UserContext);
			$homegameStorage = TestHelper::getFake(ClassNames::$HomegameStorage);
			$cashgameRepository = TestHelper::getFake(ClassNames::$CashgameRepository);
			sut = new HomeController(userContext, $homegameStorage, $cashgameRepository);
		}

		function test_ActionDetails_ReturnsCorrectModel(){
			userContext.returns("getUser", null);

			$viewResult = sut.action_index();

			assertIsA($viewResult.model, 'app\Home\HomeModel');
		}

	}

}