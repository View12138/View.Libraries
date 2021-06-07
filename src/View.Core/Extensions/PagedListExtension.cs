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
            return new PagedList<TSource>(source.ToList(), source.Index, source.Size, source.Count);
        }
    }

}
