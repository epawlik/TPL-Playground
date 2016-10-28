using Prism.Mvvm;

namespace TplPlayground.Core.Mvvm
{
    /// <summary>
    /// Base view-model implementation containing common view-model functionality.
    /// </summary>
    /// <seealso cref="Prism.Mvvm.BindableBase" />
    public abstract class ViewModelBase : BindableBase
    {
        /// <summary>
        /// Gets or sets a value indicating whether this instance is busy.
        /// </summary>
        public bool IsBusy
        {
            get;
            set;
        }
    }
}
