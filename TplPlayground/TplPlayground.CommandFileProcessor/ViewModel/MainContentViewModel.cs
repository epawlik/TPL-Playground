using NullGuard;
using Prism.Commands;
using Prism.Mvvm;
using System.ComponentModel.Composition;
using System.IO.Abstractions;
using System.Windows.Input;
using TplPlayground.Core.Dialogs;

namespace TplPlayground.CommandFileProcessor.ViewModel
{
    [Export(typeof(MainContentViewModel))]
    public class MainContentViewModel : BindableBase
    {
        private readonly IFileSystem _fileSystem;
        private readonly IFolderBrowserDialog _folderBrowserDialog;

        [ImportingConstructor]
        public MainContentViewModel(
            IFileSystem fileSystem,
            IFolderBrowserDialog folderBrowserDialog)
        {
            this._fileSystem = fileSystem;
            this._folderBrowserDialog = folderBrowserDialog;

            this.SelectFolderCommand = new DelegateCommand(this.SelectFolder, this.CanSelectFolder);
        }

        [AllowNull]
        public string FolderPath
        {
            get;
            private set;
        }

        public bool IsBusy
        {
            get;
            private set;
        }

        public ICommand SelectFolderCommand
        {
            get;
        }

        private bool CanSelectFolder() =>
            !IsBusy;

        private void SelectFolder()
        {
            if (_folderBrowserDialog.ShowDialog() == true)
            {
                this.FolderPath = _folderBrowserDialog.SelectedPath;
            }
        }
    }
}
