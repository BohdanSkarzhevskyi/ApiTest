using System.Collections.Generic;
using System.Linq;

namespace ApiTest.Utils.Extensions
{
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Check if List (Enumerable) is null or empty
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> enumerable)
        {
            if (enumerable == null)
            {
                return true;
            }

            if (enumerable is ICollection<T> collection)
            {
                return collection.Count < 1;
            }
            return !enumerable.Any();
        }
    }
}
