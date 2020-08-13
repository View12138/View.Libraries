using System;
using View.Data.Sql.Models;

namespace View.Data.Sql
{
    /// <summary>
    /// 数据类型转换。
    /// </summary>
    public static class TypeConverter
    {
        // SQLType => CSharpType
        /// <summary>
        /// <see langword="SQL"/> 通用数据类型转 <see langword="C#"/> 数据类型。
        /// </summary>
        /// <param name="type"><see langword="SQL"/> 通用数据类型</param>
        /// <returns><see langword="C#"/> 数据类型</returns>
        public static Type SqlTypeToType(SqlType type)
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
        /// <summary>
        /// <see langword="SQL"/> 通用数据类型转 <see langword="C#"/> 数据类型。
        /// </summary>
        /// <param name="type"><see langword="SQL"/> 通用数据类型</param>
        /// <returns><see langword="C#"/> 数据类型</returns>
        public static Type SqlTypeToType(string type)
        {
            return SqlTypeToType(SqlStringToSqlType(type));
        }

        // CSharpType => SQLType
        /// <summary>
        /// <see langword="C#"/> 数据类型转 <see langword="SQL"/> 通用数据类型。
        /// </summary>
        /// <param name="type"><see langword="C#"/> 数据类型</param>
        /// <returns><see langword="SQL"/> 通用数据类型</returns>
        public static SqlType TypeToSsqlType(Type type)
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

        // SQLType <==> string
        /// <summary>
        /// <see langword="SQL"/> 数据类型转换为小写的字符串形式。
        /// </summary>
        /// <param name="type"><see langword="SQL"/> 数据类型</param>
        /// <returns>小写的字符串形式</returns>
        public static string SqlTypeToString(SqlType type)
        {
            return type.ToString().ToLower();
        }
        /// <summary>
        /// <see langword="SQL"/> 字符串形式数据类型转换为 <see cref="SqlType"/> 数据类型。
        /// </summary>
        /// <param name="str"><see langword="SQL"/> 数据类型的字符串形式</param>
        /// <returns><see langword="SQL"/> 通用数据类型</returns>
        public static SqlType SqlStringToSqlType(string str)
        {
            return (SqlType)Enum.Parse(typeof(SqlType), str, true);
        }

        // Type Length
        /// <summary>
        /// 获取 <see langword="C#"/> 数据类型的大小。
        /// </summary>
        /// <param name="type"> <see langword="C#"/> 数据类型</param>
        /// <returns></returns>
        public static int GetLength(Type type)
        {
            if (type.Name == typeof(short).Name)
            { return sizeof(short); }
            else if (type.Name == typeof(int).Name)
            { return sizeof(int); }
            else if (type.Name == typeof(long).Name)
            { return sizeof(long); }
            else if (type.Name == typeof(float).Name)
            { return sizeof(float); }
            else if (type.Name == typeof(double).Name)
            { return sizeof(double); }
            else if (type.Name == typeof(decimal).Name)
            { return sizeof(decimal); }
            else if (type.Name == typeof(byte[]).Name)
            { return 0; }
            else if (type.Name == typeof(byte).Name)
            { return sizeof(byte); }
            else if (type.Name == typeof(bool).Name)
            { return sizeof(bool); }
            else if (type.Name == typeof(DateTime).Name)
            { return 0; }
            else if (type.Name == typeof(Guid).Name)
            { return 0; }
            else if (type.Name == typeof(object).Name)
            { return 0; }
            else if (type.Name == typeof(string).Name)
            { return 255; }
            else
            { return 0; }
        }
    }
}
