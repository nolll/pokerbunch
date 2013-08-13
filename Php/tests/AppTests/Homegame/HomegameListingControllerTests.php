namespace tests\AppTests\Homegame{

	use app\Homegame\Listing\HomegameListingController;
	use core\ClassNames;
	use tests\TestHelper;
	use tests\UnitTestCase;

	class HomegameListingControllerTests extends UnitTestCase {

		/** @var HomegameListingController */
		private $sut;
		private $userContext;

		function setUp(){
			$homegameStorage = TestHelper::getFake(ClassNames::$HomegameStorage);
			$this->userContext = TestHelper::getFake(ClassNames::$UserContext);
			$this->sut = new HomegameListingController($this->userContext, $homegameStorage);
		}

		function test_ActionIndex_NotAuthorized_ThrowsException(){
			$this->userContext->throwOn('requireAdmin');
			$this->expectException();

			$this->sut->action_listing();
		}

	}

}