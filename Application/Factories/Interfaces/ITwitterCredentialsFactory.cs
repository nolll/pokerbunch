using Core.Entities;

namespace Application.Factories
{
    public interface ITwitterCredentialsFactory
    {
        TwitterCredentials Create(string key, string secret, string twitterName);
    }
}