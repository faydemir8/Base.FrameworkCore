using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Base.Framework.Core.CacheManager.Objects.Classes
{
    public interface ICacheQueryable<T> : IQueryable<T>
    {
        string CacheKey { get; set; }
    }
    public class CacheQueryable<T> : ICacheQueryable<T>, IOrderedQueryable<T>, IAsyncEnumerable<T>
    {
        private IQueryable<T> _queryable;
        public CacheQueryable(IQueryable<T> queryable)
        {
            _queryable = queryable;
        }

        Expression IQueryable.Expression => _queryable.Expression;

        Type IQueryable.ElementType => typeof(T);

        IQueryProvider IQueryable.Provider => _queryable.Provider;

        public IEnumerator<T> GetEnumerator() => _queryable.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => _queryable.GetEnumerator();

        IAsyncEnumerator<T> IAsyncEnumerable<T>.GetEnumerator() => ((IAsyncEnumerable<T>)_queryable).GetEnumerator();

        public override string ToString() => _queryable.Expression.ToString();

        public string CacheKey { get; set; }
    }
}
