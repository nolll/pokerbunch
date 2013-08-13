<?php
namespace Domain\Classes {

	use entities\Role;

	class User{

		private $id;
		private $userName;
		private $displayName;
		private $realName;
		private $email;
		private $globalRole;

		public function __construct(){
			$this->globalRole = Role::$none;
		}

		public function getId(){
			return $this->id;
		}

		public function setId($id){
			$this->id = $id;
		}

		public function getUserName(){
			return $this->userName;
		}

		public function setUserName($userName){
			$this->userName = $userName;
		}

		public function getDisplayName(){
			return $this->displayName;
		}

		public function setDisplayName($displayName){
			$this->displayName = $displayName;
		}

		public function getRealName(){
			return $this->realName;
		}

		public function setRealName($realName){
			$this->realName = $realName;
		}

		public function getEmail(){
			return $this->email;
		}

		public function setEmail($email){
			$this->email = $email;
		}

		public function getGlobalRole(){
			return $this->globalRole;
		}

		public function setGlobalRole($globalRole){
			$this->globalRole = $globalRole;
		}

		public function isAdmin(){
			return $this->globalRole == Role::$admin;
		}

	}

}