using System.Text;
using Core.Entities;

namespace Infrastructure.Data.Cache
{
    public class CacheKeyProvider : ICacheKeyProvider
    {
        public string UserKey(int id)
        {
            return ConstructCacheKey("User", id);
        }

        public string UserIdByNameOrEmailKey(string nameOrEmail)
        {
            return ConstructCacheKey("UserId", "nameoremail", nameOrEmail);
        }

        public string UserIdsKey()
        {
            return ConstructCacheKey("UserIds", "all");
        }

        public string BunchKey(int id)
        {
            return ConstructCacheKey("Homegame", id);
        }

        public string BunchIdBySlugKey(string slug)
        {
            return ConstructCacheKey("HomegameId", "slug", slug);
        }

        public string BunchIdsKey()
        {
            return ConstructCacheKey("HomegameIds", "all");
        }

        public string PlayerKey(int id)
        {
            return ConstructCacheKey("Player", id);
        }

        public string PlayerIdsKey(int bunchId)
        {
            return ConstructCacheKey("PlayerIds", bunchId);
        }

        public string PlayerIdByNameKey(int bunchId, string name)
        {
            return ConstructCacheKey("PlayerId", "name", bunchId, name);
        }

        public string PlayerIdByUserNameKey(int bunchId, string userName)
        {
            return ConstructCacheKey("PlayerId", "user", bunchId, userName);
        }

        public string CashgameKey(int id)
        {
            return ConstructCacheKey("Cashgame", id);
        }

        public string CashgameIdByDateStringKey(int bunchId, string dateString)
        {
            return ConstructCacheKey("CashgameId", bunchId, dateString);
        }

        public string CashgameIdByRunningKey(int bunchId)
        {
            return ConstructCacheKey("CashgameId", bunchId, "running");
        }

        public string CashgameIdsKey(int bunchId, GameStatus? status = null, int? year = null)
        {
            return ConstructCacheKey("CashgameIds", bunchId, status, year);
        }

        public string CashgameYearsKey(int bunchId)
        {
            return ConstructCacheKey("CashgameYears", bunchId);
        }

        public string ConstructCacheKey(string typeName, params object[] procedureParameters)
        {
            // construct a cachekey in the format "typeName:parameter1value:parameter2value:"
            var stringBuilder = new StringBuilder();

            stringBuilder.Append(typeName);
            stringBuilder.Append(":");

            foreach (var parameter in procedureParameters)
            {
                stringBuilder.Append(parameter);
                stringBuilder.Append(":");
            }

            stringBuilder.Remove(stringBuilder.Length - 1, 1);
            return stringBuilder.ToString();
        }
    }
}