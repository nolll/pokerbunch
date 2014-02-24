using Core.Classes;

namespace Application.Factories
{
    public class TwitterCredentialsFactory : ITwitterCredentialsFactory
    {
        public TwitterCredentials Create(string key, string secret, string twitterName)
        {
            return new TwitterCredentials
                (
                    key,
                    secret,
                    twitterName
                );
        }
    }
}
