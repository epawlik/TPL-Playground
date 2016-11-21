using System;
using System.ComponentModel.Composition;

namespace TplPlayground.StadiumOrderProcess.Model
{
    public class Beer : DrinkItem
    {
        public Beer(Order owner) : base(owner, nameof(Beer))
        {
        }

        public override void Make()
        {
            // make drink
        }

        [Export(typeof(Func<Order, DrinkItem>))]
        private static DrinkItem CreateItem(Order owner) => new Beer(owner);
    }
}
