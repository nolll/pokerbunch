using System;
using System.Collections.Generic;
using System.Data;
using Infrastructure.Data.Interfaces;

namespace Infrastructure.Data
{
    public class StorageDataReader : IStorageDataReader
    {
        private readonly IDataReader _reader;

        public StorageDataReader(IDataReader reader)
        {
            _reader = reader;
        }

        public string GetStringValue(string key)
        {
            var ordinal = _reader.GetOrdinal(key);
            return _reader.IsDBNull(ordinal) ? default(string) : _reader.GetString(ordinal);
        }

        public IList<string> GetStringList(string key)
        {
            var list = new List<string>();
            while (Read())
            {
                list.Add(GetStringValue(key));
            }
            return list;
        }

        public int GetIntValue(string key)
        {
            var ordinal = _reader.GetOrdinal(key);
            return _reader.IsDBNull(ordinal) ? default(int) : _reader.GetInt32(ordinal);
        }

        public IList<int> GetIntList(string key)
        {
            var list = new List<int>();
            while (Read())
            {
                list.Add(GetIntValue(key));
            }
            return list;
        }

        public bool GetBooleanValue(string key)
        {
            var ordinal = _reader.GetOrdinal(key);
            return !_reader.IsDBNull(ordinal) && _reader.GetBoolean(ordinal);
        }

        public IList<bool> GetBooleanList(string key)
        {
            var list = new List<bool>();
            while (Read())
            {
                list.Add(GetBooleanValue(key));
            }
            return list;
        }

        public DateTime GetDateTimeValue(string key)
        {
            var ordinal = _reader.GetOrdinal(key);
            var dateTime = _reader.IsDBNull(ordinal) ? default(DateTime) : _reader.GetDateTime(ordinal);
            return DateTime.SpecifyKind(dateTime, DateTimeKind.Utc);
        }

        public IList<DateTime> GetDateTimeList(string key)
        {
            var list = new List<DateTime>();
            while (Read())
            {
                list.Add(GetDateTimeValue(key));
            }
            return list;
        }

        public bool Read()
        {
            return _reader.Read();
        }

        public IList<T> GetList<T>(Func<IStorageDataReader, T> func)
        {
            var list = new List<T>();
            while (Read())
            {
                list.Add(func(this));
            }
            return list;
        }

        public T GetOne<T>(Func<IStorageDataReader, T> func)
        {
            return Read() ? func(this) : default(T);
        }

        
    }
}
