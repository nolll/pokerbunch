namespace tests\AppTests\Homegame{

	use app\Homegame\List\HomegameListController;
	use core\ClassNames;
	use tests\TestHelper;
	use tests\UnitTestCase;

	class HomegameListControllerTests extends UnitTestCase {

		/** @var HomegameListController */
		private $sut;
		private $userContext;

		function setUp(){
			$homegameStorage = TestHelper::getFake(ClassNames::$HomegameStorage);
			userContext = TestHelper::getFake(ClassNames::$UserContext);
			sut = new HomegameListController(userContext, $homegameStorage);
		}

		function test_ActionIndex_NotAuthorized_ThrowsException(){
			userContext.throwOn('requireAdmin');
			expectException();

			sut.action_list();
		}

	}

}