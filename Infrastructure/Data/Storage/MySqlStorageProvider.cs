/*
using MySql.Data.MySqlClient;

namespace Infrastructure.Data.Storage {

	public class MySqlStorageProvider : IStorageProvider{

		private readonly string _connectionString;

		public MySqlStorageProvider()
		{
		    _connectionString = "server=localhost;user=root;database=world;port=3306;password=******;";
		}

        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(_connectionString);
        }

		public MySqlDataReader Query(string sql)
		{
		    var connection = GetConnection();
		    var command = new MySqlCommand(sql, connection);
		    return command.ExecuteReader();
		}

		public int Execute(string sql){
			var connection = GetConnection();
		    var command = new MySqlCommand(sql, connection);
		    return command.ExecuteNonQuery();
		}

		public function executePrepared($preparedSql){
			$stmt = conn.prepare($preparedSql);
			$params = array();
			$numArgs = func_num_args();
			for($i = 1 ; $i < $numArgs; $i++) {
				$params[] = func_get_arg($i);
			}
			return $stmt.execute($params);
		}

		public int getLatestInsertId($success = true){
			if(!$success){
				return null;
			}
			return conn.lastInsertId();
		}

		public string quote($string){
			return conn.quote($string);
		}

		private bool hasCredentials($host, $database, $username, $password){
			return	$host != null &&
					$database != null &&
					$username != null &&
					$password != null;
		}

		public int boolToInt($bool){
			return $bool ? 1 : 0;
		}

		public function __destruct(){
			conn = null;
		}

	}

}
*/