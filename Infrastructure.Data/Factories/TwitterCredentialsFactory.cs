using Core.Classes;
using Infrastructure.Data.Classes;
using Infrastructure.Data.Factories.Interfaces;

namespace Infrastructure.Data.Factories
{
    public class TwitterCredentialsFactory : ITwitterCredentialsFactory
    {
        public TwitterCredentials Create(RawTwitterCredentials rawCredentials)
        {
            return new TwitterCredentials
                (
                    rawCredentials.Key,
                    rawCredentials.Secret,
                    rawCredentials.TwitterName
                );
        }
    }
}
