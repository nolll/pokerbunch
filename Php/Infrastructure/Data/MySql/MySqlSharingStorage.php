namespace Infrastructure\Data\MySql {

	use Infrastructure\Data\Interfaces\SharingStorage;
	use Infrastructure\Data\MySql\StorageProvider;
	use Domain\Classes\User;

	class MySqlSharingStorage implements SharingStorage{

		private $db;

		public function __construct(StorageProvider $db){
			$this->db = $db;
		}

		public function getServices(User $user){
			$sql =	"SELECT us.ServiceName " .
					"FROM usersharing us " .
					"WHERE us.UserID = {$user->getId()}";
			return $this->getServicesFromSql($sql);
		}

		public function isSharing(User $user, $sharingProvider){
			$sql =	"SELECT us.UserID, us.ServiceName " .
					"FROM usersharing us " .
					"WHERE us.UserID = {$user->getId()} " .
					"AND us.ServiceName = '{$sharingProvider}'";
			return $this->getSharingStatusFromSql($sql);
		}

		public function addSharing(User $user, $sharingProvider){
			$sql =	"INSERT INTO usersharing " .
					"(UserID, ServiceName) " .
					"VALUES " .
					"({$user->getId()}, '{$sharingProvider}')";
			$this->db->execute($sql);
		}

		public function removeSharing(User $user, $sharingProvider){
			$sql =	"DELETE FROM usersharing " .
					"WHERE UserID = {$user->getId()} " .
					"AND ServiceName = '{$sharingProvider}'";
			$rowCount = $this->db->execute($sql);
			return $rowCount > 0;
		}

		private function getServicesFromSql($sql){
			$res = $this->db->query($sql);
			$services = array();
			foreach($res->fetchAll() as $row){
				$services[] = $this->serviceFromDbRow($row);
			}
			return $services;
		}

		private function getSharingStatusFromSql($sql){
			$res = $this->db->query($sql);
			foreach($res->fetchAll() as $row){
				return true;
				break;
			}
			return false;
		}

		private function serviceFromDbRow($row){
			return $row["ServiceName"];
		}

	}

}