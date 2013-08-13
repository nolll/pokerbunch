namespace tests\AppTests\User{

	use Domain\Classes\User;
	use tests\UnitTestCase;
	use app\User\UserNavigationModel;
	use tests\TestHelper;

	class UserNavigationModelTests extends UnitTestCase {

		function test_Show_DefaultContentSet(){
			$sut = new UserNavigationModel();

			assertIdentical("Account", $sut.heading);
			assertIdentical("user-nav", $sut.cssClass);
		}

		function test_Show_NotLoggedIn_AnonymousContent(){
			$sut = new UserNavigationModel();

			$nodes = $sut.nodes;
			assertIsA($nodes[0].urlModel, 'app\Urls\AuthLoginUrlModel');
			assertIsA($nodes[1].urlModel, 'app\Urls\UserAddUrlModel');
			assertIsA($nodes[2].urlModel, 'app\Urls\ForgotPasswordUrlModel');
		}

		function test_Show_LoggedIn_AuthorizedContent(){
			$user = new User();
			$user.setDisplayName('a');

			$sut = new UserNavigationModel($user);

			$nodes = $sut.nodes;
			assertIsA($nodes[0].urlModel, 'app\Urls\UserDetailsUrlModel');
			assertIdentical('a', $nodes[0].name);
			assertIsA($nodes[1].urlModel, 'app\Urls\SharingSettingsUrlModel');
			assertIsA($nodes[2].urlModel, 'app\Urls\AuthLogoutUrlModel');
		}

	}

}