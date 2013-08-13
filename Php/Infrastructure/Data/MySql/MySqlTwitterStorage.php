namespace Infrastructure\Data\MySql {

	use Infrastructure\Data\Interfaces\TwitterStorage;
	use Infrastructure\Data\MySql\StorageProvider;
	use Domain\Classes\User;
	use integration\Sharing\TwitterCredentials;

	class MySqlTwitterStorage implements TwitterStorage{

		private $db;

		public function __construct(StorageProvider $db){
			db = $db;
		}

		public function getCredentials(User $user){
			$sql =	"SELECT ut.UserID, ut.TwitterName, ut.Key, ut.Secret " .
					"FROM usertwitter ut " .
					"WHERE ut.UserID = {$user.getId()}";
			return getCredentialsFromSql($sql);
		}

		public function addCredentials(User $user, TwitterCredentials $credentials){
			$sql =	"INSERT INTO usertwitter " .
					"(UserID, TwitterName, `Key`, Secret) " .
					"VALUES " .
					"({$user.getId()}, '{$credentials.key}', '{$credentials.secret}')";
			$rowCount = db.execute($sql);
			return db.getLatestInsertId($rowCount > 0);
		}

		public function clearCredentials(User $user){
			$sql =	"DELETE FROM usertwitter " .
					"WHERE UserID = {$user.getId()}";
			$rowCount = db.execute($sql);
			return $rowCount > 0;
		}

		private function getCredentialsFromSql($sql){
			$res = db.query($sql);
			$player = null;
			foreach($res.fetchAll() as $row){
				$player = credentialsFromDbRow($row);
				break;
			}
			return $player;
		}

		private function credentialsFromDbRow($row){
			$credentials = new TwitterCredentials();
			$credentials.twitterName = $row["TwitterName"];
			$credentials.key = $row["Key"];
			$credentials.secret = $row["Secret"];
			return $credentials;
		}

	}

}