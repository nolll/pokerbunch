using Core.Classes;
using Core.Repositories;
using Infrastructure.Data.Storage.Interfaces;

namespace Infrastructure.Repositories
{
    public class TwitterRepository : ITwitterRepository
    {
        private readonly ITwitterStorage _twitterStorage;

        public TwitterRepository(ITwitterStorage twitterStorage)
        {
            _twitterStorage = twitterStorage;
        }

        public TwitterCredentials GetCredentials(User user)
        {
            return _twitterStorage.GetCredentials(user);
        }

        public int AddCredentials(User user, TwitterCredentials credentials)
        {
            return _twitterStorage.AddCredentials(user, credentials);
        }

    }
}