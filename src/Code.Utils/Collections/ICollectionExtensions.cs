using System;
using System.Collections.Generic;
using System.Linq;

namespace Code.Utils.Collections
{
    public static class ICollectionExtensions
    {
        public static int FindIndex<T>(this ICollection<T> list, Func<T, bool> predicate, int start = 0)
        {
            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            for (int i = start, n = list.Count; i < n; i++)
            {
                // LC: As per https://referencesource.microsoft.com/#System.Core/System/Linq/Enumerable.cs,7db56d44563d8761 ElementAt does an conversion to IList<T> first, otherwise loops
                if (predicate.Invoke(list.ElementAt(i)))
                {
                    return i;
                }
            }

            return -1;
        }

        public static int FindIndex<T>(this ICollection<T> list, Func<T, int, bool> predicate, int start = 0)
        {
            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            for (int i = start, n = list.Count; i < n; i++)
            {
                if (predicate.Invoke(list.ElementAt(i), i))
                {
                    return i;
                }
            }

            return -1;
        }
    }
}