using System;
using System.Collections;
using System.Collections.Generic;

namespace Fornax.Net.Util.Collections.Generic
{
    /// <summary>
    /// A Sub-List that is contained in a <see cref="IList{T}"/> .
    /// The Container of the Sublist is used to create an Instance of a Sublist.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="System.Collections.Generic.IList{T}" />
    public sealed class SubList<T> : IList<T>, ISubList<T>
    {
        private readonly IList<T> list;
        private readonly int fromIndex;
        private int toIndex;

        /// <summary>
        /// Creates a ranged view of the given <paramref name="list"/>.
        /// </summary>
        /// <param name="list">The original list to view.</param>
        /// <param name="fromIndex">The inclusive starting index.</param>
        /// <param name="toIndex">The exclusive ending index.</param>
        public SubList(IList<T> list, int fromIndex, int toIndex) {
            if (fromIndex < 0)
                throw new ArgumentOutOfRangeException("fromIndex");

            if (toIndex > list.Count)
                throw new ArgumentOutOfRangeException("toIndex");

            if (toIndex < fromIndex)
                throw new ArgumentOutOfRangeException("toIndex");

            if (list == null || list.Count == 0)
                throw new ArgumentNullException("list");

            this.list = list;
            this.fromIndex = fromIndex;
            this.toIndex = toIndex;
        }
        /// <summary>
        /// Determines the index of a specific item in the <see cref="SubList{T}`1" />.
        /// </summary>
        /// <param name="item">The object to locate in the <see cref="SubList{T}`1" />.</param>
        /// <returns>
        /// The index of <paramref name="item" /> if found in the list; otherwise, -1.
        /// </returns>
        public int IndexOf(T item) {
            for (int i = fromIndex, fakeIndex = 0; i < toIndex; i++, fakeIndex++) {
                var current = list[i];

                if (current == null && item == null)
                    return fakeIndex;

                if (current.Equals(item)) {
                    return fakeIndex;
                }
            }

            return -1;
        }

        /// <summary>
        /// Inserts an item to the <see cref="SubList{T}`1" /> at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index at which <paramref name="item" /> should be inserted.</param>
        /// <param name="item">The object to insert into the <see cref = "SubList{T}`1" />.</param>
        /// <exception cref="NotSupportedException"></exception>
        public void Insert(int index, T item) {
            list.Insert(fromIndex + index, item);
        }

        /// <summary>
        /// Removes the <see cref="SubList{T}`1" /> item at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the item to remove.</param>
        public void RemoveAt(int index) {
            list.RemoveAt(fromIndex + index);
            toIndex--;
        }

        /// <summary>
        /// Gets or sets the <see cref="T"/> at the specified index.
        /// </summary>
        /// <value>
        /// The <see cref="T"/>.
        /// </value>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        /// <exception cref="IndexOutOfRangeException">
        /// </exception>
        public T this[int index] {
            get {
                if ((index >= 0) && (index < list.Count))
                    return list[fromIndex + index];
                else throw new IndexOutOfRangeException();
            }
            set {
                if ((index >= 0) && (index < list.Count))
                    list[fromIndex + index] = value;
                else throw new IndexOutOfRangeException();
            }
        }

        /// <summary>
        /// Adds an item to the <see cref="SubList{T}`1" />.
        /// </summary>
        /// <param name="item">The object to add to the <see cref="SubList{T}`1" />.</param>
        public void Add(T item) {
            list.Insert(toIndex-1, item);
        }

        /// <summary>
        /// Removes all items from the <see cref="SubList{T}`1" />.
        /// </summary>
        public void Clear() {
            for (int i = toIndex - 1; i >= fromIndex; i--) {
                list.RemoveAt(i);
            }
            toIndex = fromIndex; 
        }

        /// <summary>
        /// Determines whether the <see cref="SubList{T}`1" /> contains a specific value.
        /// </summary>
        /// <param name="item">The object to locate in the <see cref="SubList{T}`1" />.</param>
        /// <returns>
        ///   <see langword="true" /> if <paramref name="item" /> is found in the <see cref="SubList{T}`1" />; otherwise, <see langword="false" />.
        /// </returns>
        public bool Contains(T item) {
            return IndexOf(item) >= 0;
        }

        /// <summary>
        /// Copies the elements of the <see cref="SubList{T}`1" /> to an <see cref="T:System.Array" />, starting at a particular <see cref="T:System.Array" /> index.
        /// </summary>
        /// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="SubList{T}`1" />. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
        /// <param name="arrayIndex">The zero-based index in <paramref name="array" /> at which copying begins.</param>
        public void CopyTo(T[] array, int arrayIndex) {
            int count = array.Length - arrayIndex;

            for (int i = fromIndex, arrayi = arrayIndex; i <= Math.Min(toIndex - 1, fromIndex + count - 1); i++, arrayi++) {
                array[arrayi] = list[i];
            }
        }

        /// <summary>
        /// Gets the number of elements contained in the <see cref="SubList{T}" />.
        /// </summary>
        public int Count {
            get { return Math.Max(toIndex - fromIndex, 0); }
        }

        /// <summary>
        /// Gets a value indicating whether the <see cref="SubList{T}" /> is read-only.
        /// </summary>
        public bool IsReadOnly {
            get { return list.IsReadOnly; }
        }
        /// <summary>
        /// Gets the container, which is the Current state of the <see cref="IList{T}"/> which holds the sublist.
        /// </summary>
        /// <value>
        /// The container list.
        /// </value>
        public IList<T> Container => this.list;

        /// <summary>
        /// Removes the first occurrence of a specific object from the <see cref="SubList{T}`1" />.
        /// </summary>
        /// <param name="item">The object to remove from the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
        /// <returns>
        ///   <see langword="true" /> if <paramref name="item" /> was successfully removed from the <see cref="SubList{T}`1" />; otherwise, <see langword="false" />. This method also returns <see langword="false" /> if <paramref name="item" /> is not found in the original <see cref="T:System.Collections.Generic.ICollection`1" />.
        /// </returns>
        public bool Remove(T item) {
            var index = IndexOf(item);

            if (index < 0)
                return false;

            list.RemoveAt(fromIndex + index);
            toIndex--;

            return true;
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// An enumerator that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<T> GetEnumerator() {
            return YieldItems().GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }

        private IEnumerable<T> YieldItems() {
            for (int i = fromIndex; i <= Math.Min(toIndex - 1, list.Count - 1); i++) {
                yield return list[i];
            }
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj) {
            var list = obj as SubList<T>;
            return list != null &&
                   EqualityComparer<IList<T>>.Default.Equals(this.list, list.list) &&
                   Count == list.Count &&
                   EqualityComparer<IList<T>>.Default.Equals(Container, list.Container);
        }

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="list1">The list1.</param>
        /// <param name="list2">The list2.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator ==(SubList<T> list1, SubList<T> list2) {
            return EqualityComparer<SubList<T>>.Default.Equals(list1, list2);
        }

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="list1">The list1.</param>
        /// <param name="list2">The list2.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator !=(SubList<T> list1, SubList<T> list2) {
            return !(list1 == list2);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode() {
            IList<T> list = new List<T>();

            return list.GetHashCode();
                }
    }
}
