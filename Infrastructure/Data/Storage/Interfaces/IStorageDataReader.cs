using System;

namespace Infrastructure.Data.Storage
{
    public interface IStorageDataReader
    {
        string GetString(string key);
        int GetInt(string key);
        bool GetBoolean(string key);
        DateTime GetDateTime(string key);
        bool Read();
    }
}