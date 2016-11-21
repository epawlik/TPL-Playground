using System;
using System.Collections.Generic;

namespace TplPlayground.StadiumOrderProcess.Model
{
    public class Order
    {
        private static int _orderNumber = 0;

        public List<ItemBase> Items
        {
            get;
            set;
        }

        public int OrderNumber
        {
            get;
        } = ++_orderNumber;

        public override string ToString() =>
            $"Order {OrderNumber}{Environment.NewLine}{string.Join(Environment.NewLine, Items)}";
    }
}
