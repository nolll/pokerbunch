using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Application.Services;
using Infrastructure.Data.Interfaces;

namespace Infrastructure.Data.SqlServer
{
	public class SqlServerStorageProvider : IStorageProvider
    {
	    private readonly ISettings _settings;

        public SqlServerStorageProvider(ISettings settings)
        {
            _settings = settings;
        }

	    private SqlConnection GetConnection()
        {
            return new SqlConnection(_settings.GetConnectionString());
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

	    public IStorageDataReader Query(string sql, IList<SqlParameter> parameters)
	    {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddRange(parameters.ToArray());
                    var mySqlReader = command.ExecuteReader();
                    var dt = new DataTable();
                    dt.Load(mySqlReader);
                    return new StorageDataReader(dt.CreateDataReader());
                }
            }
	    }

	    public IStorageDataReader Query(string sql, SqlListParameter parameter)
	    {
	        var sqlWithIdList = sql.Replace(parameter.ParameterName, parameter.ParameterNameList);
	        return Query(sqlWithIdList, parameter.ParameterList);
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

        public int Execute(string sql, IList<SqlParameter> parameters)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddRange(parameters.ToArray());
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

        public int ExecuteInsert(string sql, IList<SqlParameter> parameters)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddRange(parameters.ToArray());
                    return Convert.ToInt32(command.ExecuteScalar());
                }
            }
        }

        public int BoolToInt(bool boolean){
			return boolean ? 1 : 0;
		}
	}

}