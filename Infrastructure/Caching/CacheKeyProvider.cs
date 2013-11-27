using System.Text;

namespace Infrastructure.Caching
{
    public class CacheKeyProvider : ICacheKeyProvider
    {
        public string UserKey(int id)
        {
            return ConstructCacheKey("User", id); ;
        }

        public string UserIdByTokenKey(string token)
        {
            return ConstructCacheKey("UserId", "token", token);
        }

        public string UserIdByNameOrEmailKey(string nameOrEmail)
        {
            return ConstructCacheKey("UserId", "nameoremail", nameOrEmail);
        }

        public string UserIdsKey()
        {
            return ConstructCacheKey("UserIds", "all");
        }

        public string HomegameKey(int id)
        {
            return ConstructCacheKey("Homegame", id); ;
        }

        public string HomegameIdBySlugKey(string slug)
        {
            return ConstructCacheKey("HomegameId", "slug", slug);
        }

        public string HomegameIdsKey()
        {
            return ConstructCacheKey("HomegameIds", "all");
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