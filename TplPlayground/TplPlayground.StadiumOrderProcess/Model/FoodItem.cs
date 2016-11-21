namespace TplPlayground.StadiumOrderProcess.Model
{
    /// <summary>
    /// Base class for food items within the refreshment system.
    /// </summary>
    /// <seealso cref="TplPlayground.StadiumOrderProcess.Model.ItemBase" />
    public abstract class FoodItem : ItemBase
    {
        protected FoodItem(Order owner, string name)
            : base(owner, name)
        {
        }
    }
}
