﻿using System;
using System.Data.SqlClient;

namespace Infrastructure.Storage
{
    public class SimpleSqlParameter : IEquatable<SimpleSqlParameter>
    {
        private string ParameterName { get; set; }
        private object Value { get; set; }

        public SimpleSqlParameter(string parameterName, object value)
        {
            ParameterName = parameterName;
            Value = value;
        }

        public SqlParameter SqlParameter
        {
            get
            {
                var value = Value ?? DBNull.Value;
                return new SqlParameter(ParameterName, value);
            }
        }

        public bool Equals(SimpleSqlParameter other)
        {
            return ParameterName.Equals(other.ParameterName) && Value.Equals(other.Value);
        }
    }
}