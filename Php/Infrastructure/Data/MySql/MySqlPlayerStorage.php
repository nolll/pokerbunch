namespace Infrastructure\Data\MySql {

	use entities\PlayerFactory;
	use Infrastructure\Data\Interfaces\PlayerStorage;
	use Infrastructure\Data\MySql\StorageProvider;
	use entities\Role;
	use Domain\Classes\User;
	use entities\Player;
	use entities\Homegame;

	class MySqlPlayerStorage implements PlayerStorage{

		private $db;
		private $playerFactory;

		public function __construct(StorageProvider $db,
									PlayerFactory $playerFactory){
			db = $db;
			playerFactory = $playerFactory;
		}

		public function getPlayerById(Homegame $homegame, $id){
			$sql = getPlayersBaseSql($homegame);
			$sql .= "AND p.PlayerID = {$id}";
			return getPlayerFromSql($sql);
		}

		public function getPlayerByName(Homegame $homegame, $name){
			$sql = getPlayersBaseSql($homegame);
			$sql .= "AND (p.PlayerName = '{$name}' OR u.DisplayName = '{$name}')";
			return getPlayerFromSql($sql);
		}

		public function getPlayerByUserName(Homegame $homegame, $userName){
			$sql = getPlayersBaseSql($homegame);
			$sql .= "AND u.UserName = '{$userName}'";
			return getPlayerFromSql($sql);
		}

		public function getPlayers(Homegame $homegame){
			$sql = getPlayersBaseSql($homegame);
			$sql .= "ORDER BY DisplayName";
			return getPlayersFromSql($sql);
		}

		private function getPlayersBaseSql(Homegame $homegame){
			$sql =	"SELECT p.HomegameID, p.PlayerID, p.UserID, p.RoleID, COALESCE(p.PlayerName, u.DisplayName) AS DisplayName, u.UserName, u.Email " .
					"FROM player p " .
					"LEFT JOIN user u on p.UserID = u.UserID " .
					"WHERE p.HomegameID = {$homegame.getId()} ";
			return $sql;
		}

		public function addPlayer(Homegame $homegame, $playerName){
			$role = Role::$player;
			$sql =	"INSERT INTO player " .
					"(HomegameID, RoleID, Approved, PlayerName) " .
					"VALUES " .
					"({$homegame.getId()}, {$role}, 1, '$playerName')";
			$rowCount = db.execute($sql);
			return db.getLatestInsertId($rowCount > 0);
		}

		public function addPlayerWithUser(Homegame $homegame, User $user, $role){
			$sql =	"INSERT INTO player " .
					"(HomegameID, UserID, RoleID, Approved) " .
					"VALUES " .
					"({$homegame.getId()}, {$user.getId()}, {$role}, 1)";
			$rowCount = db.execute($sql);
			return db.getLatestInsertId($rowCount > 0);
		}

		public function joinHomegame(Player $player, Homegame $homegame, User $user){
			$sql =	"UPDATE player " .
					"SET " .
						"HomegameID = {$homegame.getId()}, " .
						"PlayerName = NULL, " .
						"UserID = {$user.getId()}, " .
						"RoleID = {$player.getRole()}, " .
						"Approved = 1 " .
					"WHERE PlayerID = {$player.getId()}";
			$rowCount = db.execute($sql);
			return $rowCount > 0;
		}

		public function deletePlayer(Player $player){
			$sql =	"DELETE FROM player " .
					"WHERE PlayerID = {$player.getId()}";
			$rowCount = db.execute($sql);
			return $rowCount > 0;
		}

		private function getPlayerFromSql($sql){
			$res = db.query($sql);
			$player = null;
			foreach($res.fetchAll() as $row){
				$player = playerFromDbRow($row);
				break;
			}
			return $player;
		}

		private function getPlayersFromSql($sql){
			$res = db.query($sql);
			$players = array();
			foreach($res.fetchAll() as $row){
				$players[] = playerFromDbRow($row);
			}
			return $players;
		}

		private function playerFromDbRow($row){
			$userName = isset($row["UserName"]) ? $row["UserName"] : null;
			return playerFactory.create($row["DisplayName"], $row["RoleID"], $userName, $row["PlayerID"]);
		}

	}

}