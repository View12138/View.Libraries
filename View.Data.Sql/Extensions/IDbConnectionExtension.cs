using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Dapper.Contrib.Extensions;
using View.Core.Extensions;
using View.Core.Models;

namespace View.Data.Sql.Extensions
{
    /// <summary>
    /// IDbConnection 扩展类
    /// </summary>
    public static class IDbConnectionExtension
    {
        /// <summary>
        /// 分页获取指定的实体类。
        /// <para>默认使用条件表达式，当条件表达式无法完成筛选时使用 where 子句</para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="db"></param>
        /// <param name="pagination">分页标记</param>
        /// <param name="predicate">条件表达式</param>
        /// <returns></returns>
        public static async Task<PaginationResult<IEnumerable<T>>> GetPaginationResultAsync<T>(this IDbConnection db, Pagination pagination, Expression<Func<T, bool>> predicate = null) where T : IEntity
        {
            var type = typeof(T);

            string tableName = type.Name.ToLower();
            object[] attributes = type.GetCustomAttributes(typeof(TableAttribute), true);
            foreach (object attribute in attributes)
            {
                if (attribute is TableAttribute table)
                { tableName = table.Name; break; }
            }

            string _condition = "";
            if (predicate != null)
            { _condition = $"and {ExpressionExtension.ExpressionToSql(predicate)}"; }
            string sql = $"{tableName} where true {_condition}";
            string sqlPage = $"select * from {sql} limit {(pagination.Index - 1) * pagination.Size},{pagination.Size}";
            var resultData = await db.QueryAsync<T>(sqlPage); // 获取分页结果
            int count = await db.QueryFirstAsync<int>($"select count(*) from {sql}"); // 总条目数

            return pagination.GetResult(resultData, count);
        }
        /// <summary>
        /// 分页获取指定的实体类。
        /// <para>不首先使用此表达式</para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="db"></param>
        /// <param name="pagination">分页标记</param>
        /// <param name="condition">where 子句</param>
        /// <returns></returns>
        public static async Task<PaginationResult<IEnumerable<T>>> GetPaginationResultAsync<T>(this IDbConnection db, Pagination pagination, string condition = "") where T : IEntity
        {
            var type = typeof(T);

            string tableName = type.Name.ToLower();
            object[] attributes = type.GetCustomAttributes(typeof(TableAttribute), true);
            foreach (object attribute in attributes)
            {
                if (attribute is TableAttribute table)
                { tableName = table.Name; break; }
            }
            string _condition = "";
            if (!condition.IsNullOrWhiteSpace())
            { _condition = $"and {condition}"; }
            string sql = $"{tableName} where true {_condition}";
            string sqlPage = $"select * from {sql} limit {(pagination.Index - 1) * pagination.Size},{pagination.Size}";
            var resultData = await db.QueryAsync<T>(sqlPage); // 获取分页结果
            int count = await db.QueryFirstAsync<int>($"select count(*) from {sql}"); // 总条目数

            return pagination.GetResult(resultData, count);
        }
    }

    /// <summary>
    /// 表达式扩展
    /// </summary>
    static class ExpressionExtension
    {
        /// <summary>
        /// 获取运算符
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string GetOperator(ExpressionType type)
        {
            switch (type)
            {
                case ExpressionType.OrElse: return "or";
                case ExpressionType.Or: return "|";
                case ExpressionType.AndAlso: return "and";
                case ExpressionType.And: return "&";
                case ExpressionType.GreaterThan: return ">";
                case ExpressionType.GreaterThanOrEqual: return ">=";
                case ExpressionType.LessThan: return "<";
                case ExpressionType.LessThanOrEqual: return "<=";
                case ExpressionType.NotEqual: return "<>";
                case ExpressionType.Add: return "+";
                case ExpressionType.Subtract: return "-";
                case ExpressionType.Multiply: return "*";
                case ExpressionType.Divide: return "/";
                case ExpressionType.Modulo: return "%";
                case ExpressionType.Equal: return "=";
                default: throw new NotSupportedException($"不支持的运算符 : {type}");
            }
        }

