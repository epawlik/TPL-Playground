using System.ComponentModel.Composition;
using System.Windows.Controls;
using TplPlayground.StadiumOrderProcess.ViewModel;

namespace TplPlayground.StadiumOrderProcess.View
{
    /// <summary>
    /// Interaction logic for MainContent.xaml
    /// </summary>
    [Export(ContractName)]
    public partial class MainContent : UserControl
    {
        internal const string ContractName = "TplPlayground.StadiumOrderProcess.View.MainContent";

        [ImportingConstructor]
        public MainContent(MainContentViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
