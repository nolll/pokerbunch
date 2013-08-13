<?php
namespace tests\AppTests\User{

	use Domain\Classes\User;
	use tests\UnitTestCase;
	use app\User\UserNavigationModel;
	use tests\TestHelper;

	class UserNavigationModelTests extends UnitTestCase {

		function test_Show_DefaultContentSet(){
			$sut = new UserNavigationModel();

			$this->assertIdentical("Account", $sut->heading);
			$this->assertIdentical("user-nav", $sut->cssClass);
		}

		function test_Show_NotLoggedIn_AnonymousContent(){
			$sut = new UserNavigationModel();

			$nodes = $sut->nodes;
			$this->assertIsA($nodes[0]->urlModel, 'app\Urls\AuthLoginUrlModel');
			$this->assertIsA($nodes[1]->urlModel, 'app\Urls\UserAddUrlModel');
			$this->assertIsA($nodes[2]->urlModel, 'app\Urls\ForgotPasswordUrlModel');
		}

		function test_Show_LoggedIn_AuthorizedContent(){
			$user = new User();
			$user->setDisplayName('a');

			$sut = new UserNavigationModel($user);

			$nodes = $sut->nodes;
			$this->assertIsA($nodes[0]->urlModel, 'app\Urls\UserDetailsUrlModel');
			$this->assertIdentical('a', $nodes[0]->name);
			$this->assertIsA($nodes[1]->urlModel, 'app\Urls\SharingSettingsUrlModel');
			$this->assertIsA($nodes[2]->urlModel, 'app\Urls\AuthLogoutUrlModel');
		}

	}

}