using System.ComponentModel.Composition;
using System.Windows.Controls;

namespace TplPlayground.CommandFileProcessor.View
{
    /// <summary>
    /// Interaction logic for MainContent.xaml
    /// </summary>
    [Export(ContractName)]
    public partial class MainContent : UserControl
    {
        internal const string ContractName = "TplPlayground.CommandFileProcessor.View.MainContent";
        
        public MainContent()
        {
            InitializeComponent();
        }
    }
}
