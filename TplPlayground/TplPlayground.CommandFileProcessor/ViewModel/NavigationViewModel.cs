using Prism.Commands;
using Prism.Events;
using System.ComponentModel.Composition;
using System.Windows.Input;
using TplPlayground.Core;

namespace TplPlayground.CommandFileProcessor.ViewModel
{
    [Export(typeof(NavigationViewModel))]
    public class NavigationViewModel
    {
        private readonly IEventAggregator _eventAggregator;

        [ImportingConstructor]
        public NavigationViewModel(IEventAggregator eventAggregator)
        {
            this._eventAggregator = eventAggregator;
            this.NavigateCommand = new DelegateCommand(this.OnNavigate, CanNavigate);
        }

        public ICommand NavigateCommand
        {
            get;
        }

        private static bool CanNavigate() => true;

        private void OnNavigate() =>
            this._eventAggregator.GetEvent<NavigationEvent>().Publish(RegionNames.ContentRegion);
    }

    public class NavigationEvent : PubSubEvent<string>
    {
    }
}
