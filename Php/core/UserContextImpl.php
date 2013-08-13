namespace core{

	use exceptions\AccessDeniedException;
	use exceptions\NotLoggedInException;
	use Infrastructure\Data\Interfaces\UserStorage;
	use Domain\Classes\User;
	use Infrastructure\Data\Interfaces\HomegameStorage;
	use entities\Homegame;
	use entities\Role;

	class UserContextImpl implements UserContext{

		private $webContext;
		private $userStorage;
		private $homegameStorage;
		private $user;
		private $fetchedUser;

		public function __construct(WebContext $webContext,
									UserStorage $userStorage,
									HomegameStorage $homegameStorage){
			webContext = $webContext;
			userStorage = $userStorage;
			homegameStorage = $homegameStorage;
			fetchedUser = false;
		}

		/**
		 * @return User
		 */

		public function getUser(){
			if(!fetchedUser){
				$token = getToken();
				if($token != null){
					user = userStorage.getUserByToken($token);
				}
				fetchedUser = true;
			}
			return user;
		}

		public function isLoggedIn(){
			$user = getUser();
			return $user != null;
		}

		public function getToken(){
			return webContext.getCookie('token');
		}

		public function getRole(Homegame $homegame){
			return homegameStorage.getHomegameRole($homegame, getUser());
		}

		public function isInRole(Homegame $homegame, $roleToCheck){
			if(isAdmin()){
				return true;
			}
			$role = getRole($homegame);
			if($role >= $roleToCheck){
				return true;
			}
			return false;
		}

		public function isGuest(Homegame $homegame){
			return isInRole($homegame, Role::$guest);
		}

		public function isPlayer(Homegame $homegame){
			return isInRole($homegame, Role::$player);
		}

		public function isManager(Homegame $homegame){
			return isInRole($homegame, Role::$manager);
		}

		public function isAdmin(){
			return getUser().isAdmin();
		}

		public function requireUser(){
			if(!isLoggedIn()){
				throw new NotLoggedInException();
			}
		}

		public function requireRole(Homegame $homegame, $role){
			requireUser();
			if(!isInRole($homegame, $role)){
				throw new AccessDeniedException();
			}
		}

		public function requirePlayer(Homegame $homegame){
			requireUser();
			if(!isPlayer($homegame)){
				throw new AccessDeniedException();
			}
		}

		public function requireManager(Homegame $homegame){
			requireUser();
			if(!isManager($homegame)){
				throw new AccessDeniedException();
			}
		}

		public function requireAdmin(){
			requireUser();
			if(!isAdmin()){
				throw new AccessDeniedException();
			}
		}

	}

}