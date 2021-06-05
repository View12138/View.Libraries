using System;
using System.Collections.Generic;
using System.Text;

namespace View.Core.Models.Results
{
    /// <summary>
    /// 响应结果
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IResult<T>
    {
        /// <summary>
        /// 响应数据。
        /// </summary>
        T Data { get; }

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
