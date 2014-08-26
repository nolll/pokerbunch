using Core.Entities;
using Infrastructure.Data.Classes;
using Infrastructure.Data.Interfaces;

namespace Infrastructure.Data.Factories
{
    public static class RawTwitterCredentialsFactory
    {
        public static RawTwitterCredentials Create(IStorageDataReader reader)
        {
            return new RawTwitterCredentials
                {
                    TwitterName = reader.GetStringValue("TwitterName"),
                    Key = reader.GetStringValue("Key"),
                    Secret = reader.GetStringValue("Secret")
                };
        }

        public static RawTwitterCredentials Create(TwitterCredentials credentials)
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
