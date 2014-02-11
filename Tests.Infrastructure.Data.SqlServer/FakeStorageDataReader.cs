using System;
using System.Collections.Generic;
using Infrastructure.Data.Interfaces;

namespace Tests.Infrastructure.Data.SqlServer
{
    public class FakeStorageDataReader : IStorageDataReader
    {
        public string GetStringValue(string key)
        {
            return null;
        }

        public IList<string> GetStringList(string key)
        {
            return new List<string>();
        }

        public int GetIntValue(string key)
        {
            return 0;
        }

        public IList<int> GetIntList(string key)
        {
            return new List<int>();
        }

        public bool GetBooleanValue(string key)
        {
            return false;
        }

        public IList<bool> GetBooleanList(string key)
        {
            return new List<bool>();
        }

        public DateTime GetDateTimeValue(string key)
        {
            return DateTime.MinValue;
        }

        public IList<DateTime> GetDateTimeList(string key)
        {
            return new List<DateTime>();
        }

        public bool Read()
        {
            return false;
        }

        public IList<T> GetList<T>(Func<IStorageDataReader, T> func)
        {
            return new List<T>();
        }

        public T GetOne<T>(Func<IStorageDataReader, T> func)
        {
            return default(T);
        }
    }
}