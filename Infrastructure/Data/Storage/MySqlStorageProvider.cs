using System.Data;
using Infrastructure.Data.Storage.Interfaces;
using MySql.Data.MySqlClient;

namespace Infrastructure.Data.Storage {

	public class MySqlStorageProvider : IStorageProvider{

		private readonly string _connectionString;

		public MySqlStorageProvider()
		{
            _connectionString = "server=127.0.0.1;user=homegame;database=homegamemanager;port=3306;password=bobb12br;Convert Zero Datetime=True;";
		}

        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(_connectionString);
        }

		public StorageDataReader Query(string sql)
		{
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new MySqlCommand(sql, connection))
                {
                    var mySqlReader = command.ExecuteReader();
                    var dt = new DataTable();
                    dt.Load(mySqlReader);
                    return new StorageDataReader(dt.CreateDataReader());
                }
            }
		}

        public int Execute(string sql)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new MySqlCommand(sql, connection))
                {
                    return command.ExecuteNonQuery();
                }
            }
        }

        public int ExecuteInsert(string sql)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new MySqlCommand(sql, connection))
                {
                    command.ExecuteNonQuery();
                    return (int)command.LastInsertedId;
                }
            }
        }

        public int BoolToInt(bool boolean){
			return boolean ? 1 : 0;
		}
        
        /*
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

		public function __destruct(){
			conn = null;
		}
        */

	}

}