using System;
using System.Collections.Generic;

using Fornax.Net.Util.Collections.Generic;


namespace Fornax.Net.Util.Linq
{
    public static partial class XQueryable
    {
        private static class List { }

        public static void AddRange<T>(this IList<T> list, IEnumerable<T> values) {
            var lt = list as List<T>;

            if (lt != null)
                lt.AddRange(values);
            else {
                foreach (var item in values) {
                    list.Add(item);
                }
            }
        }

        /// <summary>
        /// Creates A SubsList form this <code><see cref="IList{T}"/> <paramref name="list"/>.</code>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">The list.</param>
        /// <param name="fromIndex">From index.</param>
        /// <param name="toIndex">To index.</param>
        /// <returns></returns>
        public static IList<T> SubList<T>(this IList<T> list, int fromIndex, int toIndex) {
            return new SubList<T>(list, fromIndex, toIndex);
        }

        /// <summary>
        /// Swaps the position of two elements A @<paramref name="indexA"/> and B @<paramref name="indexB"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">The list.</param>
        /// <param name="indexA">The index a.</param>
        /// <param name="indexB">The index b.</param>
        public static void Swap<T>(this IList<T> list, int indexA, int indexB) {
            T tmp = list[indexA];
            list[indexA] = list[indexB];
            list[indexB] = tmp;
        }

        /// <summary>
        /// If the underlying type is <see cref="List{T}"/>,
        /// calls <see cref="List{T}.Sort()"/>. If not, 
        /// uses <see cref="Util.CollectionUtil.TimSort{T}(IList{T})"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">this <see cref="IList{T}"/></param>
        public static void Sort<T>(this IList<T> list) {
            if (list is List<T>) {
                ((List<T>)list).Sort();
            } else {
              //  Util.CollectionUtil.TimSort(list);
            }
        }

        /// <summary>
        /// If the underlying type is <see cref="List{T}"/>,
        /// calls <see cref="List{T}.Sort(IComparer{T})"/>. If not, 
        /// uses <see cref="Util.CollectionUtil.TimSort{T}(IList{T}, IComparer{T})"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">this <see cref="IList{T}"/></param>
        /// <param name="comparer">the comparer to use for the sort</param>
        public static void Sort<T>(this IList<T> list, IComparer<T> comparer) {
            if (list is List<T>) {
                ((List<T>)list).Sort(comparer);
            } else {
             //   Util.CollectionUtil.TimSort(list, comparer);
            }
        }

        /// <summary>
        /// If the underlying type is <see cref="List{T}"/>,
        /// calls <see cref="List{T}.Sort(IComparer{T})"/>. If not, 
        /// uses <see cref="Util.CollectionUtil.TimSort{T}(IList{T}, IComparer{T})"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">this <see cref="IList{T}"/></param>
        /// <param name="comparison">the comparison function to use for the sort</param>
        public static void Sort<T>(this IList<T> list, Comparison<T> comparison) {
            IComparer<T> comparer = new FunctorComparer<T>(comparison);
            Sort(list, comparer);
        }

        /// <summary>
        /// Sorts the given <see cref="IList{T}"/> using the <see cref="IComparer{T}"/>.
        /// This method uses the Tim sort
        /// algorithm, but falls back to binary sort for small lists.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">this <see cref="IList{T}"/></param>
        public static void TimSort<T>(this IList<T> list) {
           // Util.CollectionUtil.TimSort(list);
        }

        /// <summary>
        /// Sorts the given <see cref="IList{T}"/> using the <see cref="IComparer{T}"/>.
        /// This method uses the Tim sort
        /// algorithm, but falls back to binary sort for small lists.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">this <see cref="IList{T}"/></param>
        /// <param name="comparer">The <see cref="IComparer{T}"/> to use for the sort.</param>
        public static void TimSort<T>(this IList<T> list, IComparer<T> comparer) {
         ///   Util.CollectionUtil.TimSort(list, comparer);
        }

        /// <summary>
        /// Sorts the given <see cref="IList{T}"/> using the <see cref="IComparer{T}"/>.
        /// This method uses the intro sort
        /// algorithm, but falls back to insertion sort for small lists. 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">this <see cref="IList{T}"/></param>
        public static void IntroSort<T>(this IList<T> list) {
           // Util.CollectionUtil.IntroSort(list);
        }

        /// <summary>
        /// Sorts the given <see cref="IList{T}"/> using the <see cref="IComparer{T}"/>.
        /// This method uses the intro sort
        /// algorithm, but falls back to insertion sort for small lists. 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">this <see cref="IList{T}"/></param>
        /// <param name="comparer">The <see cref="IComparer{T}"/> to use for the sort.</param>
        public static void IntroSort<T>(this IList<T> list, IComparer<T> comparer) {
           // Util.CollectionUtil.IntroSort(list, comparer);
        }

    }
}