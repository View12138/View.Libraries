using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using View.Core.Interfaces;

namespace View.Core.Extensions
{
    /// <summary>
    /// Lambda 表达式扩展
    /// </summary>
    public static class ExpressionToSqlExtension
    {
        /// <summary>
        /// 内部处理方法
        /// </summary>
        static class ExpressHandler
        {
            /// <summary>
            /// 运算符 解析
            /// </summary>
            /// <param name="type"></param>
            /// <returns></returns>
            public static string ToSql(ExpressionType type)
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
            /// 将常量表达式转换为 <see langword="SQL"/> 值形式。
            /// <para>
            /// • <see cref="string"/>、<see cref="char"/>、<see cref="DateTime"/> 类型 使用 <c>''</c> 包裹 <br />
            /// • <see cref="DateTime"/> 类型 的数据使用 <see cref="DateTimeFormat"/> 格式化 <br />
            /// • <see cref="bool"/> 类型 的数据格式化为 1(<see langword="true"/>) 或 0(<see langword="false"/>) <br />
            /// • <see cref="Enum"/> 类型 的数据格式化为 <see cref="int"/> 类型 <br />
            /// • 其它 类型使用 <see cref="object.ToString"/> 转换为此对象的字符串表达形式 <br />
            /// </para>
            /// </summary>
            /// <param name="value">常量表达式</param>
            /// <returns>Sql 中的值表达形式</returns>
            public static string ToSqlValue(object value)
            {
                if (value == null)
                { return "NULL"; }
                else if (value is char @char)
                { return $"'{@char}'"; }
                else if (value is string @string)
                { return $"'{@string}'"; }
                else if (value is DateTime time)
                { return $"'{time.ToString(DateTimeFormat)}'"; }
                else if (value is bool @bool)
                { return @bool ? "1" : "0"; }
                else if (value is Enum @enum)
                { return ((int)Enum.Parse(value.GetType(), @enum.ToString())).ToString(); }
                else
                { return value.ToString(); }
            }

            /// <summary>
            /// 判断属性是否是 <see cref="DateTime"/> 结构的默认属性
            /// </summary>
            /// <param name="member">属性表达式</param>
            /// <returns></returns>
            public static bool IsDateTimeDefaultProperty(MemberExpression member)
            {
                if (member.Member is PropertyInfo property)
                {
                    bool isDefaultName = property.Name == nameof(DateTime.Date) ||
                        property.Name == nameof(DateTime.Day) ||
                        property.Name == nameof(DateTime.DayOfWeek) ||
                        property.Name == nameof(DateTime.DayOfYear) ||
                        property.Name == nameof(DateTime.Hour) ||
                        property.Name == nameof(DateTime.Kind) ||
                        property.Name == nameof(DateTime.Millisecond) ||
                        property.Name == nameof(DateTime.Minute) ||
                        property.Name == nameof(DateTime.Now) ||
                        property.Name == nameof(DateTime.Second) ||
                        property.Name == nameof(DateTime.Ticks) ||
                        property.Name == nameof(DateTime.TimeOfDay) ||
                        property.Name == nameof(DateTime.Today) ||
                        property.Name == nameof(DateTime.UtcNow) ||
                        property.Name == nameof(DateTime.Year);
                    return member.Expression == null && isDefaultName;
                }
                else
                { return false; }
            }
        }

        /// <summary>
        /// 获取或设置 格式化日期时所采用的格式化字符串
        /// <para>默认使用 <see cref="Core.Values.DateTimeValue.NormalDateTimeFormat"/> 格式化</para>
        /// </summary>
        public static string DateTimeFormat { get; set; } = Values.DateTimeValue.NormalDateTimeFormat;

        /// <summary>
        /// 获取或设置 在构造条件时是否确定优先级 ( 即 : 是否使用括号包含不同优先级的表达式 )
        /// <para>默认不确定优先级</para>
        /// </summary>
        public static bool HasPriority { get; set; } = false;

        /// <summary>
        /// 设置 解析时采用的配置文本
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="lambda"></param>
        /// <param name="dateTimeFormat">格式化日期时所采用的格式化字符串</param>
        /// <param name="hasPriority">在构造条件时是否确定优先级</param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> SetConfigurations<T>(this Expression<Func<T, bool>> lambda,
            string dateTimeFormat = null, bool hasPriority = false) where T : IEntity
        {
            if (!string.IsNullOrEmpty(dateTimeFormat))
            { DateTimeFormat = dateTimeFormat; }
            HasPriority = hasPriority;
            return lambda;
        }

