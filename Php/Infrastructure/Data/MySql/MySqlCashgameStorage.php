<?php
namespace Infrastructure\Data\MySql {

	use Infrastructure\Data\Classes\RawCashgame;
	use Infrastructure\Data\Classes\RawCashgameResult;
	use Infrastructure\Data\Interfaces\CashgameStorage;
	use Infrastructure\Data\MySql\PreparedStatement;
	use Infrastructure\Data\MySql\StorageProvider;
	use entities\Checkpoints\ReportCheckpoint;
	use DateTimeZone;
	use core\DateTimeFactory;
	use core\Globalization;
    use entities\Checkpoints\CashoutCheckpoint;
	use entities\Checkpoints\BuyinCheckpoint;
	use entities\Checkpoints\Checkpoint;
	use entities\GameStatus;
	use entities\Homegame;
	use entities\Cashgame;
	use DateTime;
	use entities\Player;

	class MySqlCashgameStorage implements CashgameStorage {

		private $db;

		public function __construct(StorageProvider $db){
			$this->db = $db;
		}

		/**
		 * @param Homegame $homegame
		 * @param Cashgame $cashgame
		 * @return int
		 */
		public function addGame(Homegame $homegame, Cashgame $cashgame){
			$sql =	"INSERT INTO game " .
					"(HomegameID, Location, Status) " .
					"VALUES " .
					"({$homegame->getId()}, '{$cashgame->getLocation()}', {$cashgame->getStatus()})";
			$rowCount = $this->db->execute($sql);
			return $this->db->getLatestInsertId($rowCount > 0);
		}

		public function deleteGame(Cashgame $cashgame){
			$sql =	"DELETE FROM game " .
					"WHERE GameID = {$cashgame->getId()}";
			$rowCount = $this->db->execute($sql);
			return $rowCount > 0;
		}

		public function addCheckpoint(Cashgame $cashgame, Player $player, Checkpoint $checkpoint){
			$timestampStr = Globalization::formatIsoDateTime(DateTimeFactory::toUtc($checkpoint->getTimestamp()));
			$sql =	"INSERT INTO cashgamecheckpoint " .
					"(GameID, PlayerID, Type, Amount, Stack, Timestamp) " .
					"VALUES " .
					"({$cashgame->getId()}, {$player->getId()}, {$checkpoint->getType()}, '{$checkpoint->getAmount()}', '{$checkpoint->getStack()}', '{$timestampStr}')";
			$rowCount = $this->db->execute($sql);
			return $this->db->getLatestInsertId($rowCount > 0);
		}

		/**
		 * @param Checkpoint $checkpoint
		 * @return bool
		 */
		public function updateCheckpoint(Checkpoint $checkpoint){
			$sql =	"UPDATE cashgamecheckpoint " .
					"SET " .
					"Amount = {$checkpoint->getAmount()}, " .
					"Stack = {$checkpoint->getStack()} " .
					"WHERE CheckpointID = {$checkpoint->getId()}";
			$rowCount = $this->db->execute($sql);
			return $rowCount > 0;
		}

		/**
		 * @param $id
		 * @return bool
		 */
		public function deleteCheckpoint($id){
			$sql =	"DELETE FROM cashgamecheckpoint " .
					"WHERE CheckpointID = {$id}";
			$rowCount = $this->db->execute($sql);
			return $rowCount > 0;
		}

		private function getGameSql(Homegame $homegame){
			return	"SELECT " .
					"g.GameID, g.Location, g.Status, g.Date, " .
					"cp.CheckpointID, cp.PlayerID, cp.Type, cp.Stack, cp.Amount, cp.Timestamp " .
					"FROM game g " .
					"LEFT JOIN cashgamecheckpoint cp ON g.GameID = cp.GameID " .
					"WHERE g.HomegameID = {$homegame->getId()} ";
		}

		public function getGame(Homegame $homegame, DateTime $date){
			$dateStr = Globalization::formatIsoDate($date);
			$sql =	$this->getGameSql($homegame) .
					"AND g.Date = '{$dateStr}' " .
					"ORDER BY cp.PlayerID, cp.Timestamp";
			$res = $this->db->query($sql);
			$cashgames = $this->getGamesFromDbResult($homegame, $res);
			if(count($cashgames) == 0){
				return null;
			}
			return $cashgames[0];
		}

