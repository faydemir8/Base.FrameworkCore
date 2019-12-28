using System;
using Base.Framework.Core.CacheManager.Objects.Enums;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Base.Framework.Core.CacheManager.Redis
{
    public interface IRedisDataAgent
    {
        bool Get<T>(string key, out T value);
        T Get<T>(string key) where T : class;
        string Get(string key);
        void Set(string key, object value);
        void Set(string key, object value, int duration, CacheExpirationOption? expirationOption);
        void Remove(string key);
        void Refresh(string key);

    }
    public class RedisDataAgent : IRedisDataAgent
    {
        private readonly IDistributedCache _distributedCache;

        public RedisDataAgent(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        #region Get

        public bool Get<T>(string key, out T value)
        {
            var result = _distributedCache.GetString(key);
            var flag = string.IsNullOrEmpty(result);
            value = flag ? default(T) : JsonConvert.DeserializeObject<T>(result);
            return !flag;
        }

        public T Get<T>(string key) where T : class
        {
            var value = _distributedCache.GetString(key);
            return string.IsNullOrEmpty(value) ? null : JsonConvert.DeserializeObject<T>(value);
        }

        public string Get(string key) 
        {
            var value = _distributedCache.GetString(key);
            return value;
        }

        #endregion
        #region Set

        public void Set(string key, object value)
        {
            SetBase(key,value,-1,null);
        }

        public void Set(string key, object value, int duration, CacheExpirationOption? expirationOption)
        {
            SetBase(key, value, duration, expirationOption);
        }

        private void SetBase(string key, object value, int duration, CacheExpirationOption? expirationOption)
        {
            if (string.IsNullOrEmpty(key) || value == null) return;
            if (duration > -1)
            {
                DistributedCacheEntryOptions options = null;
                if (expirationOption == CacheExpirationOption.Sliding)
                    options = new DistributedCacheEntryOptions
                    {
                        SlidingExpiration = TimeSpan.FromMinutes(duration)
                    };
                else
                    options = new DistributedCacheEntryOptions
                    {
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(duration)
                    };

                _distributedCache.SetString(key, JsonConvert.SerializeObject(value), options);

            }
            else
            {
                _distributedCache.SetString(key, JsonConvert.SerializeObject(value));

            }


        }

        #endregion

        #region Remove
        public void Remove(string key)
        {
            _distributedCache.Remove(key);
        }
        #endregion

        #region Refresh
        public void Refresh(string key)
        {
            _distributedCache.Refresh(key);
        }
        #endregion


    }

}
