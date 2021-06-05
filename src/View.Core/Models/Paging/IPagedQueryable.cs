using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace View.Core.Models.Paging
{
    /// <summary>
    /// 表示分页操作的结果。
    /// </summary>
    public interface IPagedQueryable : IEnumerable, IQueryable, IPagedInfo
    {
    }

    /// <summary>
    /// 表示分页操作的结果。
    /// </summary>
    /// <typeparam name="T">数据源的内容类型。</typeparam>
    public interface IPagedQueryable<out T> : IEnumerable<T>, IEnumerable, IQueryable, IQueryable<T>, IPagedQueryable
    {
    }
}
