using System;
using System.Collections.Generic;
using System.Text;

namespace View.Core.Extensions
{
    /// <summary>
    /// List 扩展类
    /// </summary>
    public static class ListExtension
    {
        /// <summary>
        /// 递归查找整个列表中满足条件的项目
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="match"></param>
        /// <returns></returns>
        public static T FindRecursion<T>(this List<T> list, Predicate<T> match)
        {
            foreach (var item in list)
            {
                if (match.Invoke(item))
                { return item; }
                foreach (var propertyInfo in item.GetType().GetProperties())
                {
                    if (propertyInfo.PropertyType == typeof(List<T>))
                    {
                        var child = (List<T>)propertyInfo.GetValue(item);
                        if (child.Count > 0)
                        {
                            return FindRecursion(child, match);
                        }
                    }
                }
            }
            return default;
        }
    }
}
