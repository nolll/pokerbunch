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
			userStorage = registerFake(ClassNames::$UserStorage);
			homegameStorage = registerFake(ClassNames::$HomegameStorage);
			webContext = registerFake(ClassNames::$WebContext);
			userContext = new UserContextImpl(webContext, userStorage, homegameStorage);
		}

		function test_GetToken_NoTokenExists_ReturnsNull(){
			$token = userContext.getToken();

			assertNull($token);
		}

		function test_GetToken_TokenExists_ReturnsToken(){
			setupToken();

			$token = userContext.getToken();

			assertIdentical('a token', $token);
		}

		function test_GetUser_NoTokenExists_ReturnsNullUser(){
			$user = userContext.getUser();

			assertNull($user);
		}

		function test_GetUser_TokenExistsButNoMatchingUser_ReturnsNull(){
			setupToken();
			setupNullUser();

			$user = userContext.getUser();

			assertNull($user);
		}

		function test_GetUser_TokenExistsAndMatchingUserExists_ReturnsUser(){
			setupToken();
			setupUser();

			$user = userContext.getUser();

			assertNotNull($user);
		}

		function test_GetUser_CalledTwice_TokenExists_DatabaseIsOnlyAskedOnce(){
			setupToken();
			setupNullUser();
			userStorage.expectOnce('getUserByToken');

			userContext.getUser();
			userContext.getUser();
		}

		function test_IsLoggedIn_TokenExistsButNoMatchingUser_ReturnsFalse(){
			setupToken();
			setupNullUser();

			$result = userContext.isLoggedIn();

			assertFalse($result);
		}

		function test_IsLoggedIn_TokenExistsAndMatchingUserExists_ReturnsTrue(){
			setupToken();
			setupUser();

			$result = userContext.isLoggedIn();

			assertTrue($result);
		}

		function test_IsAdmin_WithNonAdminUser_ReturnsFalse(){
			setupToken();
			setupUser();

			$result = userContext.isAdmin();

			assertFalse($result);
		}

		function test_IsAdmin_WithAdminUser_ReturnsTrue(){
			setupToken();
			$user = setupUser();
			$user.setGlobalRole(Role::$admin);

			$result = userContext.isAdmin();

			assertTrue($result);
		}

		function test_IsInRole_WithManagerRoleAndPlayerUser_ReturnsFalse(){
			setupToken();
			$user = setupUser();
			$homegame = new Homegame();
			homegameStorage.returns('getHomegameRole', Role::$player, array($homegame, $user.getId()));

			$result = userContext.isInRole($homegame, Role::$manager);

			assertFalse($result);
		}

		function test_IsInRole_WithPlayerRoleAndManagerUser_ReturnsTrue(){
			setupToken();
			$user = setupUser();
			$homegame = new Homegame();
			homegameStorage.returns('getHomegameRole', Role::$manager, array($homegame, $user));

			$result = userContext.isInRole($homegame, Role::$player);

			assertTrue($result);
		}

		function test_IsInRole_WithAdminRoleAndAdminUser_ReturnsTrue(){
			setupToken();
			$user = setupUser();
			$user.setGlobalRole(Role::$admin);
			$homegame = new Homegame();

			$result = userContext.isInRole($homegame, Role::$admin);

			assertTrue($result);
		}

		function setupToken(){
			webContext.returns('getCookie', 'a token', array('token'));
		}

		function setupNullUser(){
			userStorage.returns('getUserByToken', null);
			return null;
		}

		private function setupUser($user = null){
			if($user == null){
				$user = new User();
			}
			userStorage.returns('getUserByToken', $user);
			return $user;
        }

	}

}