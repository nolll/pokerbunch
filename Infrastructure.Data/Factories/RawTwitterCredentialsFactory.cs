using Core.Classes;
using Infrastructure.Data.Classes;
using Infrastructure.Data.Factories.Interfaces;
using Infrastructure.Data.Interfaces;

namespace Infrastructure.Data.Factories
{
    public class RawTwitterCredentialsFactory : IRawTwitterCredentialsFactory
    {
        public RawTwitterCredentials Create(IStorageDataReader reader)
        {
            return new RawTwitterCredentials
                {
                    TwitterName = reader.GetString("TwitterName"),
                    Key = reader.GetString("Key"),
                    Secret = reader.GetString("Secret")
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
