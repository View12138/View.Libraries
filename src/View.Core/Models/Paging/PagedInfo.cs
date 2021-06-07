namespace View.Core.Models.Paging
{
    /// <summary>
    /// 分页结果信息
    /// </summary>
    public interface IPagedInfo
    {
        /// <summary>
        /// 当前页码
        /// </summary>
        int Index { get; }
        /// <summary>
        /// 每页行数
        /// </summary>
        int Size { get; }
        /// <summary>
        /// 总行数
        /// </summary>
        long Count { get; }
        /// <summary>
        /// 总页数
        /// </summary>
        long Pages { get; }
        /// <summary>
        /// 是否有上一页
        /// </summary>
        bool HasPrevious { get; }
        /// <summary>
        /// 是否有下一页
        /// </summary>
        bool HasNext { get; }
    }

}
