namespace View.Core.Models.Paging
{
    /// <summary>
    /// 分页查询信息
    /// </summary>
    public class PagedQueryInfo
    {
        /// <summary>
        /// 初始化分页查询信息。
        /// </summary>
        /// <param name="index"></param>
        /// <param name="size"></param>
        /// <param name="count"></param>
        public PagedQueryInfo(int index = 1, int size = 100, long count = 0)
        {
            Index = index;
            Size = size;
            Count = count;
        }

        /// <summary>
        /// 当前页码
        /// </summary>
        public int Index { get; set; }
        /// <summary>
        /// 每页数据量
        /// </summary>
        public int Size { get; set; }
        /// <summary>
        /// 总行数
        /// </summary>
        public long Count { get; set; } = 0;
    }

}
