using System;
using System.Collections.Generic;
using System.Text;

namespace View.Data.Sql.Models
{
    /// <summary>
    /// <see langword="SQL"/> 通用数据类型。
    /// </summary>
    public enum SqlType : int
    {
        /// <summary>
        /// 整数值（没有小数点）。精度 5。
        /// </summary>
        SmallInt,
        /// <summary>
        /// 整数值（没有小数点）。精度 10。
        /// </summary>
        Int,
        /// <summary>
        /// 整数值（没有小数点）。精度 10。
        /// </summary>
        Integer,
        /// <summary>
        /// 整数值（没有小数点）。精度 19。
        /// </summary>
        BigInt,
        /// <summary>
        /// 整数值（没有小数点）。
        /// </summary>
        Year,
        /// <summary>
        /// 整数值（没有小数点）。
        /// </summary>
        MediumInt,
        /// <summary>
        /// 近似数值，尾数精度 7。
        /// </summary>
        Real,
        /// <summary>
        /// 近似数值，尾数精度 16。
        /// </summary>
        Float,
        /// <summary>
        /// 近似数值。
        /// </summary>
        Double,
        /// <summary>
        /// 近似数值。
        /// </summary>
        Precision,
        /// <summary>
        /// 布尔类型。
        /// </summary>
        Bool,
        /// <summary>
        /// 布尔类型。
        /// </summary>
        Boolean,
        /// <summary>
        /// 精确数值。
        /// </summary>
        Decimal,
        /// <summary>
        /// 精确数值。
        /// </summary>
        Dec,
        /// <summary>
        /// 精确数值。
        /// </summary>
        Numeric,
        /// <summary>
        /// 精确数值。
        /// </summary>
        Fixed,
        /// <summary>
        /// 金钱类型。
        /// </summary>
        Money,
        /// <summary>
        /// 金钱类型。
        /// </summary>
        SmallMoney,
        /// <summary>
        /// 二进制串。固定长度。
        /// </summary>
        Binary,
        /// <summary>
        /// 图片类型
        /// </summary>
        Image,
        /// <summary>
        /// 二进制串。可变长度。
        /// </summary>
        VarBinary,
        /// <summary>
        /// 二进制串
        /// </summary>
        Blob,
        /// <summary>
        /// 二进制串
        /// </summary>
        MediumBlob,
        /// <summary>
        /// 二进制串
        /// </summary>
        TinyBlob,
        /// <summary>
        /// 小整数。
        /// </summary>
        TinyInt,
        /// <summary>
        /// 存储 TRUE 或 FALSE 值
        /// </summary>
        Bit,
        /// <summary>
        /// 日期。
        /// </summary>
        Date,
        /// <summary>
        /// 时间。
        /// </summary>
        Time,
        /// <summary>
        /// 日期。
        /// </summary>
        DateTime,
        /// <summary>
        /// 日期。
        /// </summary>
        SmallDateTime,
        /// <summary>
        /// 时刻
        /// </summary>
        TimeStamp,
        /// <summary>
        /// 唯一标识符
        /// </summary>
        UniqueIdentifier,
        /// <summary>
        /// Object
        /// </summary>
        Variant,
        /// <summary>
        /// 字符串。
        /// </summary>
        Text,
        /// <summary>
        /// 字符串。
        /// </summary>
        Char,
        /// <summary>
        /// 字符串。
        /// </summary>
        NChar,
        /// <summary>
        /// 字符串。
        /// </summary>
        NText,
        /// <summary>
        /// 字符串。
        /// </summary>
        NVarChar,
        /// <summary>
        /// 字符串。
        /// </summary>
        VarChar,
        /// <summary>
        /// 字符串。
        /// </summary>
        TinyText,
        /// <summary>
        /// 字符串。
        /// </summary>
        MediumText,
        /// <summary>
        /// 字符串。
        /// </summary>
        LongText,
    }
}
