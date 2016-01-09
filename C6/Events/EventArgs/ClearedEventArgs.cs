﻿// This file is part of the C6 Generic Collection Library for C# and CLI
// See https://github.com/lundmikkel/C6/blob/master/LICENSE.md for licensing details.

using System;
using System.Diagnostics;
using System.Diagnostics.Contracts;

namespace C6
{
    /// <summary>
    /// Provides data for the
    /// <see cref="ICollectionValue{T}.CollectionCleared"/> event.
    /// </summary>
    [Serializable]
    [DebuggerDisplay("(ClearedEventArgs {Count} {Full})")] // TODO: format appropriately
    public class ClearedEventArgs : EventArgs
    {
        /// <summary>
        /// Gets a value indicating whether a collection was cleared, or
        /// whether a list view or an index range was cleared.
        /// </summary>
        /// <value><c>true</c> if the operation cleared a collection;
        /// <c>false</c> if the operation cleared a list view or an index range
        /// (even if the view or range is the entire collection).</value>
        public bool Full { get; }

        /// <summary>
        /// Gets the number of items cleared by the operation.
        /// </summary>
        /// <value>The number of items cleared by the operation.</value>
        [Pure]
        public int Count { get; }

        /// <summary>
        /// Gets the position (when known) of the first item if a list view or
        /// an index range was cleared.
        /// </summary>
        /// <value>The index of the first item cleared, when known;
        /// otherwise, <c>null</c>.</value>
        [Pure]
        public int? Start { get; }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            // Count is positive
            Contract.Invariant(Count > 0);

            // Start is only set, if a list view or index range was cleared
            Contract.Invariant(!Start.HasValue || !Full);
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="ClearedEventArgs"/>
        /// class for an operation that cleared a collection or a list
        /// view/index range using an item count and an optional start position.
        /// </summary>
        /// <param name="full">
        /// <c>true</c> if the operation cleared a collection;
        /// <c>false</c> if the operation cleared a list view or an index range
        /// (even if the view or range is the entire collection).</param>
        /// <param name="count">The number of items removed by the operation.</param>
        /// <param name="start">The position of the first item in the range
        /// deleted, when known.</param>
        public ClearedEventArgs(bool full, int count, int? start = null)
        {
            // Argument must be positive
            Contract.Requires(count > 0);

            // Start is only set, if a list view or index range was cleared
            Contract.Requires(!start.HasValue || !full);


            Full = full;
            Count = count;
            Start = start;

            
            Contract.Assume(Count > 0); // Static checker shortcoming
            Contract.Assume(!Start.HasValue || !Full); // Static checker shortcoming
        }
    }
}