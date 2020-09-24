﻿using System;
using System.ComponentModel;
using System.Reflection;

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
        /// <param name="enum"></param>
        /// <param name="_enum"></param>
        /// <returns></returns>
        public static TEnum Remove<TEnum>(this Enum @enum, TEnum _enum) where TEnum : Enum
        {
            if (!typeof(TEnum).IsEnum) { throw new ArgumentException("TEnum must be an enumerated type."); }

            int value = Convert.ToInt32(@enum);
            int _value = Convert.ToInt32(_enum);
            
            return (TEnum)Enum.ToObject(typeof(TEnum), value & (~_value));

        }

        /// <summary>
        /// 获取枚举类型的描述。
        /// </summary>
        /// <param name="enum"></param>
        /// <returns>枚举描述信息</returns>
        public static string ToDescription(this Enum @enum)
        {
            Type type = @enum.GetType();
            MemberInfo[] memInfo = type.GetMember(@enum.ToString());
            if (null != memInfo && memInfo.Length > 0)
            {
                object[] attrs = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (null != attrs && attrs.Length > 0)
                { return ((DescriptionAttribute)attrs[0]).Description; }
            }
            return string.Empty;
        }
    }
}
