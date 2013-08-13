<?php
namespace tests\CoreTests{

	use entities\Homegame;
	use tests\SharbatUnitTestCase;
	use core\ClassNames;
	use entities\Role;
	use Domain\Classes\User;
	use core\UserContextImpl;
	use tests\TestHelper;

	class UserContextTests extends SharbatUnitTestCase {

		/** @var UserContextImpl */
		private $userContext;
		private $webContext;
		private $userStorage;
		private $homegameStorage;

		function setUp(){
			parent::setUp();
			$this->userStorage = $this->registerFake(ClassNames::$UserStorage);
			$this->homegameStorage = $this->registerFake(ClassNames::$HomegameStorage);
			$this->webContext = $this->registerFake(ClassNames::$WebContext);
			$this->userContext = new UserContextImpl($this->webContext, $this->userStorage, $this->homegameStorage);
		}

		function test_GetToken_NoTokenExists_ReturnsNull(){
			$token = $this->userContext->getToken();

			$this->assertNull($token);
		}

		function test_GetToken_TokenExists_ReturnsToken(){
			$this->setupToken();

			$token = $this->userContext->getToken();

			$this->assertIdentical('a token', $token);
		}

		function test_GetUser_NoTokenExists_ReturnsNullUser(){
			$user = $this->userContext->getUser();

			$this->assertNull($user);
		}

		function test_GetUser_TokenExistsButNoMatchingUser_ReturnsNull(){
			$this->setupToken();
			$this->setupNullUser();

			$user = $this->userContext->getUser();

			$this->assertNull($user);
		}

		function test_GetUser_TokenExistsAndMatchingUserExists_ReturnsUser(){
			$this->setupToken();
			$this->setupUser();

			$user = $this->userContext->getUser();

			$this->assertNotNull($user);
		}

		function test_GetUser_CalledTwice_TokenExists_DatabaseIsOnlyAskedOnce(){
			$this->setupToken();
			$this->setupNullUser();
			$this->userStorage->expectOnce('getUserByToken');

			$this->userContext->getUser();
			$this->userContext->getUser();
		}

		function test_IsLoggedIn_TokenExistsButNoMatchingUser_ReturnsFalse(){
			$this->setupToken();
			$this->setupNullUser();

			$result = $this->userContext->isLoggedIn();

			$this->assertFalse($result);
		}

		function test_IsLoggedIn_TokenExistsAndMatchingUserExists_ReturnsTrue(){
			$this->setupToken();
			$this->setupUser();

			$result = $this->userContext->isLoggedIn();

			$this->assertTrue($result);
		}

		function test_IsAdmin_WithNonAdminUser_ReturnsFalse(){
			$this->setupToken();
			$this->setupUser();

			$result = $this->userContext->isAdmin();

			$this->assertFalse($result);
		}

		function test_IsAdmin_WithAdminUser_ReturnsTrue(){
			$this->setupToken();
			$user = $this->setupUser();
			$user->setGlobalRole(Role::$admin);

			$result = $this->userContext->isAdmin();

			$this->assertTrue($result);
		}

		function test_IsInRole_WithManagerRoleAndPlayerUser_ReturnsFalse(){
			$this->setupToken();
			$user = $this->setupUser();
			$homegame = new Homegame();
			$this->homegameStorage->returns('getHomegameRole', Role::$player, array($homegame, $user->getId()));

			$result = $this->userContext->isInRole($homegame, Role::$manager);

			$this->assertFalse($result);
		}

		function test_IsInRole_WithPlayerRoleAndManagerUser_ReturnsTrue(){
			$this->setupToken();
			$user = $this->setupUser();
			$homegame = new Homegame();
			$this->homegameStorage->returns('getHomegameRole', Role::$manager, array($homegame, $user));

			$result = $this->userContext->isInRole($homegame, Role::$player);

			$this->assertTrue($result);
		}

		function test_IsInRole_WithAdminRoleAndAdminUser_ReturnsTrue(){
			$this->setupToken();
			$user = $this->setupUser();
			$user->setGlobalRole(Role::$admin);
			$homegame = new Homegame();

			$result = $this->userContext->isInRole($homegame, Role::$admin);

			$this->assertTrue($result);
		}

		function setupToken(){
			$this->webContext->returns('getCookie', 'a token', array('token'));
		}

		function setupNullUser(){
			$this->userStorage->returns('getUserByToken', null);
			return null;
		}

		private function setupUser($user = null){
			if($user == null){
				$user = new User();
			}
			$this->userStorage->returns('getUserByToken', $user);
			return $user;
        }

	}

}