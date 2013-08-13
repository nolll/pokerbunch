namespace Infrastructure\Data\MySql {

	use Infrastructure\Data\Classes\RawHomegame;
	use Infrastructure\Data\Interfaces\HomegameStorage;
	use Infrastructure\Data\MySql\StorageProvider;
	use DateTimeZone;
	use Domain\Classes\User;
	use entities\Homegame;
	use entities\CurrencySettings;
	use entities\Role;

	class MySqlHomegameStorage implements HomegameStorage{

		private $db;

		public function __construct(StorageProvider $db){
			$this->db = $db;
		}

		public static function className(){
			return __CLASS__;
		}

		/**
		 * @return Homegame[]
		 */
		public function getHomegames(){
			$sql = $this->getHomegameBaseSql();
			$sql .= "ORDER BY h.DisplayName";
			return $this->getHomegamesFromSql($sql);
		}

		public function getHomegamesByRole($token, $role){
			$sql = $this->getHomegameBaseSql();
			$sql .= "INNER JOIN player p on h.HomegameID = p.HomegameID " .
					"INNER JOIN user u on p.UserID = u.UserID " .
					"WHERE u.Token = '{$token}' " .
					"AND p.RoleID >= {$role} " .
					"ORDER BY h.Name";
			return $this->getHomegamesFromSql($sql);
		}

		/**
		 * @param string $homegameName
		 * @return Homegame
		 */
		public function getHomegameByName($homegameName){
			$sql = $this->getHomegameBaseSql();
			$sql .= "WHERE Name = '{$homegameName}'";
			return $this->getHomegameFromSql($sql);
		}

		/**
		 * @param string $homegameName
		 * @return RawHomegame
		 */
		public function getRawHomegameByName($homegameName){
			$sql = $this->getHomegameBaseSql();
			$sql .= "WHERE Name = '{$homegameName}'";
			return $this->getRawHomegameFromSql($sql);
		}

		public function getHomegameRole(Homegame $homegame, User $user){
			$sql =	"SELECT p.RoleID " .
					"FROM player p " .
					"WHERE p.UserID = " . $user->getId() . " " .
					"AND p.HomegameID = " . $homegame->getId();
			return $this->getRoleFromSql($sql);
		}

		private function getHomegameBaseSql(){
			$sql =	"SELECT h.HomegameID, h.Name, h.DisplayName, h.Description, h.Currency, h.CurrencyLayout, h.Timezone, h.DefaultBuyin, h.CashgamesEnabled, h.TournamentsEnabled, h.VideosEnabled, h.HouseRules " .
					"FROM homegame h ";
			return $sql;
		}

		private function getHomegameFromSql($sql){
			$res = $this->db->query($sql);
			foreach($res->fetchAll() as $row){
				return $this->homegameFromDbRow($row);
			}
			return null;
		}

		private function getRawHomegameFromSql($sql){
			$res = $this->db->query($sql);
			foreach($res->fetchAll() as $row){
				return $this->rawHomegameFromDbRow($row);
			}
			return null;
		}

		private function getRoleFromSql($sql){
			$res = $this->db->query($sql);
			$role = Role::$guest;
			foreach($res->fetchAll() as $row){
				$role = $this->roleFromDbRow($row);
				break;
			}
			return $role;
		}

		private function getHomegamesFromSql($sql){
			$res = $this->db->query($sql);
			$homegames = array();
			foreach($res->fetchAll() as $row){
				$homegames[] = $this->homegameFromDbRow($row);
			}
			return $homegames;
		}

		/**
		 * @param Homegame $homegame
		 * @return Homegame
		 */
		public function addHomegame(Homegame $homegame){
			$currency = $homegame->getCurrency();
			$sql =	"INSERT INTO homegame " .
					"(Name, DisplayName, Description, Currency, CurrencyLayout, Timezone, DefaultBuyin, CashgamesEnabled, TournamentsEnabled, VideosEnabled, HouseRules) " .
					"VALUES " .
					"('{$homegame->getSlug()}', '{$homegame->getDisplayName()}', '{$homegame->getDescription()}', '{$currency->getSymbol()}', '{$currency->getLayout()}', '{$homegame->getTimezone()->getName()}', 0, {$this->db->boolToInt($homegame->cashgamesEnabled)}, {$this->db->boolToInt($homegame->tournamentsEnabled)}, {$this->db->boolToInt($homegame->videosEnabled)}, '{$homegame->getHouseRules()}')";
			$rowCount = $this->db->execute($sql);
			$homegame->setId($this->db->getLatestInsertId($rowCount > 0));
			return $homegame;
		}

		/**
		 * @param Homegame $homegame
		 * @return bool
		 */
		public function updateHomegame(Homegame $homegame){
			$currency = $homegame->getCurrency();
			$sql =	"UPDATE homegame " .
				"SET " .
				"Name = '{$homegame->getSlug()}', " .
				"DisplayName = '{$homegame->getDisplayName()}', " .
				"Description = '{$homegame->getDescription()}', " .
				"HouseRules = '{$homegame->getHouseRules()}', " .
				"Currency = '{$currency->getSymbol()}', " .
				"CurrencyLayout = '{$currency->getLayout()}', " .
				"Timezone = '{$homegame->getTimezone()->getName()}', " .
				"DefaultBuyin = {$homegame->getDefaultBuyin()}, " .
				"CashgamesEnabled = {$this->db->boolToInt($homegame->cashgamesEnabled)}, " .
				"TournamentsEnabled = {$this->db->boolToInt($homegame->tournamentsEnabled)}, " .
				"VideosEnabled = {$this->db->boolToInt($homegame->videosEnabled)} " .
				"WHERE HomegameID = {$homegame->getId()}";
			$rowCount = $this->db->execute($sql);
			return $rowCount > 0;
		}

		/**
		 * @param Homegame $homegame
		 * @return bool
		 */
		public function deleteHomegame(Homegame $homegame){
			$sql =	"DELETE FROM homegame " .
					"WHERE Name = '{$homegame->getSlug()}'";
			$rowCount = $this->db->execute($sql);
			return $rowCount > 0;
		}

		private function homegameFromDbRow($row){
			$homegame = new Homegame();
			$homegame->setId($row["HomegameID"]);
			$homegame->setSlug($row["Name"]);
			$homegame->setDisplayName($row["DisplayName"]);
			$homegame->setDescription($row["Description"]);
			$homegame->setHouseRules($row["HouseRules"]);
			$homegame->setCurrency(new CurrencySettings($row["Currency"], $row["CurrencyLayout"]));
			$homegame->setTimezone(new DateTimeZone($row["Timezone"]));
			$homegame->setDefaultBuyin($row["DefaultBuyin"]);
			$homegame->cashgamesEnabled = $row["CashgamesEnabled"] == "1" ? true : false;
			$homegame->tournamentsEnabled = $row["TournamentsEnabled"] == "1" ? true : false;
			$homegame->videosEnabled = $row["VideosEnabled"] == "1" ? true : false;
			return $homegame;
		}

		private function rawHomegameFromDbRow($row){
			$homegame = new RawHomegame();
			$homegame->setId($row["HomegameID"]);
			$homegame->setSlug($row["Name"]);
			$homegame->setDisplayName($row["DisplayName"]);
			$homegame->setDescription($row["Description"]);
			$homegame->setHouseRules($row["HouseRules"]);
			$homegame->setCurrencyLayout($row["CurrencyLayout"]);
			$homegame->setCurrencySymbol($row["Currency"]);
			$homegame->setTimezoneName($row["Timezone"]);
			$homegame->setDefaultBuyin($row["DefaultBuyin"]);
			$homegame->cashgamesEnabled = $row["CashgamesEnabled"] == "1" ? true : false;
			$homegame->tournamentsEnabled = $row["TournamentsEnabled"] == "1" ? true : false;
			$homegame->videosEnabled = $row["VideosEnabled"] == "1" ? true : false;
			return $homegame;
		}

		private function roleFromDbRow($row){
			return $row["RoleID"];
		}

	}

}