using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Infrastructure.Storage.Interfaces;

namespace Infrastructure.Storage
{
	public abstract class SqlServerStorageProvider
    {
	    private SqlConnection GetConnection()
        {
            var connectionString = ConnectionString;
            return new SqlConnection(connectionString);
        }

	    private static string ConnectionString
	    {
	        get { return ConfigurationManager.ConnectionStrings["pokerbunch"].ConnectionString; }
	    }

	    protected IStorageDataReader Query(string sql, IEnumerable<SimpleSqlParameter> parameters = null)
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

        protected IStorageDataReader Query(string sql, ListSqlParameter parameter)
	    {
	        var sqlWithIdList = sql.Replace(parameter.ParameterName, parameter.ParameterNameList);
	        return Query(sqlWithIdList, parameter.ParameterList);
	    }

        protected int Execute(string sql, IEnumerable<SimpleSqlParameter> parameters = null)
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

        protected int ExecuteInsert(string sql, IEnumerable<SimpleSqlParameter> parameters = null)
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
	}
}