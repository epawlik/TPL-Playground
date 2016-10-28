using Prism.Events;
using Prism.Mef.Modularity;
using Prism.Modularity;
using Prism.Regions;
using System.ComponentModel.Composition;
using TplPlayground.Core;
using TplPlayground.Core.Mvvm;
using TplPlayground.StadiumOrderProcess.View;

namespace TplPlayground.StadiumOrderProcess
{
    [ModuleExport(typeof(StadiumOrderProcessModule))]
    public class StadiumOrderProcessModule : IModule
    {
        private readonly IRegionManager _regionManager;
        private readonly IEventAggregator _eventAggregator;

        [ImportingConstructor]
        public StadiumOrderProcessModule(IRegionManager regionManager, IEventAggregator eventAggregator)
        {
            this._regionManager = regionManager;
            this._eventAggregator = eventAggregator;
        }

        public void Initialize()
        {
            // add views to regions
            this._regionManager.RegisterViewWithRegion(RegionNames.ButtonRegion, typeof(NavigationButton));
            this._eventAggregator.GetEvent<NavigationEvent>().Subscribe(NavigateTo);
        }

        private void NavigateTo(string regionName) =>
            this._regionManager.RequestNavigate(regionName, MainContent.ContractName);
    }
}
