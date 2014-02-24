using Core.Classes;

namespace Application.Factories
{
    public interface ITwitterCredentialsFactory
    {
        TwitterCredentials Create(string key, string secret, string twitterName);
    }
}