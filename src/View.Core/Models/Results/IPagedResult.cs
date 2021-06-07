using System.Collections.Generic;
using View.Core.Models.Paging;

namespace View.Core.Models.Results
{
    /// <summary>
    /// 分页响应结果
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IPagedResult<T>
    {
        /// <summary>
        /// 响应分页信息
        /// </summary>
        IPagedInfo PagedInfo { get; }

        /// <summary>
        /// 响应列表数据数据。
        /// </summary>
        IEnumerable<T> Data { get; }

        /// <summary>
        /// 响应状态。
        /// <para>0：成功、其它：失败</para>
        /// </summary>
        int Status { get; }

        /// <summary>
        /// 响应消息
        /// </summary>
        string Message { get; }
    }
}
