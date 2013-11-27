namespace Infrastructure.Caching
{
    public interface ICacheKeyProvider
    {
        string ConstructCacheKey(string typeName, params object[] procedureParameters);
        string UserKey(int id);
        string UserIdByTokenKey(string token);
        string UserIdByNameOrEmailKey(string nameOrEmail);
        string UserIdsKey();
        string HomegameKey(int id);
        string HomegameIdsKey();
        string HomegameIdBySlugKey(string slug);
    }
}