        /// <summary>
        /// 将 Lambda 表达式转为 sql 条件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="lambda"></param>
        /// <exception cref="NotSupportedException"/>
        /// <returns>sql 条件表达式</returns>
        public static string ToSql<T>(this Expression<Func<T, bool>> lambda) where T : IEntity
        {
            string value = lambda.Body.ToSql();
            return value;
        }

        /// <summary>
        /// 不同类型的表达式 解析
        /// </summary>
        /// <param name="expression"></param>
        /// <exception cref="NotSupportedException"/>
        /// <returns></returns>
        private static string ToSql(this Expression expression)
        {
            if (expression is LambdaExpression lambda)
            {
                return lambda.ToSql();
            }
            else if (expression is BinaryExpression binary)
            {
                return binary.ToSql();
            }
            else if (expression is MemberExpression member)
            {
                return member.ToSql();
            }
            else if (expression is MethodCallExpression methodCall)
            {
                return methodCall.ToSql();
            }
            else if (expression is ConstantExpression constant)
            {
                return constant.ToSql();
            }
            else if (expression is UnaryExpression unary)
            {
                return unary.ToSql();
            }
            else
            {
                throw new NotSupportedException($"未支持的表达式 : {expression.GetType().Name}");
            }
        }

        /// <summary>
        /// Lambda 表达式 解析
        /// </summary>
        /// <param name="lambda"></param>
        /// <returns></returns>
        private static string ToSql(this LambdaExpression lambda)
        {
            string value = lambda.Body.ToSql();
            return value;
        }

        /// <summary>
        /// 二元运算符表达式 解析
        /// </summary>
        /// <param name="binary"></param>
        /// <returns></returns>
        private static string ToSql(this BinaryExpression binary)
        {
            string value;
            string left = binary.Left.ToSql();
            string @operator = ExpressHandler.ToSql(binary.NodeType);
            string right = binary.Right.ToSql();
            if (binary.Left.Type == typeof(bool) && 
                binary.Right.Type == typeof(bool) &&
                binary.NodeType == ExpressionType.Equal) // 解析 Boolean 类型和 Boolean 常量比较
            { left = left.Substring(0, left.Length - 1 - 2); }

            if (right == "NULL")
            {
                if (binary.NodeType == ExpressionType.Equal)
                { @operator = "is"; }
                else
                { @operator = "is not"; }
            }
            if (HasPriority)
            { value = $"({left} {@operator} {right})"; }
            else
            { value = $"{left} {@operator} {right}"; }
            return value;
        }

        /// <summary>
        /// 常量表达式转为 sql 
        /// </summary>
        /// <param name="constant"></param>
        /// <returns></returns>
        private static string ToSql(this ConstantExpression constant)
        {
            string value = ExpressHandler.ToSqlValue(constant.Value);
            return value;
        }

