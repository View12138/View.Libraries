using System;
using View.Core.Models.Sql;

namespace View.Core.Interfaces
{
    /// <summary>
    /// 提供一个将 Sql 类型转换为 <see cref="Type"/> 的类型转换器。
    /// </summary>
    public interface ISqlTypeConverter
    {
        /// <summary>
        /// 针对特定的类型进行特定的转换。
        /// </summary>
        /// <param name="sqlType">Sql 类型</param>
        /// <returns>null:使用默认类型</returns>
        Type ConvertTo(SqlType sqlType);
    }
}
