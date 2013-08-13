<?php
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
			$this->webContext = $webContext;
			$this->userStorage = $userStorage;
			$this->homegameStorage = $homegameStorage;
			$this->fetchedUser = false;
		}

		/**
		 * @return User
		 */

		public function getUser(){
			if(!$this->fetchedUser){
				$token = $this->getToken();
				if($token != null){
					$this->user = $this->userStorage->getUserByToken($token);
				}
				$this->fetchedUser = true;
			}
			return $this->user;
		}

		public function isLoggedIn(){
			$user = $this->getUser();
			return $user != null;
		}

		public function getToken(){
			return $this->webContext->getCookie('token');
		}

		public function getRole(Homegame $homegame){
			return $this->homegameStorage->getHomegameRole($homegame, $this->getUser());
		}

		public function isInRole(Homegame $homegame, $roleToCheck){
			if($this->isAdmin()){
				return true;
			}
			$role = $this->getRole($homegame);
			if($role >= $roleToCheck){
				return true;
			}
			return false;
		}

		public function isGuest(Homegame $homegame){
			return $this->isInRole($homegame, Role::$guest);
		}

		public function isPlayer(Homegame $homegame){
			return $this->isInRole($homegame, Role::$player);
		}

		public function isManager(Homegame $homegame){
			return $this->isInRole($homegame, Role::$manager);
		}

		public function isAdmin(){
			return $this->getUser()->isAdmin();
		}

		public function requireUser(){
			if(!$this->isLoggedIn()){
				throw new NotLoggedInException();
			}
		}

		public function requireRole(Homegame $homegame, $role){
			$this->requireUser();
			if(!$this->isInRole($homegame, $role)){
				throw new AccessDeniedException();
			}
		}

		public function requirePlayer(Homegame $homegame){
			$this->requireUser();
			if(!$this->isPlayer($homegame)){
				throw new AccessDeniedException();
			}
		}

		public function requireManager(Homegame $homegame){
			$this->requireUser();
			if(!$this->isManager($homegame)){
				throw new AccessDeniedException();
			}
		}

		public function requireAdmin(){
			$this->requireUser();
			if(!$this->isAdmin()){
				throw new AccessDeniedException();
			}
		}

	}

}