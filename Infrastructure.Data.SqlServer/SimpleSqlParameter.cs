using System;
using System.Data.SqlClient;

namespace Infrastructure.Data.SqlServer
{
    public class SimpleSqlParameter : IEquatable<SimpleSqlParameter>
    {
        public string ParameterName { get; private set; }
        public object Value { get; private set; }

        public SimpleSqlParameter(string parameterName, object value)
        {
            ParameterName = parameterName;
            Value = value;
        }

        public SqlParameter SqlParameter
        {
            get
            {
                return new SqlParameter(ParameterName, Value);
            }
        }

        public bool Equals(SimpleSqlParameter other)
        {
            return ParameterName.Equals(other.ParameterName) && Value.Equals(other.Value);
        }
    }
}
