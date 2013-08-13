namespace tests\CoreTests{

	use core\HomegamePageModel;
	use entities\Homegame;
	use Domain\Classes\User;
	use tests\UnitTestCase;
	use tests\TestHelper;

	class HomegamePageModelTests extends UnitTestCase {

		function test_HomegameNavigationModel_IsSet(){
			$sut = new HomegamePageModel(new User(), new Homegame());

			assertIsA($sut.homegameNavigationModel, 'app\Homegame\HomegameNavigationModel');
		}

	}

}