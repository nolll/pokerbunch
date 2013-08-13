<?php
namespace tests\AppTests\User{

	use app\User\Listing\UserListingController;
	use core\ClassNames;
	use tests\TestHelper;
	use tests\UnitTestCase;

	class UserListingControllerTests extends UnitTestCase {

		/** @var UserListingController */
		private $sut;
		private $userContext;

		function setUp(){
			$userStorage = TestHelper::getFake(ClassNames::$UserStorage);
			$this->userContext = TestHelper::getFake(ClassNames::$UserContext);
			$this->sut = new UserListingController($this->userContext, $userStorage);
		}

		function test_ActionIndex_NotAuthorized_ThrowsException(){
			$this->userContext->throwOn('requireAdmin');
			$this->expectException();

			$this->sut->action_listing();
		}

	}

}