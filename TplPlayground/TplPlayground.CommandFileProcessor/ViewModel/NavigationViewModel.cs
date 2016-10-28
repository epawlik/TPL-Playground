using Prism.Events;
using System.ComponentModel.Composition;
using TplPlayground.Core;
using TplPlayground.Core.Mvvm;

namespace TplPlayground.CommandFileProcessor.ViewModel
{
    [Export(typeof(NavigationViewModel))]
    public class NavigationViewModel : NavigationViewModelBase
    {
        [ImportingConstructor]
        public NavigationViewModel(IEventAggregator eventAggregator) : base(eventAggregator)
        {
        }

        protected override void OnNavigate() =>
            this.EventAggregator.GetEvent<CommandFileNavigationEvent>().Publish(RegionNames.ContentRegion);
    }

    public class CommandFileNavigationEvent : NavigationEvent
    {
    }
}
