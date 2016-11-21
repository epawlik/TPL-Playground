using System;
using System.ComponentModel.Composition;

namespace TplPlayground.StadiumOrderProcess.Model
{
    public class Soda : DrinkItem
    {
        public Soda(Order owner) : base(owner, nameof(Soda))
        {
        }

        public override void Make()
        {
            // make drink
        }

        [Export(typeof(Func<Order, DrinkItem>))]
        private static DrinkItem CreateItem(Order owner) => new Soda(owner);
    }
}
