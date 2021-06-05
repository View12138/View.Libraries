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
        public int Index { get; }
        /// <summary>
        /// 每页行数
        /// </summary>
        public int Size { get; }
        /// <summary>
        /// 总行数
        /// </summary>
        public long Count { get; }
        /// <summary>
        /// 总页数
        /// </summary>
        public long Pages { get; }
        /// <summary>
        /// 是否有上一页
        /// </summary>
        public bool HasPrevious { get; }
        /// <summary>
        /// 是否有下一页
        /// </summary>
        public bool HasNext { get; }
    }

}
