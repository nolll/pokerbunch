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

        public TwitterRepository(
            ITwitterStorage twitterStorage)
        {
            _twitterStorage = twitterStorage;
        }

        public TwitterCredentials GetCredentials(User user)
        {
            var rawCredentials = _twitterStorage.GetCredentials(user.Id);
            return TwitterCredentialsDataMapper.Map(rawCredentials);
        }

        public int AddCredentials(User user, TwitterCredentials credentials)
        {
            var rawTwitterCredentials = RawTwitterCredentialsFactory.Create(credentials);
            return _twitterStorage.AddCredentials(user.Id, rawTwitterCredentials);
        }
    }
}