		public function getGames(Homegame $homegame, $status = null, $year = null){
			$sql = $this->getGameSql($homegame);
			if($status != null){
				$sql .= "AND g.Status = {$status} ";
			}
			if($year != null){
				$sql .= "AND YEAR(g.Date) = {$year} ";
			}
			$sql .= "ORDER BY g.GameID, cp.PlayerID, cp.Timestamp";
			$res = $this->db->query($sql);
			return $this->getGamesFromDbResult($homegame, $res);
		}

		private function getGamesFromDbResult(Homegame $homegame, $res){
			$cashgames = array();
			$currentGame = null;
			$currentGameId = -1;
			$currentResult = null;
			$currentPlayerId = -1;
			foreach($res->fetchAll() as $row){
				if($row["GameID"] != $currentGameId){
					$currentGame = $this->rawCashgameFromDbRow($row);
					$currentGameId = $currentGame->getId();
					$cashgames[] = $currentGame;
					$currentResult = null;
					$currentPlayerId = -1;
				}
				if($row["PlayerID"] != $currentPlayerId){
					if($row["PlayerID"] != null){
						$currentResult = $this->rawCashgameResultFromDbRow($row);
						$currentPlayerId = $currentResult->getPlayerId();
						$currentGame->addResult($currentResult);
					}
				}
				if($row["CheckpointID"] != null){
					$checkpoint = $this->checkpointFromDbRow($row, $homegame->getTimezone());
					$currentResult->addCheckpoint($checkpoint);
				}
			}
			return $cashgames;
		}

		public function getYears(Homegame $homegame){
			$sql =	"SELECT DISTINCT YEAR(ccp.Timestamp) as 'Year' " .
					"FROM cashgamecheckpoint ccp " .
					"LEFT JOIN game g ON ccp.GameID = g.GameID " .
					"LEFT JOIN homegame h ON g.HomegameID = h.HomegameID " .
					"WHERE h.Name = '{$homegame->getSlug()}' " .
					"ORDER BY 'Year' DESC";
			$res = $this->db->query($sql);
			$years = array();
			foreach($res->fetchAll() as $row){
				$years[] = $row["Year"];
			}
			return $years;
		}

		public function updateGame(RawCashgame $cashgame){
			$location = $cashgame->getLocation();
			$date = $cashgame->getDate();
			$status = $cashgame->getStatus();
			$id = $cashgame->getId();
			return $this->db->executePrepared(PreparedStatement::UpdateCashgame, $location, $date, $status, $id);
		}

		public function hasPlayed(Player $player){
			$sql =	"SELECT DISTINCT PlayerID " .
					"FROM cashgamecheckpoint " .
					"WHERE PlayerId = {$player->getId()}";
			$res = $this->db->query($sql);
			foreach($res->fetchAll() as $row){
				return true;
			}
			return false;
		}

		public function getLocations(Homegame $homegame){
			$sql =	"SELECT DISTINCT g.Location " .
					"FROM game g " .
					"LEFT JOIN homegame h ON g.HomegameID = h.HomegameID " .
					"WHERE Name = '{$homegame->getSlug()}' " .
					"AND g.Location <> '' " .
					"ORDER BY g.Location";
			$res = $this->db->query($sql);
			$locations = array();
			foreach($res->fetchAll() as $row){
				$locations[] = $row["Location"];
			}
			return $locations;
		}

		private function rawCashgameFromDbRow($row){
			$id = $row["GameID"];
			$location = isset($row["Location"]) && $row["Location"] != "" ? $row["Location"] : null;
			$status = (int)$row["Status"];
			$date = $row["Date"];
			$cashgame = new RawCashgame($id, $location, $status, $date);
			return $cashgame;
		}

		private function rawCashgameResultFromDbRow($row){
			$playerId = $row["PlayerID"];
			$result = new RawCashgameResult($playerId);
			return $result;
		}

		private function checkpointFromDbRow($row, DateTimeZone $timezone){
			$id = (int)$row['CheckpointID'];
			$type = (int)$row['Type'];
			$amount = (int)$row['Amount'];
			$stack = (int)$row['Stack'];
			$timestamp = DateTimeFactory::create($row['Timestamp']);
			$timestamp->setTimezone($timezone);
			$checkpoint = $this->createCheckpoint($type, $timestamp, $stack, $amount);
			$checkpoint->setId($id);
			return $checkpoint;
		}

		private function createCheckpoint($type, $timestamp, $stack, $amount){
			if($type == 1){
				return new BuyinCheckpoint($timestamp, $stack, $amount);
			} else if ($type == 2){
				return new CashoutCheckpoint($timestamp, $stack);
			}
			return new ReportCheckpoint($timestamp, $stack);
		}

	}

}