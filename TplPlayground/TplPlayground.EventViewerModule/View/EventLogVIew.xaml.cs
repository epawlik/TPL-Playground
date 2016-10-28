using System.ComponentModel.Composition;
using System.Windows.Controls;
using TplPlayground.EventViewerModule.ViewModel;

namespace TplPlayground.EventViewerModule.View
{
    /// <summary>
    /// Interaction logic for EventLogVIew.xaml
    /// </summary>
    [Export(typeof(EventLogView))]
    public partial class EventLogView : UserControl
    {
        [ImportingConstructor]
        public EventLogView(EventLogViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }
    }
}
