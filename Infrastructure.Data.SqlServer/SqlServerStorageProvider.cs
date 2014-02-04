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

        public SqlServerStorageProvider(
            ISettings settings)
        {
            _settings = settings;
        }

	    private SqlConnection GetConnection()
        {
            return new SqlConnection(_settings.GetConnectionString());
        }

        public IStorageDataReader Query(string sql, IList<SimpleSqlParameter> parameters)
	    {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand(sql, connection))
                {
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(ToSqlCommands(parameters));
                    }
                    var mySqlReader = command.ExecuteReader();
                    var dt = new DataTable();
                    dt.Load(mySqlReader);
                    return new StorageDataReader(dt.CreateDataReader());
                }
            }
	    }

	    public IStorageDataReader Query(string sql, ListSqlParameter parameter)
	    {
	        var sqlWithIdList = sql.Replace(parameter.ParameterName, parameter.ParameterNameList);
	        return Query(sqlWithIdList, parameter.ParameterList);
	    }

        public int Execute(string sql, IList<SimpleSqlParameter> parameters)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand(sql, connection))
                {
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(ToSqlCommands(parameters));
                    }
                    return command.ExecuteNonQuery();
                }
            }
        }

        public int ExecuteInsert(string sql, IList<SimpleSqlParameter> parameters)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand(sql, connection))
                {
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(ToSqlCommands(parameters));
                    }
                    return Convert.ToInt32(command.ExecuteScalar());
                }
            }
        }

        private SqlParameter[] ToSqlCommands(IEnumerable<SimpleSqlParameter> parameters)
        {
            return parameters.Select(o => o.SqlParameter).ToArray();
        }

        public int? GetInt(string sql, string columnName, IList<SimpleSqlParameter> parameters)
        {
            var reader = Query(sql, parameters);
            while (reader.Read())
            {
                return reader.GetInt(columnName);
            }
            return null;
        }

        public IList<int> GetIntList(string sql, string columnName, IList<SimpleSqlParameter> parameters)
        {
            var reader = Query(sql, parameters);
            var ids = new List<int>();
            while (reader.Read())
            {
                ids.Add(reader.GetInt(columnName));
            }
            return ids;
        }
	}
}