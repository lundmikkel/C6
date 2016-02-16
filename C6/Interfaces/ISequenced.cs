﻿// This file is part of the C6 Generic Collection Library for C# and CLI
// See https://github.com/lundmikkel/C6/blob/master/LICENSE.md for licensing details.


using System;
using System.Collections;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

using SCG = System.Collections.Generic;


namespace C6
{
    // TODO: Rewrite documentation based on hash code solution.
    /// <summary>
    /// Represents an editable collection maintaining a definite sequence order
    /// of its items.
    /// </summary>
    /// <remarks>
    /// Implementations of this interface must compute the hash code and 
    /// equality exactly as prescribed in the method definitions in order to be
    /// consistent with other collection classes implementing this interface.
    /// </remarks>
    [ContractClass(typeof(ISequencedContract<>))]
    public interface ISequenced<T> : ICollection<T>, IDirectedCollectionValue<T>
    {
        // TODO: Consider how this should be implemented/documented. Maybe use a static helper class.
        /// <summary>
        /// Returns the sequenced (order-sensitive) hash code of the collection.
        /// </summary>
        /// <returns>The sequenced hash code of the collection.</returns>
        /// <remarks>
        /// The hash code is defined as <c>h(...h(h(h(x1),x2),x3),...,xn)</c>
        /// for <c>h(a,b)=CONSTANT*a+b</c> and the x's the hash codes of the
        /// items of this collection.
        /// </remarks>
        [Pure]
        int GetSequencedHashCode();


        /// <summary>
        /// Compares the items in this collection to the items in the other 
        /// collection with regards to multiplicities and sequence order.
        /// </summary>
        /// <param name="otherCollection">The collection to compare this
        /// collection to.</param>
        /// <returns><c>true</c> if the collections contain equal items in the
        /// same order; otherwise, <c>false</c>.</returns>
        /// <remarks>
        /// <para>
        /// Enumeration of the collections must yield equal items, place for
        /// place. The comparison uses <b>this</b> collection's
        /// <see cref="IExtensible{T}.EqualityComparer"/> to determine item
        /// equality. If the two collections use different notions of item
        /// equality, there is no guarantee that this method is symmetric, i.e.
        /// the following test is undetermined:
        /// <code>
        /// // Undetermined when coll1.EqualityComparer and coll2.EqualityComparer are not equal
        /// coll1.SequencedEquals(coll2) == coll2.SequencedEquals(coll1)
        /// </code>
        /// </para>
        /// <para>This method is equivalent to
        /// <c>Enumerable.SequenceEqual(coll1, coll2, coll1.EqualityComparer)</c>,
        /// but might be more efficient.</para>
        /// </remarks>
        /// <seealso cref="GetSequencedHashCode"/>
        /// <seealso cref="Enumerable.SequenceEqual{T}(SCG.IEnumerable{T}, SCG.IEnumerable{T}, SCG.IEqualityComparer{T})"/>
        [Pure]
        bool SequencedEquals(ISequenced<T> otherCollection);
    }



    [ContractClassFor(typeof(ISequenced<>))]
    internal abstract class ISequencedContract<T> : ISequenced<T>
    {
        // ReSharper disable InvocationIsSkipped

        public int GetSequencedHashCode()
        {
            // TODO: Use static helper class to define result?


            throw new NotImplementedException();
        }


        public bool SequencedEquals(ISequenced<T> otherCollection)
        {
            // No Requires


            // Enumeration of the collections must yield equal items
            Contract.Ensures(Contract.Result<bool>() == this.SequenceEqual(otherCollection ?? Enumerable.Empty<T>(), EqualityComparer));


            throw new NotImplementedException();
        }


        // ReSharper restore InvocationIsSkipped


        #region Non-Contract Methods

        public abstract bool Add(T item);
        public abstract void Clear();
        public abstract bool Contains(T item);
        public abstract void CopyTo(T[] array, int arrayIndex);
        public abstract bool Remove(T item);
        public abstract bool Remove(T item, out T removedItem);
        public abstract bool RemoveAll(T item);
        public abstract void RemoveAll(SCG.IEnumerable<T> items);
        public abstract void RetainAll(SCG.IEnumerable<T> items);
        public abstract ICollectionValue<T> UniqueItems();
        public abstract bool UnsequencedEquals(ICollection<T> otherCollection);
        public abstract bool Update(T item);
        public abstract bool Update(T item, out T oldItem);
        public abstract bool UpdateOrAdd(T item);
        public abstract bool UpdateOrAdd(T item, out T oldItem);
        public abstract int Count { get; }
        public abstract bool Find(ref T item);
        public abstract bool FindOrAdd(ref T item);
        public abstract int GetUnsequencedHashCode();
        public abstract bool IsReadOnly { get; }
        public abstract bool ContainsAll(SCG.IEnumerable<T> items);
        public abstract int ContainsCount(T item);
        public abstract Speed ContainsSpeed { get; }
        public abstract void AddAll(SCG.IEnumerable<T> items);
        public abstract bool AllowsDuplicates { get; }
        void ICollectionValue<T>.CopyTo(T[] array, int arrayIndex) { throw new NotImplementedException(); }
        void SCG.ICollection<T>.CopyTo(T[] array, int arrayIndex) { throw new NotImplementedException(); }
        int SCG.ICollection<T>.Count { get { throw new NotImplementedException(); } }
        public abstract bool DuplicatesByCounting { get; }
        public abstract SCG.IEqualityComparer<T> EqualityComparer { get; }
        bool IExtensible<T>.IsReadOnly { get { throw new NotImplementedException(); } }
        bool SCG.ICollection<T>.IsReadOnly { get { throw new NotImplementedException(); } }
        public abstract ICollectionValue<KeyValuePair<T, int>> ItemMultiplicities();
        bool SCG.ICollection<T>.Remove(T item) { throw new NotImplementedException(); }
        public abstract SCG.IEnumerator<T> GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public abstract string ToString(string format, IFormatProvider formatProvider);
        public abstract bool Show(StringBuilder stringBuilder, ref int rest, IFormatProvider formatProvider);
        public abstract EventTypes ListenableEvents { get; }
        public abstract EventTypes ActiveEvents { get; }
        public abstract event EventHandler CollectionChanged;
        public abstract event EventHandler<ClearedEventArgs> CollectionCleared;
        public abstract event EventHandler<ItemCountEventArgs<T>> ItemsAdded;
        public abstract event EventHandler<ItemCountEventArgs<T>> ItemsRemoved;
        public abstract event EventHandler<ItemAtEventArgs<T>> ItemInserted;
        public abstract event EventHandler<ItemAtEventArgs<T>> ItemRemovedAt;
        void SCG.ICollection<T>.Add(T item) { throw new NotImplementedException(); }
        bool IExtensible<T>.Add(T item) { throw new NotImplementedException(); }
        void SCG.ICollection<T>.Clear() { throw new NotImplementedException(); }
        public abstract T Choose();
        bool SCG.ICollection<T>.Contains(T item) { throw new NotImplementedException(); }
        int ICollectionValue<T>.Count { get { throw new NotImplementedException(); } }
        public abstract Speed CountSpeed { get; }
        public abstract bool IsEmpty { get; }
        public abstract T[] ToArray();
        IDirectedEnumerable<T> IDirectedEnumerable<T>.Backwards() { throw new NotImplementedException(); }
        IDirectedCollectionValue<T> IDirectedCollectionValue<T>.Backwards() { throw new NotImplementedException(); }
        public abstract EnumerationDirection Direction { get; }

        #endregion
    }
}