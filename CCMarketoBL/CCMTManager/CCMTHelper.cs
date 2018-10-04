using CCMarketoBL;
using CCMarketoBL.Model;
using System;
using System.Collections.Generic;
using System.Configuration;

using System.Runtime.Caching;
namespace CCMarketoBL.CCMTManager
{
   public class CCMTHelper
    {
        public static string GetFullUrl(string resourceUrl)
        {
            String url = ConfigurationManager.AppSettings["MTBaseURL"] + resourceUrl;
            return url;
        }
        public static object GetCacheValue(string key)
        {
            MemoryCache memoryCache = MemoryCache.Default;
          
            return memoryCache.Get(key);
        }

        public static bool AddCache(string key, object value, DateTimeOffset absExpiration)
        {
            MemoryCache memoryCache = MemoryCache.Default;
            return memoryCache.Add(key, value, absExpiration);
        }

        public static void DeleteCache(string key)
        {
            MemoryCache memoryCache = MemoryCache.Default;
            if (memoryCache.Contains(key))
            {
                memoryCache.Remove(key);
            }
        }

        public static void logError(Exception ex)
        {
            throw new NotImplementedException();
        }
    }
}
