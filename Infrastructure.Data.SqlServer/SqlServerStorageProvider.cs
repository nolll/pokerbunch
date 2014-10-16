using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Infrastructure.Data.SqlServer
{
	public class SqlServerStorageProvider : IStorageProvider
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
	}
}