using System;
using System.Web;
using System.Runtime.Caching;
using System.Collections.Generic;
using System.Linq;

namespace PBASE.Service
{
    public class CacheService : ICacheService
    {
        static readonly ObjectCache Cache = MemoryCache.Default;
        public T Get<T>(string cacheID, Func<T> getItemCallback) where T : class
        {
            T item = HttpRuntime.Cache.Get(cacheID) as T;
            if (item == null)
            {
                item = getItemCallback();
                HttpContext.Current.Cache.Insert(cacheID, item);
            }
            return item;
        }
        public static T Get<T>(string key) where T : class
        {
            try
            {
                return (T)Cache[key];
            }
            catch
            {
                return null;
            }
        }

        public static void Add<T>(string key, T objectToCache) where T : class
        {
            Cache.Add(key, objectToCache, DateTime.Now.AddHours(4));
        }

        public static void Clear(string key)
        {
            Cache.Remove(key);
        }
        public static void ClearAll()
        {
            List<string> cacheKeys = Cache.Select(kvp => kvp.Key).ToList();
            foreach (string cacheKey in cacheKeys)
            {
                Cache.Remove(cacheKey);
            }
        }
    }

    public interface ICacheService
    {
        T Get<T>(string cacheID, Func<T> getItemCallback) where T : class;
    }
}
