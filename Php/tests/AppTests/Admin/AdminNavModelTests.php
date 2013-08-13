<?php
namespace tests\AppTests\Admin{

	use tests\UnitTestCase;
	use Domain\Classes\User;
	use entities\Role;
	use app\Admin\AdminNavModel;
	use tests\TestHelper;

	class AdminNavModelTests extends UnitTestCase {

		function test_Show_AdminUser_DefaultContentSet(){
			$user = new User();
			$user->setGlobalRole(Role::$admin);
			$sut = new AdminNavModel($user);

			$this->assertIdentical("Admin", $sut->heading);
			$this->assertIdentical("admin-nav", $sut->cssClass);
		}

		function test_Show_NotLoggedIn_NoNodes(){
			$sut = new AdminNavModel();

			$nodes = $sut->nodes;
			$this->assertIdentical(0, count($nodes));
		}

		function test_Show_WithNonAdminUser_NoNodes(){
			$user = new User();
			$sut = new AdminNavModel($user);

			$nodes = $sut->nodes;
			$this->assertIdentical(0, count($nodes));
		}

		function test_Show_WithAdminUser_SetsNodes(){
			$user = new User();
			$user->setGlobalRole(Role::$admin);
			$sut = new AdminNavModel($user);

			$nodes = $sut->nodes;
			$this->assertIsA($nodes[0]->urlModel, 'app\Urls\HomegameListingUrlModel');
			$this->assertIsA($nodes[1]->urlModel, 'app\Urls\UserListingUrlModel');
		}

	}

}