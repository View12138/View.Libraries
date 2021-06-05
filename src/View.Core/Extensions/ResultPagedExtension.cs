using System.Collections.Generic;
using View.Core.Models.Paging;
using View.Core.Models.Results;

namespace View.Core.Extensions
{
    /// <summary>
    /// IPagedResult 扩展。
    /// </summary>
    public static class ResultPagedExtension
    {
        /// <summary>
        /// 将对象转换为 <see cref="IPagedResult{T}"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="padegList">响应列表数据</param>
        /// <param name="status">响应状态<para>0：成功、其它：失败</para></param>
        /// <param name="message">响应消息</param>
        /// <returns>分页响应结果</returns>
        public static IPagedResult<T> ToPagedResult<T>(this IPagedList<T> padegList, int status = 0, string message = "")
        {
            return new InnerPagedResult<T>(padegList, status, message);
        }

        private class InnerPagedResult<T> : IPagedResult<T>
        {
            public InnerPagedResult(IPagedList<T> data, int status, string message)
            {
                PagedInfo = new InnerPagedInfo(data.Index, data.Size, data.Count);
                Data = data;
                Status = status;
                Message = message;
            }
            public IPagedInfo PagedInfo { get; }

            public int Status { get; }

            public IEnumerable<T> Data { get; }

            public string Message { get; }

            private class InnerPagedInfo : IPagedInfo
            {
                internal InnerPagedInfo(int index, int size, long count)
                {
                    Index = index;
                    Size = size;
                    Count = count;
                }

                public int Index { get; }
                public int Size { get; }
                public long Count { get; }
                public long Pages => (Count + Size - 1) / Size;
                public bool HasPrevious => Index > 0;
                public bool HasNext => Index + 1 < Pages;
            }
        }
    }
}
