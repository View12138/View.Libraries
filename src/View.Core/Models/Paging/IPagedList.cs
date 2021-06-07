using System.Collections;
using System.Collections.Generic;

namespace View.Core.Models.Paging
{
    /// <summary>
    /// 表示可按照索引单独访问的对象的集合。并且包括分页信息。
    /// </summary>
    public interface IPagedList : IEnumerable, IPagedInfo
    {

    }

    /// <summary>
    /// 表示可按照索引单独访问的对象的集合。并且包括分页信息。
    /// </summary>
    /// <typeparam name="T"> 列表中元素的类型。</typeparam>
    public interface IPagedList<T> : IList<T>, IPagedList
    {
        /// <summary>
        /// 总行数
        /// </summary>
        new long Count { get; }
        /// <summary>
        /// 获取当前 <see cref="IPagedList{T}"/> 中包含的行数。
        /// </summary>
        long CurrentCount { get; }
    }


    /// <summary>
    /// 表示可通过索引访问的对象的强类型列表。 提供用于对列表进行搜索、排序和操作的方法。并包含分页信息。
    /// </summary>
    /// <typeparam name="T">列表中元素的类型。</typeparam>
    public class PagedList<T> : List<T>, IPagedList<T>
    {
        /// <summary>
        /// 表示可通过索引访问的对象的强类型列表。 提供用于对列表进行搜索、排序和操作的方法。并包含分页信息。
        /// </summary>
        /// <param name="data"></param>
        /// <param name="index"></param>
        /// <param name="size"></param>
        /// <param name="count"></param>
        public PagedList(IEnumerable<T> data, int index, int size, long count)
            : base(data)
        {
            Index = index;
            Size = size;
            Count = count;
        }


        /// <summary>
        /// 当前页码
        /// </summary>
        public int Index { get; }
        /// <summary>
        /// 每页行数
        /// </summary>
        public int Size { get; }
        /// <summary>
        /// 总行数
        /// </summary>
        public new long Count { get; }
        /// <summary>
        /// 总页数
        /// </summary>
        public long Pages => (Count + Size - 1) / Size;

        /// <summary>
        /// 获取当前 <see cref="PagedList{T}"/> 中包含的行数。
        /// </summary>
        public long CurrentCount => base.Count;
        /// <summary>
        /// 是否有上一页
        /// </summary>
        public bool HasPrevious => Index > 0;
        /// <summary>
        /// 是否有下一页
        /// </summary>
        public bool HasNext => Index + 1 < Pages;
    }

}
