using Infrastructure.Data.Classes;
using Infrastructure.Data.Interfaces;

namespace Infrastructure.Data.Factories.Interfaces
{
    public interface IRawPlayerFactory
    {
        RawPlayer Create(IStorageDataReader reader);
    }
}