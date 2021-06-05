using System;
using View.Core.Models.Sql;

namespace View.Core.Extensions
{
    /// <summary>
    /// <see cref="SqlType"/> 扩展。
    /// </summary>
    public static class SqlTypeExtension
    {
        /// <summary>
        /// <see langword="SQL"/> 通用数据类型转 <see langword="C#"/> 数据类型。
        /// </summary>
        /// <param name="type"><see langword="SQL"/> 通用数据类型</param>
        /// <returns><see langword="C#"/> 数据类型</returns>
        public static Type ToType(this SqlType type)
        {
            switch (type)
            {
                case SqlType.SmallInt: return typeof(short);
                case SqlType.MediumInt:
                case SqlType.Integer:
                case SqlType.Year:
                case SqlType.Int: return typeof(int);
                case SqlType.BigInt: return typeof(long);
                case SqlType.Real: return typeof(float);
                case SqlType.Double:
                case SqlType.Precision:
                case SqlType.Float: return typeof(double);
                case SqlType.Dec:
                case SqlType.Decimal:
                case SqlType.Money:
                case SqlType.Numeric:
                case SqlType.Fixed:
                case SqlType.SmallMoney: return typeof(decimal);
                case SqlType.Binary:
                case SqlType.Image:
                case SqlType.Blob:
                case SqlType.MediumBlob:
                case SqlType.TinyBlob:
                case SqlType.LongText:
                case SqlType.VarBinary: return typeof(byte[]);
                case SqlType.TinyInt: return typeof(byte);
                case SqlType.Bool:
                case SqlType.Boolean:
                case SqlType.Bit: return typeof(bool);
                case SqlType.Date:
                case SqlType.Time:
                case SqlType.DateTime:
                case SqlType.SmallDateTime: return typeof(DateTime);
                case SqlType.TimeStamp: return typeof(uint);
                case SqlType.UniqueIdentifier: return typeof(Guid);
                case SqlType.Variant: return typeof(object);
                case SqlType.Text:
                case SqlType.Char:
                case SqlType.NChar:
                case SqlType.NText:
                case SqlType.NVarChar:
                case SqlType.VarChar:
                case SqlType.TinyText:
                case SqlType.MediumText:
                default: return typeof(string);
            }
        }
    }
}
