using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
        /// <param name="queryInfo">分页查询信息</param>
        /// <returns>一个 <see cref="IPagedQueryable{T}"/>，表示输入序列</returns>
        /// <exception cref="ArgumentNullException">source 为 null。</exception>
        public static IPagedQueryable<TElement> AsPagedQueryable<TElement>(this IEnumerable<TElement> source, PagedQueryInfo queryInfo)
        {
            return source.AsQueryable().AsPagedQueryable(queryInfo);
        }

        /// <summary>
        /// 将泛型 <see cref="IQueryable{T}"/> 转换为泛型 <see cref="IPagedQueryable{T}"/>。
        /// </summary>
        /// <typeparam name="TElement">source 的元素类型。</typeparam>
        /// <param name="queryable">要转换的泛型 <see cref="IQueryable{T}"/>。</param>
        /// <param name="queryInfo">分页查询信息</param>
        /// <returns>一个 <see cref="IPagedQueryable{T}"/>，表示输入序列</returns>
        public static IPagedQueryable<TElement> AsPagedQueryable<TElement>(this IQueryable<TElement> queryable, PagedQueryInfo queryInfo)
        {
            if (queryInfo.Index <= 0)
            { queryInfo.Index = 1; }

            if (queryInfo.Size <= 0)
            { queryInfo.Size = 100; }

            if (queryInfo.Count == 0)
            { queryInfo.Count = queryable.Count(); }

            queryable = queryable.Skip((queryInfo.Index - 1) * queryInfo.Size).Take(queryInfo.Size);

            return new InnerPagedQueryable<TElement>(queryable, queryInfo.Index, queryInfo.Size, queryInfo.Count);
        }

        #region Private Inner Class
        /// <summary>
        /// 表示作为 <see cref="IPagedQueryable{T}"/> 数据源的 <see cref="IEnumerable{T}"/> 集合。
        /// </summary>
        /// <typeparam name="T">集合中数据的类型。</typeparam>
        private class InnerPagedQueryable<T> : EnumerableQuery<T>,
            IEnumerable<T>, IEnumerable, IOrderedQueryable, IQueryable, IOrderedQueryable<T>, IQueryable<T>, IQueryProvider, IPagedQueryable, IPagedQueryable<T>
        {
            internal InnerPagedQueryable(Expression expression, int index, int size, long count)
                : base(expression)
            {
                Index = index;
                Size = size;
                Count = count;
            }
            internal InnerPagedQueryable(IEnumerable<T> enumerable, int index, int size, long count)
                : base(enumerable)
            {
                Index = index;
                Size = size;
                Count = count;
            }


            public int Index { get; }
            public int Size { get; }
            public long Count { get; }
            public long Pages => (Count + Size - 1) / Size;
            public bool HasPrevious => Index > 1;
            public bool HasNext => Index < Pages;
        }
        #endregion
    }

}
