<?php
namespace tests{

	use entities\GameStatus;
	use Domain\Classes\User;
	use core\UserContext;
	use Exception;
	use Mishiin\Request;
	use Mock;

	importlib('/SimpleTest/mock_objects.php');

	class TestHelper{

		public static function setupNullUser(UserContext $userContext){
			$userContext->returns('isLoggedIn', false);
			$userContext->returns('getUser', null);
			return null;
		}

		public static function setupUser(UserContext $userContext){
			$user = new User();
			$user->setUserName('user1');
			$userContext->returns('isLoggedIn', true);
			$userContext->returns('getUser', $user);
			return $user;
		}

		public static function setupUserWithPlayerRights(UserContext $userContext){
			$user = self::setupUser($userContext);
			$userContext->returns('isGuest', true);
			$userContext->returns('isPlayer', true);
			return $user;
		}

		public static function setupUserWithManagerRights(UserContext $userContext){
			$user = self::setupUser($userContext);
			$userContext->returns('isGuest', true);
			$userContext->returns('isPlayer', true);
			$userContext->returns('isManager', true);
			return $user;
		}

		public static function setupPostParameter(Request $request, $paramName, $paramValue){
			self::setupRequestParameter($request, 'getParamPost', $paramName, $paramValue);
		}

		private static function setupRequestParameter(Request $request, $methodName, $paramName, $paramValue){
			$request->returns($methodName, $paramValue, array($paramName));
		}

		public static function getFakeFromClassBinding(ClassBinding $classBinding){
			$interfaceName = $classBinding->getInterfaceName();
			$mockClassName = $classBinding->getMockClassName();
			if(self::classLoaded($interfaceName)){
				Mock::generate($interfaceName, $mockClassName);
				return new $mockClassName();
			} else {
				$message = 'The class ' . $interfaceName . ' could not be loaded.';
				throw new Exception($message);
			}
		}

		public static function getFake($className){
			$classBinding = new ClassBinding($className);
			return self::getFakeFromClassBinding($classBinding);
		}

		public static function classLoaded($className){
			return class_exists($className, true) || interface_exists($className, true);
		}

	}

}