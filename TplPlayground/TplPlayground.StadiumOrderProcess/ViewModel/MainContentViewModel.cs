using Prism.Commands;
using Prism.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using TplPlayground.Core.Mvvm;
using TplPlayground.StadiumOrderProcess.Model;

namespace TplPlayground.StadiumOrderProcess.ViewModel
{
    [Export(typeof(MainContentViewModel))]
    public class MainContentViewModel : ViewModelBase
    {
        private readonly ExportFactory<Dataflow> _dataFlowFactory;
        private readonly ILoggerFacade _logger;
        private readonly Func<Order, DrinkItem>[] _drinkItemFactories;
        private readonly Func<Order, FoodItem>[] _foodItemFactories;

        [ImportingConstructor]
        public MainContentViewModel(
            ILoggerFacade logger,
            ExportFactory<Dataflow> dataFlowFactory,
            [ImportMany(typeof(Func<Order, FoodItem>))] IEnumerable<Func<Order, FoodItem>> foodFactories,
            [ImportMany(typeof(Func<Order, DrinkItem>))] IEnumerable<Func<Order, DrinkItem>> drinkFactories)
        {
            this._logger = logger;
            this._dataFlowFactory = dataFlowFactory;
            this._foodItemFactories = foodFactories.ToArray();
            this._drinkItemFactories = drinkFactories.ToArray();

            this.RunProcessCommand = DelegateCommand.FromAsyncHandler(this.RunProcessAsync, this.CanRunProcess).ObservesProperty(() => IsBusy);
        }

        public DelegateCommandBase RunProcessCommand
        {
            get;
        }
        
        private bool CanRunProcess() => !IsBusy;

        private IEnumerable<Order> CreateOrders()
        {
            var rand = new Random(Environment.TickCount);

            var orders = (from idx in Enumerable.Range(0, 1000)
                          select new Order())
                              .ToList();
            foreach (var order in orders)
            {
                order.Items = new List<ItemBase>(
                    Enumerable.Range(0, rand.Next(1, 5)).Select(_ => _foodItemFactories[rand.Next(0, _foodItemFactories.Length)](order)).OfType<ItemBase>()
                    .Concat(Enumerable.Range(0, rand.Next(1, 5)).Select(_ => _drinkItemFactories[rand.Next(0, _drinkItemFactories.Length)](order))));
            }

            return orders;
        }

        private async Task RunProcessAsync()
        {
            IsBusy = true;
            _logger.Log("Beginning process.", Category.Info, Priority.None);

            using (var flow = _dataFlowFactory.CreateExport())
            {
                foreach (var order in CreateOrders())
                {
                    // push into dataflow
                    await flow.Value.InputBlock.SendAsync(order);
                }

                // complete the data flow
                flow.Value.Complete();
                await flow.Value.Completion;
            }

            _logger.Log("Ending process.", Category.Info, Priority.None);
            IsBusy = false;
        }
    }
}
