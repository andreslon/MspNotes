﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavigationApp1.Presentation
{
    /// <summary>
    /// Represents an observable collection of navigation items.
    /// </summary>
    public class NavigationItemCollection
        : ObservableCollection<NavigationItem>
    {
        private NavigationItem parent;

        internal NavigationItemCollection(NavigationItem parent)
        {
            this.parent = parent;
        }

        /// <summary>
        /// Removes all items from the collection.
        /// </summary>
        protected override void ClearItems()
        {
            var items = this.Items.ToArray();
            base.ClearItems();

            // clear parent from items
            foreach (var item in items) {
                item.Parent = null;
            }
        }

        /// <summary>
        /// Inserts an item into the collection at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index at which item should be inserted.</param>
        /// <param name="item">The object to insert</param>
        protected override void InsertItem(int index, NavigationItem item)
        {
            VerifyNewItem(item);

            base.InsertItem(index, item);

            item.Parent = this.parent;
        }

        /// <summary>
        /// Removes the item at the specified index of the collection.
        /// </summary>
        /// <param name="index">The zero-based index of the element to remove.</param>
        protected override void RemoveItem(int index)
        {
            var oldItem = this.Items[index];

            base.RemoveItem(index);

            oldItem.Parent = null;
        }

        /// <summary>
        /// Replaces the element at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the element to replace.</param>
        /// <param name="item">The new value for the element at the specified index.</param>
        protected override void SetItem(int index, NavigationItem item)
        {
            VerifyNewItem(item);
            var oldItem = this.Items[index];

            base.SetItem(index, item);
            oldItem.Parent = null;
            item.Parent = this.parent;
        }

        private void VerifyNewItem(NavigationItem item)
        {
            if (item == null) {
                throw new ArgumentNullException(nameof(item));
            }

            if (item.Parent != null) {
                throw new InvalidOperationException("ItemAlreadyInCollection");
            }
        }
    }
}
