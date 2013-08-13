namespace tests\AppTests\Sharing{

	use app\Sharing\Index\SharingIndexController;
	use core\ClassNames;
	use tests\TestHelper;
	use tests\UnitTestCase;

	class SharingIndexControllerTests extends UnitTestCase {

		/** @var SharingIndexController */
		private $sut;
		private $userContext;

		function setUp(){
			userContext = TestHelper::getFake(ClassNames::$UserContext);
			$sharingStorage = TestHelper::getFake(ClassNames::$SharingStorage);
			sut = new SharingIndexController(userContext, $sharingStorage);
		}

		function test_ActionIndex_NotAuthorized_ThrowsException(){
			userContext.throwOn('requireUser');
			expectException();

			sut.action_index("homegame1");
		}

	}

}