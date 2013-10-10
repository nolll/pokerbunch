using System.Data;
using System.Data.SqlClient;
using Infrastructure.Data.Storage.Interfaces;

namespace Infrastructure.Data.Storage {

	public class SqlServerStorageProvider : IStorageProvider{

		private readonly string _connectionString;

        public SqlServerStorageProvider()
		{
            _connectionString = "Server=tcp:o5ctpblaqd.database.windows.net,1433;Database=pokerbunch;User ID=pokerbunch@o5ctpblaqd;Password=3Sugfisk;Trusted_Connection=False;Encrypt=True;Connection Timeout=30;";
		}

        private SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }

		public StorageDataReader Query(string sql)
		{
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand(sql, connection))
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
                using (var command = new SqlCommand(sql, connection))
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
                using (var command = new SqlCommand(sql, connection))
                {
                    return (int)command.ExecuteScalar();
                }
            }
        }

        public int BoolToInt(bool boolean){
			return boolean ? 1 : 0;
		}
	}

}