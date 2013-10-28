using System.Data;
using System.Data.SqlClient;
using Core.Services;
using Infrastructure.Data.Storage.Interfaces;

namespace Infrastructure.Data.Storage {

	public class SqlServerStorageProvider : IStorageProvider{
	    private readonly ISettings _settings;

        public SqlServerStorageProvider(ISettings settings)
        {
            _settings = settings;
        }

	    private SqlConnection GetConnection()
        {
            return new SqlConnection(_settings.GetConnectionString());
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