        /// <summary>
        /// 一元运算符表达式 处理
        /// </summary>
        /// <param name="unary"></param>
        /// <returns></returns>
        private static string ToSql(this UnaryExpression unary)
        {
            string value;
            if (unary.NodeType == ExpressionType.Not && unary.Operand is MethodCallExpression notMethodCall)
            {
                if (notMethodCall.NodeType == ExpressionType.Call)
                {
                    var prop = notMethodCall.Object.ToSql();
                    if (notMethodCall.Method == typeof(string).GetMethod("Contains", new Type[] { typeof(string) }))
                    { value = $"{prop} not like '%{notMethodCall.Arguments[0].ToSql().Replace("'", "")}%'".Replace("\"", ""); }
                    else if (notMethodCall.Method == typeof(string).GetMethod("StartsWith", new Type[] { typeof(string) }))
                    { value = $"{prop} not like '{notMethodCall.Arguments[0].ToSql().Replace("'", "")}%'".Replace("\"", ""); }
                    else if (notMethodCall.Method == typeof(string).GetMethod("EndsWith", new Type[] { typeof(string) }))
                    { value = $"{prop} not like '%{notMethodCall.Arguments[0].ToSql().Replace("'", "")}'".Replace("\"", ""); }
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
                    { value = $"`{notMember.Member.Name}`<>1"; }
                    else
                    { value = notMember.ToSql(); }
                }
                else
                { throw new NotSupportedException($"不支持字段 : {notMember.Member.Name}"); }
            }
            else
            { value = unary.Operand.ToSql(); }
            return value;
        }

        /// <summary>
        /// 方法调用表达式 解析
        /// </summary>
        /// <param name="methodCall"></param>
        /// <returns></returns>
        private static string ToSql(this MethodCallExpression methodCall)
        {
            string value;
            if (methodCall.Method.DeclaringType == typeof(string))
            {
                var prop = methodCall.Object.ToSql();
                if (methodCall.Method == typeof(string).GetMethod("Contains", new Type[] { typeof(string) }))
                { value = $"{prop} like '%{methodCall.Arguments[0].ToSql().Replace("'", "")}%'".Replace("\"", ""); }
                else if (methodCall.Method == typeof(string).GetMethod("StartsWith", new Type[] { typeof(string) }))
                { value = $"{prop} like '{methodCall.Arguments[0].ToSql().Replace("'", "")}%'".Replace("\"", ""); }
                else if (methodCall.Method == typeof(string).GetMethod("EndsWith", new Type[] { typeof(string) }))
                { value = $"{prop} like '%{methodCall.Arguments[0].ToSql().Replace("'", "")}'".Replace("\"", ""); }
                else { throw new NotSupportedException($"不支持的方法 : {methodCall.Method.Name}"); }
            }
            else if (methodCall.Method.DeclaringType == typeof(SqlMethods)) // In
            {
                var prop = methodCall.Arguments[0].ToSql(); // 字段名
                string _value;
                List<string> _list = new List<string>();
                if (methodCall.Arguments[1] is ListInitExpression listInit) // 解析 List<T> 变量
                {
                    foreach (var elementInit in listInit.Initializers)
                    {
                        foreach (var expression in elementInit.Arguments)
                        {
                            string _innerValue = expression.ToSql();
                            if (_innerValue != "NULL")
                            { _list.Add(_innerValue); }
                        }
                    }
                    _value = string.Join(",", _list);
                }
                else if (methodCall.Arguments[1] is MemberExpression member) // 解析 List<T> 字段
                {
                    _value = member.ToSql();
                }
                else
                { throw new NotSupportedException($"不支持的参数类型 : {methodCall.Arguments[1].GetType().Name}"); }
                if (methodCall.Method.Name == "In")
                { value = $"{prop} in ({_value})"; }
                else if (methodCall.Method.Name == "NotIn")
                { value = $"{prop} not in ({_value})"; }
                else
                { throw new NotSupportedException($"不支持的方法 : {methodCall.Method.Name}"); }
            }
            else
            { throw new NotSupportedException($"不支持的方法 : {methodCall.Method.Name}"); }
            return value;
        }

        /// <summary>
        /// 字段或属性表达式 解析
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        private static string ToSql(this MemberExpression member)
        {
            string value;
            if (member.Member is PropertyInfo property)
            {
                if (property.PropertyType == typeof(bool)) // Boolean 类型的属性默认加上 =1, 
                { value = $"`{member.Member.Name}`=1"; }
                else if (property.PropertyType == typeof(DateTime) && ExpressHandler.IsDateTimeDefaultProperty(member))
                { value = ExpressHandler.ToSqlValue(property.GetMethod.Invoke(property, null)); }
                else
                { value = $"`{member.Member.Name}`"; }
            }
            else if (member.Member is FieldInfo field) // 字段
            {
                UnaryExpression cast = Expression.Convert(member, typeof(object));
                var @object = Expression.Lambda<Func<object>>(cast).Compile().Invoke();
                var type = @object.GetType();
                if (type.IsGenericType && type.FullName.StartsWith("System.Collections.Generic.List")) // 解析 List<T> 字段
                {
                    int count = Convert.ToInt32(type.GetProperty("Count").GetValue(@object));
                    List<string> list = new List<string>();
                    for (int i = 0; i < count; i++)
                    {
                        string _val = ExpressHandler.ToSqlValue(type.GetProperty("Item").GetValue(@object, new object[] { i }));
                        list.Add(_val);
                    }
                    value = string.Join(",", list);
                }
                else
                { value = ExpressHandler.ToSqlValue(@object); }
            }
            else
            { throw new NotSupportedException($"不支持的情况 : {member.Member}"); }
            return value;
        }
    }

    /// <summary>
    /// 表示 Sql 中的一些方法
    /// </summary>
    public static class SqlMethods
    {
        /// <summary>
        /// 确定 <paramref name="value"/> 是否在 <paramref name="list"/> 中。
        /// <para>in 方法</para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">指定的值</param>
        /// <param name="list">集合</param>
        /// <returns></returns>
        public static bool In<T>(T value, List<T> list)
        {
            bool @in = false;
            list.ForEach(v =>
            {
                if (v.Equals(value))
                {
                    @in = true;
                    return;
                }
            });
            return @in;
        }

        /// <summary>
        /// 确定 <paramref name="value"/> 是否不在 <paramref name="list"/> 中。
        /// <para>in 方法</para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">指定的值</param>
        /// <param name="list">集合</param>
        /// <returns></returns>
        public static bool NotIn<T>(T value, List<T> list) => !In(value, list);
    }
}
