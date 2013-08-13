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
			userContext = TestHelper::getFake(ClassNames::$UserContext);
			sut = new HomegameListingController(userContext, $homegameStorage);
		}

		function test_ActionIndex_NotAuthorized_ThrowsException(){
			userContext.throwOn('requireAdmin');
			expectException();

			sut.action_listing();
		}

	}

}