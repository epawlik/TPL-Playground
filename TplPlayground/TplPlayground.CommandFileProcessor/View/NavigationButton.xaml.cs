using System.ComponentModel.Composition;
using System.Windows.Controls;
using TplPlayground.CommandFileProcessor.ViewModel;

namespace TplPlayground.CommandFileProcessor.View
{
    /// <summary>
    /// Interaction logic for NavigationButton.xaml
    /// </summary>
    [Export(typeof(NavigationButton))]
    public partial class NavigationButton : UserControl
    {
        [ImportingConstructor]
        public NavigationButton(NavigationViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }
    }
}
