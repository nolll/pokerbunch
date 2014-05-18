using Core.Entities;
using Infrastructure.Data.Classes;
using Infrastructure.Data.Interfaces;

namespace Infrastructure.Data.Factories
{
    public interface IRawTwitterCredentialsFactory
    {
        RawTwitterCredentials Create(IStorageDataReader reader);
        RawTwitterCredentials Create(TwitterCredentials credentials);
    }
}