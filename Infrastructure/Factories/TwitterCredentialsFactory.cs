using Core.Classes;
using Infrastructure.Data.Classes;

namespace Infrastructure.Factories
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
