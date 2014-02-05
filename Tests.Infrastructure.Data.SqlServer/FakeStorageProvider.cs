using System.Collections.Generic;
using System.Globalization;
using Infrastructure.Data.Interfaces;
using Infrastructure.Data.SqlServer;

namespace Tests.Infrastructure.Data.SqlServer
{
    public class FakeStorageProvider : IStorageProvider
    {
        public string Sql { get; private set; }

        public IStorageDataReader Query(string sql, IList<SimpleSqlParameter> parameters = null)
        {
            Sql = GetFakeSql(sql, parameters);
            return new FakeStorageDataReader();
        }

        public IStorageDataReader Query(string sql, ListSqlParameter parameter)
        {
            Sql = GetFakeSql(sql, parameter);
            return new FakeStorageDataReader();
        }

        public int Execute(string sql, IList<SimpleSqlParameter> parameters = null)
        {
            Sql = GetFakeSql(sql, parameters);
            return 0;
        }

        public int ExecuteInsert(string sql, IList<SimpleSqlParameter> parameters = null)
        {
            Sql = GetFakeSql(sql, parameters);
            return 0;
        }

        public int? GetInt(string sql, string columnName, IList<SimpleSqlParameter> parameters = null)
        {
            Sql = GetFakeSql(sql, parameters);
            return null;
        }

        public IList<int> GetIntList(string sql, string columnName, IList<SimpleSqlParameter> parameters = null)
        {
            Sql = GetFakeSql(sql, parameters);
            return null;
        }

        private string GetFakeSql(string sql, ListSqlParameter parameter)
        {
            var result = sql.Replace(parameter.ParameterName, parameter.ParameterNameList);
            return GetFakeSql(result, parameter.ParameterList);
        }

        private string GetFakeSql(string sql, IList<SimpleSqlParameter> parameters)
        {
            if (parameters == null)
            {
                return sql;
            }
            var result = sql;
            foreach (var parameter in parameters)
            {
                result = result.Replace(parameter.ParameterName, FormatValue(parameter.Value));
            }
            return result;
        }

        private string FormatValue(object value)
        {
            if (value as int? != null)
            {
                return value.ToString();
            }
            return string.Format("'{0}'", value);
        }
    }
}