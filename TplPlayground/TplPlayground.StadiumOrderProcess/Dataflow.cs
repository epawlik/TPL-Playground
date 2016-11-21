using Prism.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using TplPlayground.StadiumOrderProcess.Model;

namespace TplPlayground.StadiumOrderProcess
{
    [Export(typeof(Dataflow))]
    public class Dataflow : IDataflowBlock
    {
        private readonly ILoggerFacade _logger;

        [ImportingConstructor]
        public Dataflow(
            ILoggerFacade logger)
        {
            this._logger = logger;

            var parallelBlockOptions = new ExecutionDataflowBlockOptions
            {
                BoundedCapacity = 10,
                MaxDegreeOfParallelism = Math.Max(1, Environment.ProcessorCount - 1)
            };

            var linkOptions = new DataflowLinkOptions
            {
                PropagateCompletion = true
            };

            /*                  Accept Order
             *                          |
             *                  Stream Food and Drinks
             *                  /               \
             *                 /                 \
             *                /                   \
             *      Make Food                       Make Drinks
             *          |                               |
             *          |                               |
             *      Merge Food                      Merge Drinks
             *              \                       /
             *               \                     /
             *                \                   /
             *                  Join Food and Drinks
             *                          |
             *                          |
             *                  Complete Order
             *                          |
             *                  Print Order
            */

            // create the dataflow blocks
            var acceptOrderBlock = new BufferBlock<Order>();
            var streamFoodAndDrinks = new TransformManyBlock<Order, ItemBase>(order => StreamOrder(order), parallelBlockOptions);
            var makeFoodBlock = new TransformBlock<ItemBase, FoodItem>(food => MakeFood(food as FoodItem), parallelBlockOptions);
            var makeDrinkBlock = new TransformBlock<ItemBase, DrinkItem>(drink => MakeDrink(drink as DrinkItem), parallelBlockOptions);
            var mergeFood = CreaterMergerBlock(
                (ItemBase item) => item?.Owner?.Items?.OfType<FoodItem>() ?? new List<FoodItem>(), item => item?.Count() ?? 0, parallelBlockOptions);

            var mergeDrinks = CreaterMergerBlock(
                (ItemBase item) => item?.Owner?.Items?.OfType<DrinkItem>() ?? new List<DrinkItem>(), item => item?.Count() ?? 0, parallelBlockOptions);
            var joinBlock = new JoinBlock<IEnumerable<FoodItem>, IEnumerable<DrinkItem>>();
            var completeOrderBlock = new TransformBlock<Tuple<IEnumerable<FoodItem>, IEnumerable<DrinkItem>>, Order>(foodAndDrinks => CompleteOrder(foodAndDrinks));
            var printBlock = new ActionBlock<Order>(order => PrintCompletedOrder(order));

            InputBlock = acceptOrderBlock;
            Completion = printBlock.Completion;

            // link dataflow blocks together
            acceptOrderBlock.LinkTo(streamFoodAndDrinks, linkOptions);
            streamFoodAndDrinks.LinkTo(makeFoodBlock, linkOptions, item => item is FoodItem);
            streamFoodAndDrinks.LinkTo(makeDrinkBlock, linkOptions, item => item is DrinkItem);
            makeFoodBlock.LinkTo(mergeFood, linkOptions);
            makeDrinkBlock.LinkTo(mergeDrinks, linkOptions);
            mergeFood.LinkTo(joinBlock.Target1);
            mergeDrinks.LinkTo(joinBlock.Target2);
            joinBlock.LinkTo(completeOrderBlock, linkOptions);
            completeOrderBlock.LinkTo(printBlock, linkOptions);

            Task.WhenAll(mergeFood.Completion, mergeDrinks.Completion).ContinueWith(x => { joinBlock.Complete(); });

            // debug task completion
            DebugCompletion(acceptOrderBlock, "Accept Order Block");
            DebugCompletion(streamFoodAndDrinks, "Stream Food and Drinks Block");
            DebugCompletion(mergeFood, "Merge Food Block");
            DebugCompletion(mergeDrinks, "Merge Drinks Block");
            DebugCompletion(makeFoodBlock, "Make Food Block");
            DebugCompletion(makeDrinkBlock, "Make Drinks Block");
            DebugCompletion(joinBlock, "Join Block");
            DebugCompletion(completeOrderBlock, "Complete Order Block");
            DebugCompletion(printBlock, "Print Block");
        }

        private Order CompleteOrder(Tuple<IEnumerable<FoodItem>, IEnumerable<DrinkItem>> foodAndDrinks)
        {
            return foodAndDrinks.Item1.First().Owner;
        }

        private FoodItem MakeFood(FoodItem food)
        {
            food.Make();
            return food;
        }

        private DrinkItem MakeDrink(DrinkItem drink)
        {
            drink.Make();
            return drink;
        }

        private IEnumerable<ItemBase> StreamOrder(Order order)
        {
            return order.Items.OfType<FoodItem>().OfType<ItemBase>()
                .Concat(order.Items.OfType<DrinkItem>());
        }

        private void PrintCompletedOrder(Order order) =>
            _logger.Log(order.ToString(), Category.Info, Priority.None);

        public Task Completion
        {
            get;
        }

        public ITargetBlock<Order> InputBlock
        {
            get;
        }

        public void Complete() =>
            InputBlock.Complete();

        public void Fault(Exception exception)
        {
            // TODO:...
        }

        private void DebugCompletion(IDataflowBlock block, string name) =>
            block.Completion.ContinueWith(_ =>
            {
                var msg = $"Completed Task: {name}";
                Debug.WriteLine(msg, ConsoleColor.Cyan);
                _logger.Log(msg, Category.Info, Priority.None);
            });

        public static IPropagatorBlock<TSplit, TMerged> CreaterMergerBlock<TSplit, TMerged>(
            Func<TSplit, TMerged> getMergedFunc,
            Func<TMerged, int> getSplitCount,
            ExecutionDataflowBlockOptions options)
        {
            var dictionary = new Dictionary<TMerged, int>();

            return new TransformManyBlock<TSplit, TMerged>(
                split =>
                {
                    var merged = getMergedFunc(split);
                    int count;
                    dictionary.TryGetValue(merged, out count);
                    count++;
                    if (getSplitCount(merged) <= count)
                    {
                        dictionary.Remove(merged);
                        return new[] { merged };
                    }

                    dictionary[merged] = count;
                    return new TMerged[0];
                }, options);
        }
    }
}
