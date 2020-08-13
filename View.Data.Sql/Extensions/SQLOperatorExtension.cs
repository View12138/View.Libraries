using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using View.Data.Sql.Models;

namespace View.Data.Sql.Extensions
{
    /// <summary>
    /// <see langword="SQL"/> 操作语句构建类。
    /// </summary>
    public static class SQLOperatorExtension
    {
        /// <summary>
        /// 构建字段的 <see langword="SQL"/> 表达形式。
        /// </summary>
        /// <param name="fieldName">字段名</param>
        /// <returns>使用 `` 包裹后的字段名</returns>
        public static string GetField(this string fieldName)
        {
            fieldName = fieldName.Replace('\'', ' ');
            fieldName = fieldName.Replace('`', ' ');
            return $"`{fieldName.Trim()}`";
        }
        /// <summary>
        /// 构建值对应的 <see langword="SQL"/> 表达形式。
        /// <list type="bullet">
        /// <item><see cref="string"/>、<see cref="char"/> 类型 使用 <c>''</c> 包裹</item>
        /// <item><see cref="DateTime"/> 类型 的数据使用 <c>yyyy-MM-dd hh:mm:ss.fffffffzzz</c> 格式化</item>
        /// <item>其它 类型使用 <see cref="object.ToString"/> 转换为此对象的字符串表达形式</item>
        /// </list>
        /// </summary>
        /// <param name="value">值</param>
        /// <returns>根据类型使用或不使用 '' 包裹值</returns>
        public static string GetValue(this object value)
        {
            if (value is string || value is char)
            { return $"'{value}'"; }
            else if (value is DateTime)
            { return $"'{value:yyyy-MM-dd HH:mm:ss.fffffffzzz}'"; }
            else
            { return $"{value}"; }
        }

        // 等于
        /// <summary>
        /// 构建 <see langword="="/> 语句。
        /// <c><see langword="fieldName"/> = <see langword="value"/></c>
        /// </summary>
        /// <param name="fieldName">字段名</param>
        /// <param name="value">值</param>
        /// <returns>构建完成的 SQL 子句</returns>
        public static string EQ(this string fieldName, object value) => $" {GetField(fieldName)} = {GetValue(value)} ";
        // 不等于
        /// <summary>
        /// 构建 <see langword="!="/> 语句。
        /// <c><see langword="fieldName"/> != <see langword="value"/></c>
        /// </summary>
        /// <param name="fieldName">字段名</param>
        /// <param name="value">值</param>
        /// <returns>构建完成的 SQL 子句</returns>
        public static string NE(this string fieldName, object value) => $" {GetField(fieldName)} <> {GetValue(value)} ";
        // 小于
        /// <summary>
        /// 构建 <see langword="&lt;"/> 语句。
        /// <c><see langword="fieldName"/> &lt; <see langword="value"/></c>
        /// </summary>
        /// <param name="fieldName">字段名</param>
        /// <param name="value">值</param>
        /// <returns>构建完成的 SQL 子句</returns>
        public static string LT(this string fieldName, object value) => $" {GetField(fieldName)} < {GetValue(value)} ";
        // 小于或等于
        /// <summary>
        /// 构建 <see langword="&lt;="/> 语句。
        /// <c><see langword="fieldName"/> &lt;= <see langword="value"/></c>
        /// </summary>
        /// <param name="fieldName">字段名</param>
        /// <param name="value">值</param>
        /// <returns>构建完成的 SQL 子句</returns>
        public static string LE(this string fieldName, object value) => $" {GetField(fieldName)} <= {GetValue(value)} ";
        // 大于
        /// <summary>
        /// 构建 <see langword="&gt;"/> 语句。
        /// <c><see langword="fieldName"/> &gt; <see langword="value"/></c>
        /// </summary>
        /// <param name="fieldName">字段名</param>
        /// <param name="value">值</param>
        /// <returns>构建完成的 SQL 子句</returns>
        public static string GT(this string fieldName, object value) => $" {GetField(fieldName)} > {GetValue(value)} ";
        // 大于或等于
        /// <summary>
        /// 构建 <see langword="&gt;="/> 语句。
        /// <c><see langword="fieldName"/> &gt;= <see langword="value"/></c>
        /// </summary>
        /// <param name="fieldName">字段名</param>
        /// <param name="value">值</param>
        /// <returns>构建完成的 SQL 子句</returns>
        public static string GE(this string fieldName, object value) => $" {GetField(fieldName)} >= {GetValue(value)} ";