        /// <summary>
        /// 将拉姆达表达式转换为 SQL 子句
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static string ExpressionToSql(Expression expression)
        {
            if (expression is LambdaExpression lambda)
            { // Lambda 表达式
                return ExpressionToSql(lambda.Body);
            }
            else if (expression is BinaryExpression binary)
            {
                string left = ExpressionToSql(binary.Left);
                string @operator = GetOperator(binary.NodeType);
                string right = ExpressionToSql(binary.Right);
                if (binary.NodeType == ExpressionType.OrElse || binary.NodeType == ExpressionType.AndAlso)
                { return $"({left} {@operator} {right})"; }
                else if (binary.Right.Type == typeof(bool))
                { throw new NotSupportedException("不支持 bool 和 常量比较 : bool b = false;\n b == true"); }
                else
                {
                    if (right == "NULL")
                    {
                        if (binary.NodeType == ExpressionType.Equal)
                        { @operator = "is"; }
                        else
                        { @operator = "is not"; }
                    }
                    return $"({left} {@operator} {right})";
                }
            }
            else if (expression is MemberExpression member)
            {
                if (member.Member is PropertyInfo property)
                {
                    if (property.PropertyType == typeof(bool))
                    { return $"`{member.Member.Name}`=1"; }
                    else
                    { return $"`{member.Member.Name}`"; }
                }
                else if (member.Member is FieldInfo field)
                {
                    UnaryExpression cast = Expression.Convert(member, typeof(object));
                    var value = Expression.Lambda<Func<object>>(cast).Compile().Invoke();
                    return ObjectToString(value);
                }
                else
                { throw new NotSupportedException($"不支持的情况 : {member.Member}"); }

            }
            else if (expression is MethodCallExpression methodCall)
            {
                if (methodCall.NodeType == ExpressionType.Call)
                {
                    var prop = ExpressionToSql(methodCall.Object);
                    if (methodCall.Method == typeof(string).GetMethod("Contains", new Type[] { typeof(string) }))
                    { return $"{prop} like '%{ExpressionToSql(methodCall.Arguments[0]).Replace("'", "")}%'".Replace("\"", ""); }
                    else if (methodCall.Method == typeof(string).GetMethod("StartsWith", new Type[] { typeof(string) }))
                    { return $"{prop} like '{ExpressionToSql(methodCall.Arguments[0]).Replace("'", "")}%'".Replace("\"", ""); }
                    else if (methodCall.Method == typeof(string).GetMethod("EndsWith", new Type[] { typeof(string) }))
                    { return $"{prop} like '%{ExpressionToSql(methodCall.Arguments[0]).Replace("'", "")}'".Replace("\"", ""); }
                    else { throw new NotSupportedException($"不支持的方法 : {methodCall.Method.Name}"); }
                }
                else
                { throw new NotImplementedException($"暂未考虑到此情况 : {methodCall.NodeType}"); }
            }
            else if (expression is ConstantExpression constant)
            {
                return ObjectToString(constant.Value);
            }
            else if (expression is UnaryExpression unary)
            {
                if (unary.NodeType == ExpressionType.Not && unary.Operand is MethodCallExpression notMethodCall)
                {
                    if (notMethodCall.NodeType == ExpressionType.Call)
                    {
                        var prop = ExpressionToSql(notMethodCall.Object);
                        if (notMethodCall.Method == typeof(string).GetMethod("Contains", new Type[] { typeof(string) }))
                        { return $"{prop} not like '%{ExpressionToSql(notMethodCall.Arguments[0]).Replace("'", "")}%'".Replace("\"", ""); }
                        else if (notMethodCall.Method == typeof(string).GetMethod("StartsWith", new Type[] { typeof(string) }))
                        { return $"{prop} not like '{ExpressionToSql(notMethodCall.Arguments[0]).Replace("'", "")}%'".Replace("\"", ""); }
                        else if (notMethodCall.Method == typeof(string).GetMethod("EndsWith", new Type[] { typeof(string) }))
                        { return $"{prop} not like '%{ExpressionToSql(notMethodCall.Arguments[0]).Replace("'", "")}'".Replace("\"", ""); }
                        else { throw new NotSupportedException($"不支持的方法 : {notMethodCall.Method.Name}"); }
                    }
                    else
                    { throw new NotImplementedException($"暂未考虑到此情况 : {notMethodCall.NodeType}"); }
                }
                else if (unary.NodeType == ExpressionType.Not && unary.Operand is MemberExpression notMember)
                {
                    if (notMember.Member is PropertyInfo property)
                    {
                        if (property.PropertyType == typeof(bool))
                        { return $"`{notMember.Member.Name}`<>1"; }
                        else
                        { return ExpressionToSql(notMember); }
                    }
                    else
                    { throw new NotSupportedException($"不支持字段 : {notMember.Member.Name}"); }
                }
                else
                { return ExpressionToSql(unary.Operand); }
            }
            else
            {
                throw new NotSupportedException($"未支持的表达式 : {expression.GetType().Name}");
            }
        }

        /// <summary>
        /// 将对象的值转换为 SQL 中表达形式
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private static string ObjectToString(object value)
        {
            if (value == null)
            { return "NULL"; }
            else if (value is string @string)
            { return $"'{@string}'"; }
            else if (value is DateTime time)
            { return $"'{time:yyyy-MM-dd HH:mm:ss}'"; }
            else if (value is bool @bool)
            { return @bool ? "1" : "0"; }
            else
            { return value.ToString(); }
        }
    }
}
