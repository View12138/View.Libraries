using System.Collections.Generic;

namespace View.AutoTask.Core
{
    internal static class HashSetExtension
    {

#if !NET472_OR_GREATER && !NETCOREAPP2_0_OR_GREATER && !NETSTANDARD2_1
        private static readonly object hashSetLocker = new object();
        public static bool TryGetValue<T>(this HashSet<T> hashset, T equalValue, out T actualValue)
        {
            lock (hashSetLocker)
            {
                if (hashset?.Contains(equalValue) ?? false)
                {
                    foreach (var item in hashset)
                    {
                        if (item.Equals(equalValue))
                        {
                            actualValue = item;
                            return true;
                        }
                    }
                }
                actualValue = default;
                return false;
            }
        }
#endif

    }
}
