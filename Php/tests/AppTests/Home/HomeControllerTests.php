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
			$this->userContext = TestHelper::getFake(ClassNames::$UserContext);
			$homegameStorage = TestHelper::getFake(ClassNames::$HomegameStorage);
			$cashgameRepository = TestHelper::getFake(ClassNames::$CashgameRepository);
			$this->sut = new HomeController($this->userContext, $homegameStorage, $cashgameRepository);
		}

		function test_ActionDetails_ReturnsCorrectModel(){
			$this->userContext->returns("getUser", null);

			$viewResult = $this->sut->action_index();

			$this->assertIsA($viewResult->model, 'app\Home\HomeModel');
		}

	}

}