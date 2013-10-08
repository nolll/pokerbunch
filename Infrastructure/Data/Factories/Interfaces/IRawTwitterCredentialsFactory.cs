using Core.Classes;
using Infrastructure.Data.Classes;
using Infrastructure.Data.Storage;

namespace Infrastructure.Data.Factories
{
    public interface IRawTwitterCredentialsFactory
    {
        RawTwitterCredentials Create(StorageDataReader reader);
        RawTwitterCredentials Create(TwitterCredentials credentials);
    }
}