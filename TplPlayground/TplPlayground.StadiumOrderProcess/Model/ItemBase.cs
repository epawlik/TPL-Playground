using System;
using System.Collections.Generic;

namespace TplPlayground.StadiumOrderProcess.Model
{
    /// <summary>
    /// Base class for an item in the refreshment ordering system.
    /// </summary>
    public abstract class ItemBase
    {
        private static readonly Dictionary<int, Dictionary<Type, int>> _references = new Dictionary<int, Dictionary<Type, int>>();

        /// <summary>
        /// Gets the name of the item.
        /// </summary>
        public string Name
        {
            get;
        }

        /// <summary>
        /// Gets the order that the item is a part of.
        /// </summary>
        public Order Owner
        {
            get;
        }

        /// <summary>
        /// Gets the item number.
        /// </summary>
        public int ItemNumber
        {
            get;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ItemBase"/> class.
        /// </summary>
        /// <param name="owner">The order that the item is a part of.</param>
        /// <param name="name">The name of the item.</param>
        protected ItemBase(Order owner, string name)
        {
            Dictionary<Type, int> typeRef;
            if (!_references.TryGetValue(owner.OrderNumber, out typeRef))
            {
                typeRef = new Dictionary<Type, int>();
                _references.Add(owner.OrderNumber, typeRef);
            }

            int refNumber;
            if (!typeRef.TryGetValue(this.GetType(), out refNumber))
            {
                typeRef.Add(this.GetType(), 0);
            }

            typeRef[this.GetType()] = ++refNumber;
            ItemNumber = refNumber;

            this.Owner = owner;
            this.Name = $"{name} {ItemNumber} (Order: {owner.OrderNumber})";
        }

        /// <summary>
        /// Makes the item.
        /// </summary>
        public abstract void Make();

        public override string ToString()
        {
            return Name;
        }
    }
}
