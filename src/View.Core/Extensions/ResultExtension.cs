using View.Core.Models.Results;

namespace View.Core.Extensions
{
    /// <summary>
    /// IResult 扩展
    /// </summary>
    public static class ResultExtension
    {
        /// <summary>
        /// 将对象转换为 <see cref="IResult{T}"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data">响应数据</param>
        /// <param name="status">响应状态<para>0：成功、其它：失败</para></param>
        /// <param name="message">响应消息</param>
        /// <returns>响应结果</returns>
        public static IResult<T> ToResut<T>(this T data, int status = 0, string message = "")
        {
            return new InnerResult<T>(data, status, message);
        }

        class InnerResult<T> : IResult<T>
        {
            public InnerResult(T data, int status, string message)
            {
                Data = data;
                Status = status;
                Message = message;
            }

            public T Data { get; }

            public int Status { get; }

            public string Message { get; }
        }
    }
}
