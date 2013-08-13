namespace tests\CoreTests{

	use core\PageModel;
	use entities\Homegame;
	use Domain\Classes\User;
	use tests\UnitTestCase;
	use tests\TestHelper;

	class PageModelTests extends UnitTestCase {

		function test_UserNavigationModel_IsSet(){
			$sut = new PageModel(new User());

			assertIsA($sut.userNavigationModel, 'app\User\UserNavigationModel');
		}

	}

}