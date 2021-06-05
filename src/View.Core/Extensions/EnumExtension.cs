using System;
using System.ComponentModel;
using System.Reflection;

namespace View.Core.Extensions
{
    /// <summary>
    /// <see cref="Enum"/> 扩展。
    /// </summary>
    public static class EnumExtension
    {
        /// <summary>
        /// 移除位域枚举中指定的值
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="flag"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static TEnum Remove<TEnum>(this Enum flag, TEnum value) where TEnum : Enum
        {
            int _flag = Convert.ToInt32(flag);
            int _value = Convert.ToInt32(value);

            return (TEnum)Enum.ToObject(typeof(TEnum), _flag & (~_value));

        }

        /// <summary>
        /// 获取枚举类型的描述。
        /// </summary>
        /// <param name="value"></param>
        /// <returns>枚举描述信息</returns>
        public static string ToDescription(this Enum value)
        {
            Type type = value.GetType();
            FieldInfo field = type.GetField(Enum.GetName(type, value));
            DescriptionAttribute description = field.GetCustomAttribute<DescriptionAttribute>(true);
            return description?.Description ?? "";
        }
    }
}
