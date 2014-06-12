using System;
using System.Collections.Generic;
using Infrastructure.Data.Interfaces;

namespace Tests.Infrastructure.Data.SqlServer
{
    public class StorageDataReaderInTest : IStorageDataReader
    {
        public string GetStringValue(string key)
        {
            return null;
        }

        public int GetIntValue(string key)
        {
            return 0;
        }

        public string ReadString(string key)
        {
            return null;
        }

        public IList<string> ReadStringList(string key)
        {
            return new List<string>();
        }

        public bool HasRows()
        {
            return Read();
        }

        public int? ReadInt(string key)
        {
            return null;
        }

        public IList<int> ReadIntList(string key)
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

        public IList<T> ReadList<T>(Func<IStorageDataReader, T> func)
        {
            return new List<T>();
        }

        public T ReadOne<T>(Func<IStorageDataReader, T> func)
        {
            return default(T);
        }
    }
}