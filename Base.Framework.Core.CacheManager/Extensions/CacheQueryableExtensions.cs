using System;
using System.Collections.Generic;
using System.Linq;
using Base.Framework.Core.CacheManager.Objects.Classes;
using Base.Framework.Core.CacheManager.Objects.Enums;
using Base.Framework.Core.CacheManager.Redis;

namespace Base.Framework.Core.CacheManager.Extensions
{
    public static class CacheQueryableExtensions
    {
        private static IRedisDataAgent CacheProvider => RedisConfigurationManager.RedisDataAgent;
        #region GetExtensions

        #region ToCachedList

        public static List<T> ToCachedList<T>(this ICacheQueryable<T> value, int duration, CacheExpirationOption? expirationOption=null)
        {
            var result = CacheProvider.Get<List<T>>(value.CacheKey);
            if (result != null)
                return result;
            lock (AsyncLock.LockOnValue(value.CacheKey))
            {
                //double check
                if (CacheProvider.Get(value.CacheKey, out result))
                {
                    return result;
                }
                result = value.ToList();
                CacheProvider.Set(value.CacheKey, result, duration, expirationOption);
            }

            return result;
        }

        #endregion

        #region ToCachedFirst

        public static T ToCachedFirst<T>(this ICacheQueryable<T> value, int duration, CacheExpirationOption? expirationOption = null) where T : class
        {
            var result = CacheProvider.Get<T>(value.CacheKey);
            if (result != null)
                return result;
            lock (AsyncLock.LockOnValue(value.CacheKey))
            {
                //double check
                if (CacheProvider.Get(value.CacheKey, out result))
                {
                    return result;
                }
                result = value.First();
                CacheProvider.Set(value.CacheKey, result, duration, expirationOption);
            }

            return result;
        }

        #endregion

        #region ToCachedFirstOrDefault
        public static T ToCachedFirstOrDefault<T>(this ICacheQueryable<T> value, int duration, CacheExpirationOption? expirationOption=null) where T : class
        {
            var result = CacheProvider.Get<T>(value.CacheKey);
            if (result != null)
                return result;
            lock (AsyncLock.LockOnValue(value.CacheKey))
            {
                //double check
                if (CacheProvider.Get(value.CacheKey, out result))
                {
                    return result;
                }
                result = value.FirstOrDefault();
                CacheProvider.Set(value.CacheKey, result, duration, expirationOption);
            }

            return result;

        }

        #endregion

        #region ToCachedCount
        public static int ToCachedCount<T>(this ICacheQueryable<T> value, int duration, CacheExpirationOption? expirationOption=null)
        {
            var cachedResult = CacheProvider.Get(value.CacheKey);
            if (cachedResult != null && int.TryParse(cachedResult,out var result))
                return result;
            lock (AsyncLock.LockOnValue(value.CacheKey))
            {
                //double check
                if (CacheProvider.Get(value.CacheKey, out result))
                {
                    return result;
                }
                result = value.Count();
                CacheProvider.Set(value.CacheKey, result, duration, expirationOption);
            }

            return result;
        }

        #endregion
        #endregion

        #region AsCacheQueryable
        public static ICacheQueryable<T> AsCacheQueryable<T>(this IQueryable<T> source, string cacheKey)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            var queryable = source as ICacheQueryable<T>;
            queryable = queryable ?? new CacheQueryable<T>(source);
            queryable.CacheKey = cacheKey;
            return queryable;
        }
        #endregion

    }
}
