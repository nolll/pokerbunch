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
        string PlayerKey(int id);
        string PlayerIdsKey(int homegameId);
        string PlayerIdByNameKey(int homegameId, string name);
        string PlayerIdByUserNameKey(int homegameId, string userName);
    }
}