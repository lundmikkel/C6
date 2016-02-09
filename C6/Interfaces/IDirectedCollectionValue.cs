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
    /// <summary>
    /// Represents a sized generic collection, that can be enumerated forwards
    /// and backwards.
    /// </summary>
    /// <typeparam name="T">The type of the items in the collection.</typeparam>
    [ContractClass(typeof(IDirectedCollectionValueContract<>))]
    public interface IDirectedCollectionValue<T> : IDirectedEnumerable<T>, ICollectionValue<T>
    {
        /// <summary>
        /// Returns an <see cref="IDirectedCollectionValue{T}"/> that contains
        /// the same items as this <see cref="IDirectedCollectionValue{T}"/>,
        /// but whose enumerator will enumerate the items backwards
        /// (in opposite order).
        /// </summary>
        /// <returns>The <see cref="IDirectedCollectionValue{T}"/> whose
        /// enumerator will enumerate the items backwards.</returns>
        /// <remarks>The <see cref="IDirectedCollectionValue{T}"/> becomes
        /// invalid, if the original is modified. The method is typically used
        /// as in <c>foreach (var item in coll.Backwards()) {...}</c>.</remarks>
        [Pure]
        new IDirectedCollectionValue<T> Backwards();
    }



    [ContractClassFor(typeof(IDirectedCollectionValue<>))]
    internal abstract class IDirectedCollectionValueContract<T> : IDirectedCollectionValue<T>
    {
        // ReSharper disable InvocationIsSkipped

        IDirectedCollectionValue<T> IDirectedCollectionValue<T>.Backwards()
        {
            // No Requires


            // Result is non-null
            Contract.Ensures(Contract.Result<IDirectedEnumerable<T>>() != null);

            // Result enumeration is backwards
            Contract.Ensures(this.Reverse().SequenceEqual(Contract.Result<IDirectedEnumerable<T>>())); // TODO: Use specific comparer?

            // Result direction is opposite
            Contract.Ensures(Contract.Result<IDirectedEnumerable<T>>().Direction != Direction);


            throw new NotImplementedException();
        }


        // ReSharper restore InvocationIsSkipped


        #region Non-Contract Methods

        public abstract EnumerationDirection Direction { get; }
        public abstract SCG.IEnumerator<T> GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public abstract IDirectedEnumerable<T> Backwards();
        public abstract string ToString(string format, IFormatProvider formatProvider);
        public abstract bool Show(StringBuilder stringbuilder, ref int rest, IFormatProvider formatProvider);
        public abstract EventTypes ListenableEvents { get; }
        public abstract EventTypes ActiveEvents { get; }
        public abstract event EventHandler CollectionChanged;
        public abstract event EventHandler<ClearedEventArgs> CollectionCleared;
        public abstract event EventHandler<ItemCountEventArgs<T>> ItemsAdded;
        public abstract event EventHandler<ItemCountEventArgs<T>> ItemsRemoved;
        public abstract event EventHandler<ItemAtEventArgs<T>> ItemInserted;
        public abstract event EventHandler<ItemAtEventArgs<T>> ItemRemovedAt;
        public abstract bool IsEmpty { get; }
        public abstract int Count { get; }
        public abstract Speed CountSpeed { get; }
        public abstract T Choose();
        public abstract void CopyTo(T[] array, int arrayIndex);
        public abstract T[] ToArray();

        #endregion
    }
}