using Core.Entities;
using Core.Repositories;
using Infrastructure.Data.Factories;
using Infrastructure.Data.Interfaces;
using Infrastructure.Data.Mappers;

namespace Infrastructure.Data.Repositories
{
    public class TwitterRepository : ITwitterRepository
    {
        private readonly ITwitterStorage _twitterStorage;
        private readonly IRawTwitterCredentialsFactory _rawTwitterCredentialsFactory;
        private readonly ITwitterCredentialsDataMapper _twitterCredentialsDataMapper;

        public TwitterRepository(
            ITwitterStorage twitterStorage,
            IRawTwitterCredentialsFactory rawTwitterCredentialsFactory,
            ITwitterCredentialsDataMapper twitterCredentialsDataMapper)
        {
            _twitterStorage = twitterStorage;
            _rawTwitterCredentialsFactory = rawTwitterCredentialsFactory;
            _twitterCredentialsDataMapper = twitterCredentialsDataMapper;
        }

        public TwitterCredentials GetCredentials(User user)
        {
            var rawCredentials = _twitterStorage.GetCredentials(user.Id);
            return _twitterCredentialsDataMapper.Map(rawCredentials);
        }

        public int AddCredentials(User user, TwitterCredentials credentials)
        {
            var rawTwitterCredentials = _rawTwitterCredentialsFactory.Create(credentials);
            return _twitterStorage.AddCredentials(user.Id, rawTwitterCredentials);
        }
    }
}