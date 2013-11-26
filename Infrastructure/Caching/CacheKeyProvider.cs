using System.Text;

namespace Infrastructure.Caching
{
    public class CacheKeyProvider : ICacheKeyProvider
    {
        public string UserKey(int id)
        {
            return ConstructCacheKey("User", id); ;
        }

        public string UserIdByEmailKey(string email)
        {
            return ConstructCacheKey("UserId", "email", email);
        }

        public string UserIdsKey()
        {
            return ConstructCacheKey("UserIds"); ;
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