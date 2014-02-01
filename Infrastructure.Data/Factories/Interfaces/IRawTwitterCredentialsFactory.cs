using Core.Classes;
using Infrastructure.Data.Classes;
using Infrastructure.Data.Interfaces;

namespace Infrastructure.Data.Factories.Interfaces
{
    public interface IRawTwitterCredentialsFactory
    {
        RawTwitterCredentials Create(IStorageDataReader reader);
        RawTwitterCredentials Create(TwitterCredentials credentials);
    }
}