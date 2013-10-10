using System;
using System.Data;

namespace Infrastructure.Data.Storage
{
    public class StorageDataReader
    {
        private readonly IDataReader _reader;

        public StorageDataReader(IDataReader reader)
        {
            _reader = reader;
        }

        public string GetString(string key)
        {
            var ordinal = _reader.GetOrdinal(key);
            return _reader.IsDBNull(ordinal) ? null : _reader.GetString(ordinal);
        }

        public int GetInt(string key)
        {
            var ordinal = _reader.GetOrdinal(key);
            return _reader.IsDBNull(ordinal) ? 0 : _reader.GetInt32(ordinal);
        }

        public bool GetBoolean(string key)
        {
            var ordinal = _reader.GetOrdinal(key);
            return !_reader.IsDBNull(ordinal) && _reader.GetBoolean(ordinal);
        }

        public DateTime GetDateTime(string key)
        {
            var ordinal = _reader.GetOrdinal(key);
            var dateTime = _reader.IsDBNull(ordinal) ? DateTime.MinValue : _reader.GetDateTime(ordinal);
            return DateTime.SpecifyKind(dateTime, DateTimeKind.Utc);
        }

        public bool Read()
        {
            return _reader.Read();
        }
    }
}
