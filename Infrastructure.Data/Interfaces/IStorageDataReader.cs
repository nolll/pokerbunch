using System;
using System.Collections.Generic;

namespace Infrastructure.Data.Interfaces
{
    public interface IStorageDataReader
    {
        string GetStringValue(string key);
        int GetIntValue(string key);
        bool GetBooleanValue(string key);
        DateTime GetDateTimeValue(string key);
        bool Read();
        IList<T> GetList<T>(Func<IStorageDataReader, T> func);
        T GetOne<T>(Func<IStorageDataReader, T> func);
        IList<int> GetIntList(string key);
        IList<string> GetStringList(string key);
        IList<bool> GetBooleanList(string key);
        IList<DateTime> GetDateTimeList(string key);
    }
}