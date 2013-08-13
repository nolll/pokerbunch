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
			globalRole = Role::$none;
		}

		public function getId(){
			return id;
		}

		public function setId($id){
			id = $id;
		}

		public function getUserName(){
			return userName;
		}

		public function setUserName($userName){
			userName = $userName;
		}

		public function getDisplayName(){
			return displayName;
		}

		public function setDisplayName($displayName){
			displayName = $displayName;
		}

		public function getRealName(){
			return realName;
		}

		public function setRealName($realName){
			realName = $realName;
		}

		public function getEmail(){
			return email;
		}

		public function setEmail($email){
			email = $email;
		}

		public function getGlobalRole(){
			return globalRole;
		}

		public function setGlobalRole($globalRole){
			globalRole = $globalRole;
		}

		public function isAdmin(){
			return globalRole == Role::$admin;
		}

	}

}