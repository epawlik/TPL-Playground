using Prism.Events;
using System.ComponentModel.Composition;
using TplPlayground.Core;
using TplPlayground.Core.Mvvm;

namespace TplPlayground.StadiumOrderProcess.ViewModel
{
    [Export(typeof(NavigationViewModel))]
    public class NavigationViewModel : NavigationViewModelBase
    {
        [ImportingConstructor]
        public NavigationViewModel(IEventAggregator eventAggregator) : base(eventAggregator)
        {
        }

        protected override void OnNavigate() =>
            this.EventAggregator.GetEvent<StadiumOrderNavigationEvent>().Publish(RegionNames.ContentRegion);
    }

    public class StadiumOrderNavigationEvent : NavigationEvent
    {
    }
}
