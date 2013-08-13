<?php
namespace tests\AppTests\User{

	use Domain\Classes\User;
	use tests\UnitTestCase;
	use entities\Role;
	use app\User\Details\UserDetailsModel;
	use core\ClassNames;
	use tests\TestHelper;

	class UserDetailsModelTests extends UnitTestCase {

		private $avatarService;

		function setUp(){
			$this->avatarService = TestHelper::getFake(ClassNames::$AvatarService);
		}

		function test_ActionDetails_SetsUserData(){
			$displayUser = new User();
			$displayUser->setUserName('a');
			$displayUser->setDisplayName('b');
			$displayUser->setRealName('c');
			$displayUser->setEmail('d');

			$currentUser = $displayUser;

			$sut = new UserDetailsModel($currentUser, $displayUser, $this->avatarService);

			$this->assertIdentical('a', $sut->userName);
			$this->assertIdentical('b', $sut->displayName);
			$this->assertIdentical('c', $sut->realName);
			$this->assertIdentical('d', $sut->email);
		}

		function test_ActionDetails_SetsAvatarModel(){
			$displayUser = new User();
			$currentUser = $displayUser;

			$sut = new UserDetailsModel($currentUser, $displayUser, $this->avatarService);

			$this->assertIsA($sut->avatarModel, 'app\User\Avatar\AvatarModel');
		}

		function test_ActionDetails_ViewOwnUser_OutputsEditLink(){
			$displayUser = new User();
			$currentUser = $displayUser;

			$sut = new UserDetailsModel($currentUser, $displayUser, $this->avatarService);

			$this->assertTrue($sut->showEditLink);
			$this->assertIsA($sut->editLink, 'app\Urls\UserEditUrlModel');
		}

		function test_ActionDetails_ViewOtherUserWithAdminUser_OutputsEditLink(){
			$displayUser = new User();
			$currentUser = new User();
			$currentUser->setGlobalRole(Role::$admin);
			$currentUser->setUserName('differentUserName');

			$sut = new UserDetailsModel($currentUser, $displayUser, $this->avatarService);

			$this->assertTrue($sut->showEditLink);
			$this->assertIsA($sut->editLink, 'app\Urls\UserEditUrlModel');
		}

		function test_ActionDetails_ViewOwnUser_OutputsChangePasswordLink(){
			$displayUser = new User();
			$currentUser = $displayUser;

			$sut = new UserDetailsModel($currentUser, $displayUser, $this->avatarService);

			$this->assertTrue($sut->showPasswordLink);
			$this->assertIsA($sut->changePasswordLink, 'app\Urls\ChangePasswordUrlModel');
		}

		function test_ActionDetails_ViewOtherUser_DoesNotOutputEditLink(){
			$currentUser = new User();
			$displayUser = new User();
			$currentUser->setUserName('differentUserName');

			$sut = new UserDetailsModel($currentUser, $displayUser, $this->avatarService);

			$this->assertFalse($sut->showEditLink);
		}

		function test_ActionDetails_ViewOtherUserWithAdminUser_DoesNotOutputPasswordLink(){
			$displayUser = new User();
			$currentUser = new User();
			$currentUser->setGlobalRole(Role::$admin);
			$currentUser->setUserName('differentUserName');

			$sut = new UserDetailsModel($currentUser, $displayUser, $this->avatarService);

			$this->assertFalse($sut->showPasswordLink);
		}

	}

}