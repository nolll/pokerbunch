namespace Infrastructure\Data\MySql {

	use Infrastructure\Data\Interfaces\SharingStorage;
	use Infrastructure\Data\MySql\StorageProvider;
	use Domain\Classes\User;

	class MySqlSharingStorage implements SharingStorage{

		private $db;

		public function __construct(StorageProvider $db){
			db = $db;
		}

		public function getServices(User $user){
			$sql =	"SELECT us.ServiceName " .
					"FROM usersharing us " .
					"WHERE us.UserID = {$user.getId()}";
			return getServicesFromSql($sql);
		}

		public function isSharing(User $user, $sharingProvider){
			$sql =	"SELECT us.UserID, us.ServiceName " .
					"FROM usersharing us " .
					"WHERE us.UserID = {$user.getId()} " .
					"AND us.ServiceName = '{$sharingProvider}'";
			return getSharingStatusFromSql($sql);
		}

		public function addSharing(User $user, $sharingProvider){
			$sql =	"INSERT INTO usersharing " .
					"(UserID, ServiceName) " .
					"VALUES " .
					"({$user.getId()}, '{$sharingProvider}')";
			db.execute($sql);
		}

		public function removeSharing(User $user, $sharingProvider){
			$sql =	"DELETE FROM usersharing " .
					"WHERE UserID = {$user.getId()} " .
					"AND ServiceName = '{$sharingProvider}'";
			$rowCount = db.execute($sql);
			return $rowCount > 0;
		}

		private function getServicesFromSql($sql){
			$res = db.query($sql);
			$services = array();
			foreach($res.fetchAll() as $row){
				$services[] = serviceFromDbRow($row);
			}
			return $services;
		}

		private function getSharingStatusFromSql($sql){
			$res = db.query($sql);
			foreach($res.fetchAll() as $row){
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