using Core.Entities;

namespace Infrastructure.Data.Cache
{
    public interface ICacheKeyProvider
    {
        string ConstructCacheKey(string typeName, params object[] procedureParameters);
        string UserKey(int id);
        string UserIdByNameOrEmailKey(string nameOrEmail);
        string UserIdsKey();
        string BunchKey(int id);
        string BunchIdsKey();
        string BunchIdBySlugKey(string slug);
        string PlayerKey(int id);
        string PlayerIdsKey(int bunchId);
        string PlayerIdByNameKey(int bunchId, string name);
        string PlayerIdByUserNameKey(int bunchId, string userName);
        string CashgameKey(int id);
        string CashgameIdByDateStringKey(int bunchId, string dateString);
        string CashgameIdByRunningKey(int bunchId);
        string CashgameIdsKey(int bunchId, GameStatus? status = null, int? year = null);
        string CashgameYearsKey(int bunchId);
    }
}