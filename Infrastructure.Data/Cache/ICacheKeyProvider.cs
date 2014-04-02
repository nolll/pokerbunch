using Core.Classes;

namespace Infrastructure.Data.Cache
{
    public interface ICacheKeyProvider
    {
        string ConstructCacheKey(string typeName, params object[] procedureParameters);
        string UserKey(int id);
        string UserIdByNameOrEmailKey(string nameOrEmail);
        string UserIdsKey();
        string HomegameKey(int id);
        string HomegameIdsKey();
        string HomegameIdBySlugKey(string slug);
        string PlayerKey(int id);
        string PlayerIdsKey(int homegameId);
        string PlayerIdByNameKey(int homegameId, string name);
        string PlayerIdByUserNameKey(int homegameId, string userName);
        string CashgameKey(int id);
        string CashgameIdByDateStringKey(int homegameId, string dateString);
        string CashgameIdByRunningKey(int homegameId);
        string CashgameIdsKey(int homegameId, GameStatus? status = null, int? year = null);
        string CashgameYearsKey(int homegameId);
    }
}