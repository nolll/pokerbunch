namespace Infrastructure.Caching
{
    public interface ICacheKeyProvider
    {
        string ConstructCacheKey(string typeName, params object[] procedureParameters);
        string UserKey(int id);
        string UserIdByEmailKey(string email);
        string UserIdByTokenKey(string token);
        string UserIdByNameOrEmailKey(string nameOrEmail);
        string UserIdsKey();
    }
}