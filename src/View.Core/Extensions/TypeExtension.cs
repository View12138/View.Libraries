using System;
using View.Core.Models.Sql;

namespace View.Core.Extensions
{
    /// <summary>
    /// <see cref="Type"/> 扩展。
    /// </summary>
    public static class TypeExtension
    {
        /// <summary>
        /// <see langword="C#"/> 数据类型转 <see langword="SQL"/> 通用数据类型。
        /// </summary>
        /// <param name="type"><see langword="C#"/> 数据类型</param>
        /// <returns><see langword="SQL"/> 通用数据类型</returns>
        public static SqlType ToSqlType(this Type type)
        {
            if (type.Name == typeof(short).Name)
            { return SqlType.SmallInt; }
            else if (type.Name == typeof(int).Name)
            { return SqlType.Int; }
            else if (type.Name == typeof(long).Name)
            { return SqlType.BigInt; }
            else if (type.Name == typeof(float).Name)
            { return SqlType.Real; }
            else if (type.Name == typeof(double).Name)
            { return SqlType.Float; }
            else if (type.Name == typeof(decimal).Name)
            { return SqlType.Decimal; }
            else if (type.Name == typeof(byte[]).Name)
            { return SqlType.VarBinary; }
            else if (type.Name == typeof(byte).Name)
            { return SqlType.TinyInt; }
            else if (type.Name == typeof(bool).Name)
            { return SqlType.Bit; }
            else if (type.Name == typeof(DateTime).Name)
            { return SqlType.DateTime; }
            else if (type.Name == typeof(Guid).Name)
            { return SqlType.UniqueIdentifier; }
            else if (type.Name == typeof(object).Name)
            { return SqlType.Variant; }
            else if (type.Name == typeof(string).Name)
            { return SqlType.VarChar; }
            else
            { return SqlType.VarChar; }

        }

        /// <summary>
        /// 获取类型的名称（简称形式）
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string ToPrimitiveName(this Type type)
        {
            if (type == typeof(sbyte))
            { return "sbyte"; }
            else if (type == typeof(byte))
            { return "byte"; }
            else if (type == typeof(short))
            { return "short"; }
            else if (type == typeof(ushort))
            { return "ushort"; }
            else if (type == typeof(int))
            { return "int"; }
            else if (type == typeof(uint))
            { return "uint"; }
            else if (type == typeof(long))
            { return "long"; }
            else if (type == typeof(ulong))
            { return "ulong"; }
            else if (type == typeof(char))
            { return "char"; }
            else if (type == typeof(float))
            { return "float"; }
            else if (type == typeof(double))
            { return "double"; }
            else if (type == typeof(bool))
            { return "bool"; }
            else if (type == typeof(decimal))
            { return "decimal"; }
            else if (type == typeof(string))
            { return "string"; }
            else if (type == typeof(object))
            { return "object"; }
            else
            { return type.Name; }
        }
    }
}
