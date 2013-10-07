using Core.Classes;
using Core.Repositories;
using Infrastructure.Data.Factories;
using Infrastructure.Data.Storage.Interfaces;
using Infrastructure.Factories;

namespace Infrastructure.Repositories
{
    internal class TwitterRepository : ITwitterRepository
    {
        private readonly ITwitterStorage _twitterStorage;
        private readonly IRawTwitterCredentialsFactory _rawTwitterCredentialsFactory;
        private readonly ITwitterCredentialsFactory _twitterCredentialsFactory;

        public TwitterRepository(
            ITwitterStorage twitterStorage,
            IRawTwitterCredentialsFactory rawTwitterCredentialsFactory,
            ITwitterCredentialsFactory twitterCredentialsFactory)
        {
            _twitterStorage = twitterStorage;
            _rawTwitterCredentialsFactory = rawTwitterCredentialsFactory;
            _twitterCredentialsFactory = twitterCredentialsFactory;
        }

        public TwitterCredentials GetCredentials(User user)
        {
            var rawCredentials = _twitterStorage.GetCredentials(user.Id);
            return _twitterCredentialsFactory.Create(rawCredentials);
        }

        public int AddCredentials(User user, TwitterCredentials credentials)
        {
            var rawTwitterCredentials = _rawTwitterCredentialsFactory.Create(credentials);
            return _twitterStorage.AddCredentials(user.Id, rawTwitterCredentials);
        }

    }
}