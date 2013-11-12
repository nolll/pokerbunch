using System;
using System.Collections.Concurrent;
using System.Text;

namespace Infrastructure.Caching
{
    public class CacheContainer : ICacheContainer
    {
        private static readonly ConcurrentDictionary<string, object> CurrentRequests = new ConcurrentDictionary<string, object>();
        private readonly ICacheProvider _cacheProvider;

        private readonly CacheableNullValue _nullValue = new CacheableNullValue();

        public CacheContainer(ICacheProvider cacheProvider)
        {
            _cacheProvider = cacheProvider;
        }

        public bool TryGet<T>(string key, out T value) where T : class
        {
            var o = _cacheProvider.Get(key);

            if (o is CacheableNullValue)
            {
                // A fake null value was found in the cache
                value = default(T);
                return true;
            }

            if (o == null)
            {
                // A real null was found, this means that nothing is cached for this key
                value = default(T);
                return false;
            }

            value = (T)o;
            return true;
        }

        public T Get<T>(string key) where T : class
        {
            return null;
            var o = _cacheProvider.Get(key);

            if (o is CacheableNullValue)
            {
                // A fake null value was found in the cache
                return default(T);
            }

            if (o == null)
            {
                // A real null was found, this means that nothing is cached for this key
                return default(T);
            }

            return (T)o;
        }

        public void Insert(string cacheKey, object objectToBeCached, TimeSpan cacheTime)
        {
            _cacheProvider.Put(cacheKey, objectToBeCached ?? _nullValue, cacheTime);
        }

        public void Remove(string cacheKey)
        {
            _cacheProvider.Remove(cacheKey);
        }

        public T GetCachedIfAvailable<T>(Func<T> fetchFromSourceExpression, TimeSpan cacheTime, string cacheKeyName, params object[] cacheKeyParams) where T : class
        {
            T cachedObject;
            var cacheKey = ConstructCacheKey(cacheKeyName, cacheKeyParams);
            var foundInCache = TryGet(cacheKey, out cachedObject);

            if (!foundInCache)
            {
                cachedObject = fetchFromSourceExpression();

                Insert(cacheKey, cachedObject, cacheTime);
            }

            return cachedObject;
        }

        /*
        public T GetObjectStoreInCache<T>(Func<T> fetchFromSourceExpression, TimeSpan cacheTime, string cacheKeyName, params object[] cacheKeyParams) where T : class
        {
            var cacheKey = ConstructCacheKey(cacheKeyName, cacheKeyParams);
            var obj = fetchFromSourceExpression();
            Insert(cacheKey, obj, cacheTime);

            return obj;
        }
        */

        /*
        public T GetCachedOrBackupedIfAvailable<T>(Func<T> fetchFromSourceExpression, TimeSpan cacheTime, string cacheKeyName, params object[] cacheKeyParams) where T : class
        {
            var cacheKey = ConstructCacheKey(cacheKeyName, cacheKeyParams);
            var cacheBackupKey = string.Format("{0}:{1}", cacheKey, "Backup");

            T cacheItem;
            if (TryGet(cacheKey, out cacheItem))
            {
                return cacheItem;   // Vi har nåt i cachen, allt är gott och väl
            }


            // Skaffa ett låsobjekt som är gemensamt för alla anrop med samma cachenyckel
            var _lock = new object();
            if (!CurrentRequests.TryAdd(cacheKey, _lock) && !CurrentRequests.TryGetValue(cacheKey, out _lock))
            {
                //_errorLog.ReportError(string.Format("Failed to aquire _lock object or insert a new (key: {0}). Will use old cache instead. This should NOT occur.", cacheKey), null, false, "srweb");
                return GetFromBackup<T>(cacheBackupKey);
            }

            // Första tråden kommer igenom här, alla andra kommer vänta tills första är klar
            lock (_lock)
            {
                try
                {
                    // Om vi kommer in i låset så är vi antingen (1) först in och
                    // cachen är tom, eller så har vi (2) väntat på att den andra tråden
                    // kört klart och fyllt i cachen. Så att vi frågar igen här
                    // är för att fånga fall (2)

                    if (TryGet(cacheKey, out cacheItem))
                    {
                        return cacheItem; // Vi har väntat vid låset och under tiden har cachen fyllts på, yay!
                    }

                    var backupFromCache = GetFromBackup<T>(cacheBackupKey);
                    if (backupFromCache != null)
                    {
                        // Det finns nåt i backupcachen men inte i cachen, kopiera så att
                        // nästa tråd kan returnera direkt där uppe, redan innan låset
                        Insert(cacheKey, backupFromCache, cacheTime);
                    }

                    // gå ut och gör det tunga jobbet för att hitta det nya värdet
                    cacheItem = fetchFromSourceExpression();


                    // stoppa det i cachen och i backupcachen
                    Insert(cacheKey, cacheItem, cacheTime);
                    Insert(cacheBackupKey, cacheItem, TimeSpan.FromDays(1));

                    return cacheItem; // och returnera
                }
                catch (Exception e)
                {
                    // något gick allvarligt fel på vägen, försök chansa på att det finns nåt i backupcachen, och logga

                    // Avoid hammering an unresponsive resource. Restore from whaterver is in the backup cache (even if its null).
                    var backup = GetFromBackup<T>(cacheBackupKey);
                    Insert(cacheKey, backup, TimeSpan.FromMinutes(1));
                    //_errorLog.ReportError(string.Format("Failed fetching data for cacheKey: {0}. Will use old cache instead.", cacheKey), e, false, "srweb");
                }
            }
            return null; // Här hamnar vi enbart om en exception inträffade.
        }
        */

        /*
        private T GetFromBackup<T>(string cacheKey) where T : class
        {
            T cacheItem;
            TryGet(cacheKey, out cacheItem);
            return cacheItem;
        }
        */

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

        private class CacheableNullValue
        {
        }
    }
}