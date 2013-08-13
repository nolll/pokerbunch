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
			$this->db = $db;
			$this->playerFactory = $playerFactory;
		}

		public function getPlayerById(Homegame $homegame, $id){
			$sql = $this->getPlayersBaseSql($homegame);
			$sql .= "AND p.PlayerID = {$id}";
			return $this->getPlayerFromSql($sql);
		}

		public function getPlayerByName(Homegame $homegame, $name){
			$sql = $this->getPlayersBaseSql($homegame);
			$sql .= "AND (p.PlayerName = '{$name}' OR u.DisplayName = '{$name}')";
			return $this->getPlayerFromSql($sql);
		}

		public function getPlayerByUserName(Homegame $homegame, $userName){
			$sql = $this->getPlayersBaseSql($homegame);
			$sql .= "AND u.UserName = '{$userName}'";
			return $this->getPlayerFromSql($sql);
		}

		public function getPlayers(Homegame $homegame){
			$sql = $this->getPlayersBaseSql($homegame);
			$sql .= "ORDER BY DisplayName";
			return $this->getPlayersFromSql($sql);
		}

		private function getPlayersBaseSql(Homegame $homegame){
			$sql =	"SELECT p.HomegameID, p.PlayerID, p.UserID, p.RoleID, COALESCE(p.PlayerName, u.DisplayName) AS DisplayName, u.UserName, u.Email " .
					"FROM player p " .
					"LEFT JOIN user u on p.UserID = u.UserID " .
					"WHERE p.HomegameID = {$homegame->getId()} ";
			return $sql;
		}

		public function addPlayer(Homegame $homegame, $playerName){
			$role = Role::$player;
			$sql =	"INSERT INTO player " .
					"(HomegameID, RoleID, Approved, PlayerName) " .
					"VALUES " .
					"({$homegame->getId()}, {$role}, 1, '$playerName')";
			$rowCount = $this->db->execute($sql);
			return $this->db->getLatestInsertId($rowCount > 0);
		}

		public function addPlayerWithUser(Homegame $homegame, User $user, $role){
			$sql =	"INSERT INTO player " .
					"(HomegameID, UserID, RoleID, Approved) " .
					"VALUES " .
					"({$homegame->getId()}, {$user->getId()}, {$role}, 1)";
			$rowCount = $this->db->execute($sql);
			return $this->db->getLatestInsertId($rowCount > 0);
		}

		public function joinHomegame(Player $player, Homegame $homegame, User $user){
			$sql =	"UPDATE player " .
					"SET " .
						"HomegameID = {$homegame->getId()}, " .
						"PlayerName = NULL, " .
						"UserID = {$user->getId()}, " .
						"RoleID = {$player->getRole()}, " .
						"Approved = 1 " .
					"WHERE PlayerID = {$player->getId()}";
			$rowCount = $this->db->execute($sql);
			return $rowCount > 0;
		}

		public function deletePlayer(Player $player){
			$sql =	"DELETE FROM player " .
					"WHERE PlayerID = {$player->getId()}";
			$rowCount = $this->db->execute($sql);
			return $rowCount > 0;
		}

		private function getPlayerFromSql($sql){
			$res = $this->db->query($sql);
			$player = null;
			foreach($res->fetchAll() as $row){
				$player = $this->playerFromDbRow($row);
				break;
			}
			return $player;
		}

		private function getPlayersFromSql($sql){
			$res = $this->db->query($sql);
			$players = array();
			foreach($res->fetchAll() as $row){
				$players[] = $this->playerFromDbRow($row);
			}
			return $players;
		}

		private function playerFromDbRow($row){
			$userName = isset($row["UserName"]) ? $row["UserName"] : null;
			return $this->playerFactory->create($row["DisplayName"], $row["RoleID"], $userName, $row["PlayerID"]);
		}

	}

}