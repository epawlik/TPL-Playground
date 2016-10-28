using Prism.Events;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using TplPlayground.Core.Logging;

namespace TplPlayground.EventViewerModule.ViewModel
{
    [Export(typeof(EventLogViewModel))]
    public class EventLogViewModel
    {
        private readonly IEventAggregator _eventAggregator;

        public ObservableCollection<LogMessage> LogEntries { get; }

        [ImportingConstructor]
        public EventLogViewModel(IEventAggregator eventAggregator)
        {
            this._eventAggregator = eventAggregator;
            LogEntries = new ObservableCollection<LogMessage>();
            _eventAggregator.GetEvent<LogMessageEvent>().Subscribe(msg =>
            {
                LogEntries.Add(msg);
            }, ThreadOption.UIThread);
        }
    }
}
