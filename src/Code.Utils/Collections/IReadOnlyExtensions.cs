using System;
using System.Collections.Generic;

namespace Code.Utils.Collections
{
    public static class IReadOnlyExtensions
    {
        public static int FindIndex<T>(this IReadOnlyList<T> list, Func<T, bool> predicate, int start = 0)
        {
            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            for (int i = start, n = list.Count; i < n; i++)
            {
                if (predicate.Invoke(list[i]))
                {
                    return i;
                }
            }

            return -1;
        }

        public static int FindIndex<T>(this IReadOnlyList<T> list, Func<T, int, bool> predicate, int start = 0)
        {
            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            for (int i = start, n = list.Count; i < n; i++)
            {
                if (predicate.Invoke(list[i], i))
                {
                    return i;
                }
            }

            return -1;
        }
    }
}