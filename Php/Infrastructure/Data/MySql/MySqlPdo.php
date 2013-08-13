<?php
namespace Infrastructure\Data\MySql {

	use config\Settings;
	use exceptions\DatabaseException;
	use Infrastructure\Data\MySql\StorageProvider;
	use Infrastructure\Logging\Logger;
	use PDO;
	use PDOStatement;

	class MySqlPdo implements StorageProvider{

		protected $conn = null;
		private $logger;

		public function __construct(Settings $settings, Logger $logger){
			$host = $settings->getDatabaseHost();
			$database = $settings->getDatabaseName();
			$username = $settings->getDatabaseUserName();
			$password = $settings->getDatabasePassword();
			$this->logger = $logger;
			if(!$this->hasCredentials($host, $database, $username, $password)){
				throw new DatabaseException("Application: Database variables not set");
			}
			$this->conn = new PDO(	"mysql:host={$host};dbname={$database}",
									$username,
									$password,
									array(\PDO::MYSQL_ATTR_INIT_COMMAND => "SET NAMES utf8"));
		}

		/**
		 * @param string $sql
		 * @return PDOStatement
		 */
		public function query($sql){
			$this->logger->log("sql query: " . $sql);
			return $this->conn->query($sql);
		}

		public function execute($sql){
			$this->logger->log("sql execute: " . $sql);
			return $this->conn->exec($sql);
		}

		public function executePrepared($preparedSql){
			$stmt = $this->conn->prepare($preparedSql);
			$params = array();
			$numArgs = func_num_args();
			for($i = 1 ; $i < $numArgs; $i++) {
				$params[] = func_get_arg($i);
			}
			return $stmt->execute($params);
		}

		/**
		 * @param bool $success
		 * @return int
		 */
		public function getLatestInsertId($success = true){
			if(!$success){
				return null;
			}
			return $this->conn->lastInsertId();
		}

		/**
		 * @param string $string
		 * @return string
		 */
		public function quote($string){
			return $this->conn->quote($string);
		}

		/**
		 * @param string $host
		 * @param string $database
		 * @param string $username
		 * @param string $password
		 * @return bool
		 */
		private function hasCredentials($host, $database, $username, $password){
			return	$host != null &&
					$database != null &&
					$username != null &&
					$password != null;
		}

		/**
		 * @param bool $bool
		 * @return int
		 */
		public function boolToInt($bool){
			return $bool ? 1 : 0;
		}

		public function __destruct(){
			$this->conn = null;
		}

	}

}