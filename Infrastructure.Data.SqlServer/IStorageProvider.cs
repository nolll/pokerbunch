using System.Collections.Generic;
using System.Data.SqlClient;
using Infrastructure.Data.Interfaces;

namespace Infrastructure.Data.SqlServer
{
	public interface IStorageProvider
    {
        IStorageDataReader Query(string sql);
        IStorageDataReader Query(string statement, IList<SqlParameter> parameters);
        int Execute(string sql);
        int ExecuteInsert(string sql);
        int BoolToInt(bool boolean);
	}
}