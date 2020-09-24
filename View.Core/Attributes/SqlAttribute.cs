using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace View.Core.Attributes
{
    /// <summary>
    /// 指定实体字段的 NOT NULL 约束。
    /// <para>不为空</para>
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class NotNullAttribute : Attribute { }

    /// <summary>
    /// 指定实体字段的 UNIQUE 约束。
    /// <para>唯一</para>
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class UniqueAttribute : Attribute { }

    /// <summary>
    /// 指定实体字段的 AUTOINCREMENT 约束。
    /// <para>自增</para>
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class AutoIncrementAttribute : Attribute { }

    ///// <summary>
    ///// 指定实体字段的 FOREIGN KEY 约束。
    ///// </summary>
    //[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    //public class ForeignKeyAttribute : Attribute { }

    ///// <summary>
    ///// 指定实体字段的 CHECK 约束。
    /////// </summary>
    //[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    //public class CheckAttribute : Attribute
    //{
    //    /// <summary>
    //    /// 指定实体字段的约束表达式
    //    /// </summary>
    //    /// <param name="predicate"></param>
    //    public CheckAttribute(Expression<Func<object, bool>> predicate)
    //    {
    //        Predicate = predicate;
    //    }

    //    /// <summary>
    //    /// 获取约束条件表达式
    //    /// </summary>
    //    public Expression<Func<object, bool>> Predicate { get; }
    //}

    /// <summary>
    /// 指定实体字段的 DEFAULT 约束。
    /// <para>默认值</para>
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class DefaultAttribute : Attribute
    {
        /// <summary>
        /// 指定字段的默认值
        /// </summary>
        /// <param name="value"></param>
        public DefaultAttribute(object value)
        {
            Value = value;
        }

        /// <summary>
        /// 获取默认值
        /// </summary>
        public object Value { get; }
    }

    /// <summary>
    /// 指定实体字段的长度。
    /// <para>字段大小</para>
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class LengthAttribute : Attribute
    {
        readonly int _size = 0;

        /// <summary>
        /// 指定实体类型的字段的长度
        /// </summary>
        /// <param name="size"></param>
        public LengthAttribute(int size)
        {
            this._size = size;
        }

        /// <summary>
        /// 获取指定的长度
        /// </summary>
        public int Size
        {
            get { return _size; }
        }
    }
}
