using Core.Classes;
using Infrastructure.Data.Classes;
using Infrastructure.Data.Interfaces;

namespace Infrastructure.Data.Factories
{
    public interface IRawUserFactory
    {
        RawUser Create(IStorageDataReader reader);
        RawUser Create(User user);
    }
}