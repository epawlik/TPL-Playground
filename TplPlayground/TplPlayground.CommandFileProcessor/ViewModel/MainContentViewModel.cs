using Prism.Mvvm;
using System.ComponentModel.Composition;

namespace TplPlayground.CommandFileProcessor.ViewModel
{
    [Export(typeof(MainContentViewModel))]
    public class MainContentViewModel : BindableBase
    {
    }
}
