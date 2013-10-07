using Core.Classes;
using Infrastructure.Data.Classes;
using Infrastructure.Data.Storage;

namespace Infrastructure.Data.Factories
{
    internal interface IRawTwitterCredentialsFactory
    {
        RawTwitterCredentials Create(StorageDataReader reader);
        RawTwitterCredentials Create(TwitterCredentials credentials);
    }
}