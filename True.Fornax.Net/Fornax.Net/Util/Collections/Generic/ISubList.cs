using System.Collections.Generic;

namespace Fornax.Net.Util.Collections.Generic
{
    public interface ISubList<T>
    {
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
        T this[int index] { get; set; }

        /// <summary>
        /// Gets the container, which is the Current state of the <see cref="IList{T}"/> which holds the sublist.
        /// </summary>
        /// <value>
        /// The container list.
        /// </value>
        IList<T> Container { get; }

        /// <summary>
        /// Gets the number of elements contained in the <see cref="SubList{T}" />.
        /// </summary>
        int Count { get; }

        /// <summary>
        /// Gets a value indicating whether the <see cref="SubList{T}" /> is read-only.
        /// </summary>
        bool IsReadOnly { get; }

        /// <summary>
        /// Adds an item to the <see cref="SubList{T}`1" />.
        /// </summary>
        /// <param name="item">The object to add to the <see cref="SubList{T}`1" />.</param>
        void Add(T item);

        /// <summary>
        /// Removes all items from the <see cref="SubList{T}`1" />.
        /// </summary>
        void Clear();

        /// <summary>
        /// Determines whether the <see cref="SubList{T}`1" /> contains a specific value.
        /// </summary>
        /// <param name="item">The object to locate in the <see cref="SubList{T}`1" />.</param>
        /// <returns>
        /// <see langword="true" /> if <paramref name="item" /> is found in the <see cref="SubList{T}`1" />; otherwise, <see langword="false" />.
        /// </returns>
        bool Contains(T item);

        /// <summary>
        /// Copies the elements of the <see cref="SubList{T}`1" /> to an <see cref="T:System.Array" />, starting at a particular <see cref="T:System.Array" /> index.
        /// </summary>
        /// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="SubList{T}`1" />. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
        /// <param name="arrayIndex">The zero-based index in <paramref name="array" /> at which copying begins.</param>
        void CopyTo(T[] array, int arrayIndex);

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        bool Equals(object obj);

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// An enumerator that can be used to iterate through the collection.
        /// </returns>
        IEnumerator<T> GetEnumerator();

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        int GetHashCode();


        int IndexOf(T item);

        void Insert(int index, T item);

        bool Remove(T item);

        void RemoveAt(int index);
    }
}