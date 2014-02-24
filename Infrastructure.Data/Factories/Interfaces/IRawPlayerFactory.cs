using Infrastructure.Data.Classes;
using Infrastructure.Data.Interfaces;

namespace Infrastructure.Data.Factories
{
    public interface IRawPlayerFactory
    {
        RawPlayer Create(IStorageDataReader reader);
    }
}