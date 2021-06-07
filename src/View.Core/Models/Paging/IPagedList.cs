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

}
