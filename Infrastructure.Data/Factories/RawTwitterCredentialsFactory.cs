using Core.Entities;
using Infrastructure.Data.Classes;
using Infrastructure.Data.Interfaces;

namespace Infrastructure.Data.Factories
{
    public class RawTwitterCredentialsFactory : IRawTwitterCredentialsFactory
    {
        public RawTwitterCredentials Create(IStorageDataReader reader)
        {
            return new RawTwitterCredentials
                {
                    TwitterName = reader.GetStringValue("TwitterName"),
                    Key = reader.GetStringValue("Key"),
                    Secret = reader.GetStringValue("Secret")
                };
        }

        public RawTwitterCredentials Create(TwitterCredentials credentials)
        {
            return new RawTwitterCredentials
                {
                    TwitterName = credentials.TwitterName,
                    Key = credentials.Key,
                    Secret = credentials.Secret
                };
        }
    }
}
