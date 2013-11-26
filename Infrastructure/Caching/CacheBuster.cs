using Core.Classes;

namespace Infrastructure.Caching
{
    public class CacheBuster : ICacheBuster
    {
        private readonly ICacheContainer _cacheContainer;
        private readonly ICacheKeyProvider _cacheKeyProvider;

        public CacheBuster(ICacheContainer cacheContainer, ICacheKeyProvider cacheKeyProvider)
        {
            _cacheContainer = cacheContainer;
            _cacheKeyProvider = cacheKeyProvider;
        }

        public void UserAdded()
        {
            var key = _cacheKeyProvider.UserIdsKey();
            _cacheContainer.Remove(key);
        }

        public void UserUpdated(User user)
        {
            var singleUserKey = _cacheKeyProvider.UserKey(user.Id);
            _cacheContainer.Remove(singleUserKey);
            
            var tokenKey = _cacheKeyProvider.UserIdByTokenKey(user.Token);
            _cacheContainer.Remove(tokenKey);

            var nameKey = _cacheKeyProvider.UserIdByNameOrEmailKey(user.UserName);
            _cacheContainer.Remove(nameKey);

            var emailKey = _cacheKeyProvider.UserIdByNameOrEmailKey(user.Email);
            _cacheContainer.Remove(emailKey);
        }
    }
}