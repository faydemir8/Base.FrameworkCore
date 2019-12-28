using System;
using System.Collections.Generic;

namespace Base.Framework.Core.CacheManager.Extensions
{
    public sealed class AsyncLock
    {
        private static string _lastClearDay;
        private static readonly Dictionary<string, object> LockList = new Dictionary<string, object>();
        private static readonly object LockDic = new object();
        public static object LockOnValue(string key)
        {
            lock (LockDic)
            {
                object lockObj;
                if (LockList.TryGetValue(key, out lockObj)) return lockObj;
                //her Gece 1'de Tüm Keyler Temizlenir.
                if (_lastClearDay != DateTime.Today.ToShortDateString() && DateTime.Now.Hour == 1)
                {
                    _lastClearDay = DateTime.Today.ToShortDateString();
                    LockList.Clear();
                }
                lockObj = new object();
                LockList.Add(key, lockObj);
                return lockObj;
            }
        }
    }
}
