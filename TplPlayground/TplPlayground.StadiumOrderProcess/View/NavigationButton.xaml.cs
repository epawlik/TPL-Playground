using System.ComponentModel.Composition;
using System.Windows.Controls;
using TplPlayground.StadiumOrderProcess.ViewModel;

namespace TplPlayground.StadiumOrderProcess.View
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
