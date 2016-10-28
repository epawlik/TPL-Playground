using System.ComponentModel.Composition;
using TplPlayground.Core.Mvvm;

namespace TplPlayground.StadiumOrderProcess.ViewModel
{
    [Export(typeof(MainContentViewModel))]
    public class MainContentViewModel : ViewModelBase
    {
    }
}
