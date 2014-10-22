using System.Collections.Generic;
using Infrastructure.SqlServer.Interfaces;

namespace Infrastructure.SqlServer
{
	public interface IStorageProvider
    {
        IStorageDataReader Query(string sql, IList<SimpleSqlParameter> parameters = null);
        IStorageDataReader Query(string sql, ListSqlParameter parameter);
        int Execute(string sql, IList<SimpleSqlParameter> parameters = null);
        int ExecuteInsert(string sql, IList<SimpleSqlParameter> parameters = null);
    }
}