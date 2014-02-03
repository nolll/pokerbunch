using System.Collections.Generic;
using System.Data.SqlClient;
using Infrastructure.Data.Interfaces;

namespace Infrastructure.Data.SqlServer
{
	public interface IStorageProvider
    {
        IStorageDataReader Query(string sql);
        IStorageDataReader Query(string sql, IList<SqlParameter> parameters);
        IStorageDataReader Query(string sql, SqlListParameter parameter);
        int Execute(string sql);
        int Execute(string sql, IList<SqlParameter> parameters);
        int ExecuteInsert(string sql);
        int ExecuteInsert(string sql, IList<SqlParameter> parameters);
        int BoolToInt(bool boolean);
	}
}