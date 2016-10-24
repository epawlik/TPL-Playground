using System.ComponentModel.Composition;
using System.Windows.Controls;
using TplPlayground.CommandFileProcessor.ViewModel;

namespace TplPlayground.CommandFileProcessor.View
{
    /// <summary>
    /// Interaction logic for MainContent.xaml
    /// </summary>
    [Export(ContractName)]
    public partial class MainContent : UserControl
    {
        internal const string ContractName = "TplPlayground.CommandFileProcessor.View.MainContent";
        
        [ImportingConstructor]
        public MainContent(MainContentViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
