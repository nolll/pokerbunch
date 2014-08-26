using Core.Entities;

namespace Application.Factories
{
    public static class TwitterCredentialsFactory
    {
        public static TwitterCredentials Create(string key, string secret, string twitterName)
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
