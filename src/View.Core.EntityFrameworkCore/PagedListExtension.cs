using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using View.Core.Extensions;
using View.Core.Models.Paging;

namespace View.Core.EntityFrameworkCore
{
    /// <summary>
    /// 表示异步分页操作的结果。
    /// </summary>
    /// <typeparam name="T">数据源的内容类型。</typeparam>
    interface IAsyncPagedQueryable<T> : IPagedQueryable<T>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IList<T>> ToListAsync(CancellationToken cancellationToken = default);
    }

    /// <summary>
    /// 提供一组用于查询实现 <see cref="IPagedQueryable{T}"/> 的数据结构的 static 方法。
    /// </summary>
    static class AsyncPagedQueryableExtension
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
        public static IAsyncPagedQueryable<TElement> AsAsyncPagedQueryable<TElement>(this IEnumerable<TElement> source, int index, int size, long count = 0)
        {
            if (source == null) { throw new ArgumentNullException("source"); }

            if (source is IAsyncPagedQueryable<TElement> pagedQueryable)
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
        private class InnerPagedQueryable<T> :
            IEnumerable<T>, IOrderedQueryable, IQueryable, IOrderedQueryable<T>, IQueryable<T>, IPagedQueryable, IPagedQueryable<T>, IAsyncPagedQueryable<T>
        {

            private IEnumerable<T> enumerable;
            private IQueryable<T> queryable;
            private int index;
            private int size;
            private long count;

            internal InnerPagedQueryable(IEnumerable<T> enumerable, int index, int size, long count)/* : base(enumerable)*/
            {
                this.enumerable = enumerable;
                queryable = enumerable.AsQueryable();
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

            public Type ElementType => queryable.ElementType;

            public Expression Expression => queryable.Expression;

            public IQueryProvider Provider => queryable.Provider;

            public IEnumerator<T> GetEnumerator()
            {
                return enumerable.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return enumerable.GetEnumerator();
            }

            public async Task<IList<T>> ToListAsync(CancellationToken cancellationToken = default)
            {
                return await queryable.ToListAsync(cancellationToken);
            }
        }
        #endregion
    }

    /// <summary>
    /// 提供一组用于将查询实现 <see cref="IPagedQueryable{T}"/> 的数据结构转换为 <see cref="IPagedList{T}"/> 的 static 方法。
    /// </summary>
    public static class PagedListExtension
    {
        /// <summary>
        /// 从 <see cref="IEnumerable{T}"/> 创建一个 <see cref="InnerPagedList{T}"/>。
        /// </summary>
        /// <typeparam name="TSource">source 的元素类型。</typeparam>
        /// <param name="source">要从其创建 <see cref="InnerPagedList{T}"/> 的 <see cref="IEnumerable{T}"/>。</param>
        /// <param name="index">当前页</param>
        /// <param name="size">每页数据量</param>
        /// <param name="count">总数据量</param>
        /// <returns>一个包含输入序列中的元素的 <see cref="InnerPagedList{T}"/>。</returns>
        /// <exception cref="ArgumentNullException">source 为 null。</exception>
        public static async Task<IPagedList<TSource>> ToPagedListAsync<TSource>(this IEnumerable<TSource> source, int index, int size, long count = 0)
        {
            return await source.AsPagedQueryable(index, size, count).ToPagedListAsync();
        }

        /// <summary>
        /// 从 <see cref="IPagedQueryable{T}"/> 创建一个 <see cref="IPagedList{T}"/>。
        /// </summary>
        /// <typeparam name="TSource">source 的元素类型。</typeparam>
        /// <param name="source">要从其创建 <see cref="IPagedList{T}"/> 的 <see cref="IPagedQueryable{T}"/>。</param>
        /// <param name="cancellationToken"></param>
        /// <returns>一个包含输入序列中的元素的 <see cref="IPagedList{T}"/>。</returns>
        /// <exception cref="ArgumentNullException">source 为 null。</exception>
        public static async Task<IPagedList<TSource>> ToPagedListAsync<TSource>(this IPagedQueryable<TSource> source, CancellationToken cancellationToken = default)
        {
            return new PagedList<TSource>(await source.ToListAsync(), source.Index, source.Size, source.Count);
        }
    }
}
