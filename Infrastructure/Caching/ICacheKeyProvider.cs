namespace Infrastructure.Caching
{
    public interface ICacheKeyProvider
    {
        string ConstructCacheKey(string typeName, params object[] procedureParameters);
        string UserKey(int id);
        string UserIdByEmailKey(string email);
        string UserIdsKey();
    }
}