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

        public void UserUpdated(int userId)
        {
            var key = _cacheKeyProvider.SingleUserKey(userId);
            _cacheContainer.Remove(key);
        }
    }
}