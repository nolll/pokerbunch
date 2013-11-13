namespace Infrastructure.Caching
{
    public interface ICacheKeyProvider
    {
        string SingleUserKey(int id);
        string ConstructCacheKey(string typeName, params object[] procedureParameters);
        string UserIdsKey();
    }
}