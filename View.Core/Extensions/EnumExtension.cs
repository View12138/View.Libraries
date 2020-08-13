using System;
using System.Collections.Generic;
using System.Text;

namespace View.Core.Extensions
{
    /// <summary>
    /// 枚举扩展
    /// </summary>
    public static class EnumExtension
    {
        /// <summary>
        /// 移除位域枚举中指定的值
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="_enum"></param>
        /// <param name="enum"></param>
        /// <returns></returns>
        public static TEnum Remove<TEnum>(this TEnum _enum, TEnum @enum) where TEnum : struct
        {
            if (!typeof(TEnum).IsEnum) { throw new ArgumentException("TEnum must be an enumerated type."); }

            int _value = Convert.ToInt32(_enum);
            int value = Convert.ToInt32(@enum);

            return (TEnum)Enum.ToObject(typeof(TEnum), _value & (~value));

        }
    }
}
