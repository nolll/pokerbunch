using Infrastructure.Data.Classes;
using Infrastructure.Data.Storage;

namespace Infrastructure.Data.Factories
{
    public interface IRawPlayerFactory
    {
        RawPlayer Create(IStorageDataReader reader);
    }
}