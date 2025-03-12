using System.Collections.Generic;
using System.Linq;

namespace TestSlots.Utils
{
    public static class CollectionUtils
    {
        public static T RandomElement<T>(this IList<T> list, bool remove = false)
        {
            if (list.Count == 0)
                return default(T);

            var index = UnityEngine.Random.Range(0, list.Count);
            var result = list[index];

            if (remove)
                list.RemoveAt(index);

            return result;
        }

        public static T RandomElement<T>(this IEnumerable<T> enumerable)
        {
            if (!enumerable.Any())
                return default(T);

            var index = UnityEngine.Random.Range(0, enumerable.Count());
            var result = enumerable.ElementAtOrDefault(index);

            return result;
        }
    }
}
