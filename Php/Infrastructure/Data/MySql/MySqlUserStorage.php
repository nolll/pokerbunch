namespace Infrastructure\Data\MySql {

	use Infrastructure\Data\Interfaces\UserStorage;
	use Infrastructure\Data\MySql\StorageProvider;
	use Domain\Classes\User;
	use app\User\UserFactory;

	class MySqlUserStorage implements UserStorage {

		private $db;
		private $userFactory;

		public function __construct(StorageProvider $db,
									UserFactory $userFactory){
			$this->db = $db;
			$this->userFactory = $userFactory;
		}

		public static function className(){
			return __CLASS__;
		}

		public function getUserByEmail($email){
			$sql =	$this->getUserBaseSql();
			$sql .=	"WHERE u.Email = '{$email}'";
			return $this->getUser($sql);
		}

		public function getUserByName($userName){
			$sql =	$this->getUserBaseSql();
			$sql .=	"WHERE u.UserName = '{$userName}'";
			return $this->getUser($sql);
		}

		public function getUserByToken($token){
			$sql =	$this->getUserBaseSql();
			$sql .=	"WHERE u.Token = '{$token}'";
			return $this->getUser($sql);
		}

		public function getUserByCredentials($userNameOrEmail, $password){
			$sql = $this->getUserBaseSql();
			$sql .= "WHERE (u.UserName = '{$userNameOrEmail}' OR u.Email = '{$userNameOrEmail}') " .
					"AND u.Password = '{$password}'";
			return $this->getUser($sql);
		}

		private function getUserBaseSql(){
			$sql =	"SELECT u.UserID, u.UserName, u.DisplayName, u.RealName, u.Email, u.Token, u.Password, u.Salt, u.RoleID " .
					"FROM user u ";
			return $sql;
		}

		private function getUser($sql){
			$res = $this->db->query($sql);
			foreach($res->fetchAll() as $row){
				return $this->userFromDbRow($row);
			}
			return null;
		}

		public function getUsers(){
			$sql =	$this->getUserBaseSql();
			$sql .=	"ORDER BY u.DisplayName";
			$res = $this->db->query($sql);
			$users = array();
			foreach($res->fetchAll() as $row){
				$users[] = $this->userFromDbRow($row);
			}
			return $users;
		}

		/**
		 * @param User $user
		 * @return bool
		 */
		public function updateUser($user){
			$sql =	"UPDATE user u " .
					"SET " .
						"DisplayName = '{$user->getDisplayName()}', " .
						"RealName = '{$user->getRealName()}', " .
						"Email = '{$user->getEmail()}' " .
					"WHERE UserID = {$user->getId()}";
			$rowCount = $this->db->execute($sql);
			return $rowCount > 0;
		}

		/**
		 * @param User $user
		 * @return int
		 */
		public function addUser(User $user){
			$sql =	"INSERT INTO user " .
					"(UserName, DisplayName, Email) " .
					"VALUES " .
					"('{$user->getUserName()}', '{$user->getDisplayName()}', '{$user->getEmail()}')";
			$rowCount = $this->db->execute($sql);
			return $this->db->getLatestInsertId($rowCount > 0);
		}

		public function deleteUser(User $user){
			$sql =	"DELETE FROM user u " .
					"WHERE UserID = {$user->getId()}";
			$rowCount = $this->db->execute($sql);
			return $rowCount > 0;
		}

		public function getSalt($userNameOrEmail){
			$sql =	"SELECT u.Salt " .
					"FROM user u " .
					"WHERE (u.UserName = '{$userNameOrEmail}' OR u.Email = '{$userNameOrEmail}')";
			$res = $this->db->query($sql);
			$salt = "";
			foreach($res->fetchAll() as $row){
				$salt = $row["Salt"];
				break;
			}
			return $salt;
		}

		public function setSalt(User $user, $salt){
			$sql =	"UPDATE user u " .
				"SET " .
					"Salt = '{$salt}' " .
				"WHERE UserName = '{$user->getUserName()}'";
			$rowCount = $this->db->execute($sql);
			return $rowCount > 0;
		}

		public function setEncryptedPassword(User $user, $encryptedPassword){
			$sql =	"UPDATE user u " .
				"SET " .
					"Password = '{$encryptedPassword}' " .
				"WHERE UserName = '{$user->getUserName()}'";
			$rowCount = $this->db->execute($sql);
			return $rowCount > 0;
		}

		public function setToken(User $user, $token){
			$sql =	"UPDATE user u " .
				"SET " .
					"Token = '{$token}' " .
				"WHERE UserName = '{$user->getUserName()}'";
			$rowCount = $this->db->execute($sql);
			return $rowCount > 0;
		}

		public function getToken(User $user){
			$sql =	"SELECT u.Token " .
				"FROM user u " .
				"WHERE u.UserName = '{$user->getUserName()}'";
			$res = $this->db->query($sql);
			$token = null;
			foreach($res->fetchAll() as $row){
				$token = $row["Token"];
				break;
			}
			return $token;
		}

		public function userFromDbRow($row){
			$id = $row["UserID"];
			$userName = $row["UserName"];
			$displayName = $row["DisplayName"];
			$realName = $row["RealName"];
			$email = $row["Email"];
			$globalRole = $row["RoleID"];
			$user = $this->userFactory->createUser($id, $userName, $displayName, $realName, $email, $globalRole);
			return $user;
		}

	}

}