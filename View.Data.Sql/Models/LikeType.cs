using System;
using System.Collections.Generic;
using System.Text;

namespace View.Data.Sql.Models
{
    /// <summary>
    /// <see langword="like"/> 语句类型。
    /// </summary>
    public enum LikeType
    {
        /// <summary>
        /// 包含 <see langword="value"/>。
        /// <c>like '%<see langword="value"/>%'</c>
        /// </summary>
        Contains,
        /// <summary>
        /// 不包含 <see langword="value"/>。
        /// <c>not like '%<see langword="value"/>%'</c>
        /// </summary>
        NotContains,
        /// <summary>
        /// 以 <see langword="value"/> 开始。
        /// <c>like '<see langword="value"/>%'</c>
        /// </summary>
        StartWith,
        /// <summary>
        /// 不以 <see langword="value"/> 开始。
        /// <c>not like '<see langword="value"/>%'</c>
        /// </summary>
        NotStartWith,
        /// <summary>
        /// 以 <see langword="value"/> 结束。
        /// <c>like '%<see langword="value"/>'</c>
        /// </summary>
        EndWith,
        /// <summary>
        /// 不以 <see langword="value"/> 结束。
        /// <c>not like '%<see langword="value"/>'</c>
        /// </summary>
        NotEndWith,
    }
}
