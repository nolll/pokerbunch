namespace app\User{

	use Domain\Classes\User;

	class UserFactoryImpl implements UserFactory{

		public function createUser($id, $userName, $displayName, $realName, $email, $globalRole){
			$user = new User();
			$user.setId($id);
			$user.setUserName($userName);
			$user.setDisplayName($displayName);
			$user.setRealName($realName);
			$user.setEmail($email);
			$user.setGlobalRole($globalRole);
			return $user;
		}

	}

}