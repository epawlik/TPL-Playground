using MahApps.Metro.Controls;
using System.ComponentModel.Composition;

namespace TplPlayground
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    [Export(typeof(MainWindow))]
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }
    }
}