        // like
        /// <summary>
        /// 构建 <see langword="like"/> 语句。
        /// <c><see langword="fieldName"/> like <see langword="value"/></c>
        /// </summary>
        /// <param name="fieldName">字段名</param>
        /// <param name="value">值</param>
        /// <param name="type">like 语句类型</param>
        /// <returns>构建完成的 SQL 子句</returns>
        public static string Like(this string fieldName, object value, LikeType type)
        {
            string _like;
            string _value;
            switch (type)
            {
                default:
                case LikeType.Contains:
                { _like = "like"; _value = $"'%{value}%'"; }
                break;
                case LikeType.NotContains:
                { _like = "not like"; _value = $"'%{value}%'"; }
                break;
                case LikeType.StartWith:
                { _like = "like"; _value = $"'{value}%'"; }
                break;
                case LikeType.NotStartWith:
                { _like = "not like"; _value = $"'{value}%'"; }
                break;
                case LikeType.EndWith:
                { _like = "like"; _value = $"'%{value}'"; }
                break;
                case LikeType.NotEndWith:
                { _like = "not like"; _value = $"'%{value}'"; }
                break;
            }
            return $" {GetField(fieldName)} {_like} {_value} ";
        }
        // null
        /// <summary>
        /// 构建 <see langword="is null"/> 语句。
        /// <c><see langword="fieldName"/> is null</c>
        /// </summary>
        /// <param name="fieldName">字段名</param>
        /// <returns>构建完成的 SQL 子句</returns>
        public static string IsNull(this string fieldName) => $" {GetField(fieldName)} is null ";
        /// <summary>
        /// 构建 <see langword="is not null"/> 语句。
        /// <c><see langword="fieldName"/> is null</c>
        /// </summary>
        /// <param name="fieldName">字段名</param>
        /// <returns>构建完成的 SQL 子句</returns>
        public static string IsNotNull(this string fieldName) => $" {GetField(fieldName)} is not null ";
        // between
        /// <summary>
        /// 构建 <see langword="betewwn and"/> 语句。
        /// <c><see langword="fieldName"/> between <see langword="leftValue"/> and <see langword="rightValue"/></c>
        /// </summary>
        /// <param name="fieldName">字段名</param>
        /// <param name="leftValue">左值</param>
        /// <param name="rightValue">右值</param>
        /// <returns>构建完成的 SQL 子句</returns>
        public static string Between(this string fieldName, object leftValue, object rightValue)
            => $" {fieldName} between {GetValue(leftValue)} and {GetValue(rightValue)} ";
        /// <summary>
        /// 构建 <see langword="not betewwn and"/> 语句。
        /// <c><see langword="fieldName"/> between <see langword="leftValue"/> and <see langword="rightValue"/></c>
        /// </summary>
        /// <param name="fieldName">字段名</param>
        /// <param name="leftValue">左值</param>
        /// <param name="rightValue">右值</param>
        /// <returns>构建完成的 SQL 子句</returns>
        public static string NotBetween(this string fieldName, object leftValue, object rightValue)
            => $" {fieldName} not between {GetValue(leftValue)} and {GetValue(rightValue)} ";
        // in
        /// <summary>
        /// 构建 <see langword="in"/> 语句。
        /// <c><see langword="fieldName"/> in (<see langword="values"/>)</c>
        /// </summary>
        /// <param name="fieldName">字段名</param>
        /// <param name="values">值</param>
        /// <returns>构建完成的 SQL 子句</returns>
        public static string In(this string fieldName, params object[] values)
        {
            List<string> list = new List<string>();
            values.ToList().ForEach((value) => list.Add(GetValue(value)));
            return $" {fieldName} in ({string.Join(",", list)}) ";
        }
        // not in
        /// <summary>
        /// 构建 <see langword="not in"/> 语句。
        /// <c><see langword="fieldName"/> not in (<see langword="values"/>)</c>
        /// </summary>
        /// <param name="fieldName">字段名</param>
        /// <param name="values">值</param>
        /// <returns>构建完成的 SQL 子句</returns>
        public static string NotIn(this string fieldName, params object[] values)
        {
            List<string> list = new List<string>();
            foreach (var _value in values)
            { list.Add(GetValue(_value)); }
            string value = string.Join(",", list);
            return $" {fieldName} not in ({value}) ";
        }
        // and
        /// <summary>
        /// 构建 <see langword="and"/> 语句。
        /// <c><see langword="left"/> and (<see langword="right"/>)</c>
        /// </summary>
        /// <param name="left">左值</param>
        /// <param name="right">右值</param>
        /// <returns>构建完成的 SQL 子句</returns>
        public static string And(this string left, string right)
        {
            return $" {left} and ({right}) ";
        }
        // or
        /// <summary>
        /// 构建 <see langword="or"/> 语句。
        /// <c><see langword="left"/> or (<see langword="right"/>)</c>
        /// </summary>
        /// <param name="left">左值</param>
        /// <param name="right">右值</param>
        /// <returns>构建完成的 SQL 子句</returns>
        public static string Or(this string left, string right)
        {
            return $" {left} or ({right}) ";
        }
        // limit
        /// <summary>
        /// 构建 <see langword="limit"/> 语句。
        /// <c><see langword="left"/> limit <see langword="index"/>,<see langword="rows"/></c>
        /// </summary>
        /// <param name="left"></param>
        /// <param name="index"></param>
        /// <param name="rows"></param>
        /// <returns>构建完成的 SQL 子句</returns>
        public static string Limit(this string left, int index, int rows)
        {
            return $" {left} limit {index},{rows} ";
        }
        // order by
        /// <summary>
        /// 构建 <see langword="order by"/> 语句。
        /// <c><see langword="left"/> order by <see langword="fieldName"/></c>
        /// </summary>
        /// <param name="left"></param>
        /// <param name="fieldName"></param>
        /// <param name="isDesc"></param>
        /// <returns>构建完成的 SQL 子句</returns>
        public static string OrderBy(this string left, string fieldName, bool isDesc = false)
        {
            string sort = isDesc ? "desc" : "asc";
            return $" {left} order by {GetField(fieldName)} {sort} ";
        }
    }
}
