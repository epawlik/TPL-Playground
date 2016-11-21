using System;
using System.ComponentModel.Composition;

namespace TplPlayground.StadiumOrderProcess.Model
{
    public class Hamburger : FoodItem
    {
        public Hamburger(Order owner) : base(owner, nameof(Hamburger))
        {
        }

        public override void Make()
        {
            // Make hamburger...
        }

        [Export(typeof(Func<Order, FoodItem>))]
        private static FoodItem CreateItem(Order owner) => new Hamburger(owner);
    }
}
