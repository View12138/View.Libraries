using System;
using System.Globalization;
using System.Reflection;

namespace View.Core.Models.Sql
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


    /// <summary>
    /// <see langword="SQL"/> 通用数据类型。
    /// </summary>
    class BaseSqlType
    {
        private readonly string typeName;

        /// <summary>
        /// ctor.
        /// </summary>
        /// <param name="typeName"></param>
        public BaseSqlType(string typeName)
        {
            this.typeName = typeName.ToLower();
        }

        public override string ToString()
        {
            return typeName;
        }
        public override bool Equals(object obj)
        {
            if (obj is BaseSqlType type)
            {
                return type.typeName == typeName;
            }
            else { return false; }
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// 整数值（没有小数点）。精度 5。
        /// </summary>
        public static BaseSqlType SmallInt => new BaseSqlType(nameof(SmallInt));
        /// <summary>
        /// 整数值（没有小数点）。精度 10。
        /// </summary>
        public static BaseSqlType Int => new BaseSqlType(nameof(Int));
        /// <summary>
        /// 整数值（没有小数点）。精度 10。
        /// </summary>
        public static BaseSqlType Integer => new BaseSqlType(nameof(Integer));
        /// <summary>
        /// 整数值（没有小数点）。精度 19。
        /// </summary>
        public static BaseSqlType BigInt => new BaseSqlType(nameof(BigInt));
        /// <summary>
        /// 整数值（没有小数点）。
        /// </summary>
        public static BaseSqlType Year => new BaseSqlType(nameof(Year));
        /// <summary>
        /// 整数值（没有小数点）。
        /// </summary>
        public static BaseSqlType MediumInt => new BaseSqlType(nameof(MediumInt));
        /// <summary>
        /// 近似数值，尾数精度 7。
        /// </summary>
        public static BaseSqlType Real => new BaseSqlType(nameof(Real));
        /// <summary>
        /// 近似数值，尾数精度 16。
        /// </summary>
        public static BaseSqlType Float => new BaseSqlType(nameof(Float));
        /// <summary>
        /// 近似数值。
        /// </summary>
        public static BaseSqlType Double => new BaseSqlType(nameof(Double));
        /// <summary>
        /// 近似数值。
        /// </summary>
        public static BaseSqlType Precision => new BaseSqlType(nameof(Precision));
        /// <summary>
        /// 布尔类型。
        /// </summary>
        public static BaseSqlType Bool => new BaseSqlType(nameof(Bool));
        /// <summary>
        /// 布尔类型。
        /// </summary>
        public static BaseSqlType Boolean => new BaseSqlType(nameof(Boolean));
        /// <summary>
        /// 精确数值。
        /// </summary>
        public static BaseSqlType Decimal => new BaseSqlType(nameof(Decimal));
        /// <summary>
        /// 精确数值。
        /// </summary>
        public static BaseSqlType Dec => new BaseSqlType(nameof(Dec));
        /// <summary>
        /// 精确数值。
        /// </summary>
        public static BaseSqlType Numeric => new BaseSqlType(nameof(Numeric));
        /// <summary>
        /// 精确数值。
        /// </summary>
        public static BaseSqlType Fixed => new BaseSqlType(nameof(Fixed));
        /// <summary>
        /// 金钱类型。
        /// </summary>
        public static BaseSqlType Money => new BaseSqlType(nameof(Money));
        /// <summary>
        /// 金钱类型。
        /// </summary>
        public static BaseSqlType SmallMoney => new BaseSqlType(nameof(SmallMoney));
        /// <summary>
        /// 二进制串。固定长度。
        /// </summary>
        public static BaseSqlType Binary => new BaseSqlType(nameof(Binary));
        /// <summary>
        /// 图片类型
        /// </summary>
        public static BaseSqlType Image => new BaseSqlType(nameof(Image));
        /// <summary>
        /// 二进制串。可变长度。
        /// </summary>
        public static BaseSqlType VarBinary => new BaseSqlType(nameof(VarBinary));
        /// <summary>
        /// 二进制串
        /// </summary>
        public static BaseSqlType Blob => new BaseSqlType(nameof(Blob));
        /// <summary>
        /// 二进制串
        /// </summary>
        public static BaseSqlType MediumBlob => new BaseSqlType(nameof(MediumBlob));
        /// <summary>
        /// 二进制串
        /// </summary>
        public static BaseSqlType TinyBlob => new BaseSqlType(nameof(TinyBlob));
        /// <summary>
        /// 小整数。
        /// </summary>
        public static BaseSqlType TinyInt => new BaseSqlType(nameof(TinyInt));
        /// <summary>
        /// 存储 TRUE 或 FALSE 值
        /// </summary>
        public static BaseSqlType Bit => new BaseSqlType(nameof(Bit));
        /// <summary>
        /// 日期。
        /// </summary>
        public static BaseSqlType Date => new BaseSqlType(nameof(Date));
        /// <summary>
        /// 时间。
        /// </summary>
        public static BaseSqlType Time => new BaseSqlType(nameof(Time));
        /// <summary>
        /// 日期。
        /// </summary>
        public static BaseSqlType DateTime => new BaseSqlType(nameof(DateTime));
        /// <summary>
        /// 日期。
        /// </summary>
        public static BaseSqlType SmallDateTime => new BaseSqlType(nameof(SmallDateTime));
        /// <summary>
        /// 时刻
        /// </summary>
        public static BaseSqlType TimeStamp => new BaseSqlType(nameof(TimeStamp));
        /// <summary>
        /// 唯一标识符
        /// </summary>
        public static BaseSqlType UniqueIdentifier => new BaseSqlType(nameof(UniqueIdentifier));
        /// <summary>
        /// Object
        /// </summary>
        public static BaseSqlType Variant => new BaseSqlType(nameof(Variant));
        /// <summary>
        /// 字符串。
        /// </summary>
        public static BaseSqlType Text => new BaseSqlType(nameof(Text));
        /// <summary>
        /// 字符串。
        /// </summary>
        public static BaseSqlType Char => new BaseSqlType(nameof(Char));
        /// <summary>
        /// 字符串。
        /// </summary>
        public static BaseSqlType NChar => new BaseSqlType(nameof(NChar));
        /// <summary>
        /// 字符串。
        /// </summary>
        public static BaseSqlType NText => new BaseSqlType(nameof(NText));
        /// <summary>
        /// 字符串。
        /// </summary>
        public static BaseSqlType NVarChar => new BaseSqlType(nameof(NVarChar));
        /// <summary>
        /// 字符串。
        /// </summary>
        public static BaseSqlType VarChar => new BaseSqlType(nameof(VarChar));
        /// <summary>
        /// 字符串。
        /// </summary>
        public static BaseSqlType TinyText => new BaseSqlType(nameof(TinyText));
        /// <summary>
        /// 字符串。
        /// </summary>
        public static BaseSqlType MediumText => new BaseSqlType(nameof(MediumText));
        /// <summary>
        /// 字符串。
        /// </summary>
        public static BaseSqlType LongText => new BaseSqlType(nameof(LongText));
    }
}
