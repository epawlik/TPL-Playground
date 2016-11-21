namespace TplPlayground.StadiumOrderProcess.Model
{
    /// <summary>
    /// Base class for drink items within the refreshment system.
    /// </summary>
    /// <seealso cref="TplPlayground.StadiumOrderProcess.Model.ItemBase" />
    public abstract class DrinkItem : ItemBase
    {
        protected DrinkItem(Order owner, string name)
            : base(owner, name)
        {
        }
    }
}
