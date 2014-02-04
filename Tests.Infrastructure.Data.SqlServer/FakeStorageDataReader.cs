using System;
using Infrastructure.Data.Interfaces;

namespace Tests.Infrastructure.Data.SqlServer
{
    public class FakeStorageDataReader : IStorageDataReader
    {
        public string GetString(string key)
        {
            return null;
        }

        public int GetInt(string key)
        {
            return 0;
        }

        public bool GetBoolean(string key)
        {
            return false;
        }

        public DateTime GetDateTime(string key)
        {
            return DateTime.MinValue;
        }

        public bool Read()
        {
            return false;
        }
    }
}