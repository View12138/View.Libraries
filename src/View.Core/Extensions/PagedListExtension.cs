using System;
using System.Collections.Generic;
using System.Linq;
using View.Core.Models.Paging;

namespace View.Core.Extensions
{
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
        public static IPagedList<TSource> ToPagedList<TSource>(this IEnumerable<TSource> source, int index, int size, long count = 0)
        {
            return source.AsPagedQueryable(index, size, count).ToPagedList();
        }

        /// <summary>
        /// 从 <see cref="IPagedQueryable{T}"/> 创建一个 <see cref="IPagedList{T}"/>。
        /// </summary>
        /// <typeparam name="TSource">source 的元素类型。</typeparam>
        /// <param name="source">要从其创建 <see cref="IPagedList{T}"/> 的 <see cref="IPagedQueryable{T}"/>。</param>
        /// <returns>一个包含输入序列中的元素的 <see cref="IPagedList{T}"/>。</returns>
        /// <exception cref="ArgumentNullException">source 为 null。</exception>
        public static IPagedList<TSource> ToPagedList<TSource>(this IPagedQueryable<TSource> source)
        {
            return new InnerPagedList<TSource>(source.ToList(), source.Index, source.Size, source.Count);
        }


        #region Private Inner Class
        /// <summary>
        /// 表示可通过索引访问的对象的强类型列表。 提供用于对列表进行搜索、排序和操作的方法。并包含分页信息。
        /// </summary>
        /// <typeparam name="T">列表中元素的类型。</typeparam>
        private class InnerPagedList<T> : List<T>, IPagedList<T>
        {
            internal InnerPagedList(IEnumerable<T> data, int index, int size, long count)
                : base(data)
            {
                Index = index;
                Size = size;
                Count = count;
            }
            public int Index { get; }
            public int Size { get; }
            public new long Count { get; }
            public long Pages => (Count + Size - 1) / Size;
            public long CurrentCount => base.Count;
            public bool HasPrevious => Index > 0;
            public bool HasNext => Index + 1 < Pages;
        }
        #endregion
    }

}
