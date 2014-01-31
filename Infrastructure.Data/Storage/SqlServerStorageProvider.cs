using System;
using System.Data;
using System.Data.SqlClient;
using Application.Services;

namespace Infrastructure.Data.Storage {

	public class SqlServerStorageProvider : IStorageProvider{
	    private readonly ISettings _settings;
	    private readonly string _connectionString;

        public SqlServerStorageProvider(ISettings settings)
        {
            _settings = settings;
        }

        public SqlServerStorageProvider(string connectionString)
        {
            _connectionString = connectionString;
        }

	    private SqlConnection GetConnection()
        {
            return new SqlConnection(ConnectionString);
        }

	    private string ConnectionString
	    {
	        get
	        {
	            return _settings != null ? _settings.GetConnectionString() : _connectionString;
	        }
	    }

		public IStorageDataReader Query(string sql)
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
                    return Convert.ToInt32(command.ExecuteScalar());
                }
            }
        }

        public int BoolToInt(bool boolean){
			return boolean ? 1 : 0;
		}
	}

}