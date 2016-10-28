using Prism.Commands;
using Prism.Events;
using System.Windows.Input;

namespace TplPlayground.Core.Mvvm
{
    public abstract class NavigationViewModelBase
    {
        protected IEventAggregator EventAggregator
        {
            get;
        }

        protected NavigationViewModelBase(IEventAggregator eventAggregator)
        {
            this.EventAggregator = eventAggregator;
            this.NavigateCommand = new DelegateCommand(this.OnNavigate, CanNavigate);
        }

        public ICommand NavigateCommand
        {
            get;
        }

        protected virtual bool CanNavigate() => true;

        protected abstract void OnNavigate();
    }

    public class NavigationEvent : PubSubEvent<string>
    {
    }
}
