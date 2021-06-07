using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using View.Core.Models.Paging;

namespace View.Core.Extensions
{
    /// <summary>
    /// 提供一组用于查询实现 <see cref="IPagedQueryable{T}"/> 的数据结构的 static 方法。
    /// </summary>
    public static class PagedQueryableExtension
    {
        /// <summary>
        /// 将泛型 <see cref="IEnumerable{T}"/> 转换为泛型 <see cref="IPagedQueryable{T}"/>。
        /// </summary>
        /// <typeparam name="TElement">source 的元素类型。</typeparam>
        /// <param name="source">要转换的序列。</param>
        /// <param name="index">当前页</param>
        /// <param name="size">每页数据量</param>
        /// <param name="count">总数据量</param>
        /// <returns>一个 <see cref="IPagedQueryable{T}"/>，表示输入序列</returns>
        /// <exception cref="ArgumentNullException">source 为 null。</exception>
        public static IPagedQueryable<TElement> AsPagedQueryable<TElement>(this IEnumerable<TElement> source, int index, int size, long count = 0)
        {
            if (source == null) { throw new ArgumentNullException("source"); }

            if (source is IPagedQueryable<TElement> pagedQueryable)
            { return pagedQueryable; }
            else
            {
                if (index <= 0) { index = 1; }
                if (size <= 0) { size = 100; }
                if (count == 0) { count = source.LongCount(); }

                source = source.Skip((index - 1) * size).Take(size);

                return new InnerPagedQueryable<TElement>(source, index, size, count);
            }
        }

        #region Private Inner Class
        /// <summary>
        /// 表示作为 <see cref="IPagedQueryable{T}"/> 数据源的 <see cref="IEnumerable{T}"/> 集合。
        /// </summary>
        /// <typeparam name="T">集合中数据的类型。</typeparam>
        private class InnerPagedQueryable<T> : EnumerableQuery<T>,
            IEnumerable<T>, IEnumerable, IOrderedQueryable, IQueryable, IOrderedQueryable<T>, IQueryable<T>, IQueryProvider, IPagedQueryable, IPagedQueryable<T>
        {
            private int index;
            private int size;
            private long count;

            internal InnerPagedQueryable(IEnumerable<T> enumerable, int index, int size, long count) : base(enumerable)
            {
                this.index = index;
                this.size = size;
                this.count = count;
            }

            public int Index => index;
            public int Size => size;
            public long Count => count;
            public long Pages => (Count + Size - 1) / Size;
            public bool HasPrevious => Index > 1;
            public bool HasNext => Index < Pages;
        }
        #endregion
    }

}
