using System;
using System.ComponentModel.Composition;

namespace TplPlayground.StadiumOrderProcess.Model
{
    public class Pizza : FoodItem
    {
        public Pizza(Order owner) : base(owner, nameof(Pizza))
        {
        }

        public override void Make()
        {
            // Make pizza...
        }

        [Export(typeof(Func<Order, FoodItem>))]
        private static FoodItem CreateItem(Order owner) => new Pizza(owner);
    }
}
