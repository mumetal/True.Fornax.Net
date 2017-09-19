using System.Collections.Generic;
using System.Diagnostics;

namespace Fornax.Net.Util.Linq
{
    public static partial class XQueryable
    {
        internal static class Set { }

            [DebuggerStepThrough]
            public static void RemoveAll<T>(this ICollection<T> theSet, IEnumerable<T> removeList) {
                foreach (var elt in removeList) {
                    theSet.Remove(elt);
                }
            }

            [DebuggerStepThrough]
            public static void AddAll<T>(this ICollection<T> set, IEnumerable<T> itemsToAdd) {
                foreach (var item in itemsToAdd) {
                    set.Add(item);
                }
            }
    }
}
