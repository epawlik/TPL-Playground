using NullGuard;
using Prism.Commands;
using System.ComponentModel.Composition;
using System.IO.Abstractions;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using System.Windows.Input;
using TplPlayground.Core.Dialogs;
using TplPlayground.Core.Mvvm;

namespace TplPlayground.CommandFileProcessor.ViewModel
{
    [Export(typeof(MainContentViewModel))]
    public class MainContentViewModel : ViewModelBase
    {
        private readonly ExportFactory<Dataflow> _dataFlowFactory;
        private readonly IFileSystem _fileSystem;
        private readonly IFolderBrowserDialog _folderBrowserDialog;

        [ImportingConstructor]
        public MainContentViewModel(
            IFileSystem fileSystem,
            IFolderBrowserDialog folderBrowserDialog,
            ExportFactory<Dataflow> dataFlowFactory)
        {
            this._fileSystem = fileSystem;
            this._folderBrowserDialog = folderBrowserDialog;
            this._dataFlowFactory = dataFlowFactory;

            this.SelectFolderCommand = new DelegateCommand(this.SelectFolder, this.CanSelectFolder).ObservesProperty(() => IsBusy);
            this.RunProcessCommand = DelegateCommand.FromAsyncHandler(this.RunProcessAsync, this.CanRunProcess);
        }

        [AllowNull]
        public string FolderPath
        {
            get;
            private set;
        }

        public DelegateCommandBase RunProcessCommand
        {
            get;
        }

        public ICommand SelectFolderCommand
        {
            get;
        }

        private bool CanRunProcess() =>
            !string.IsNullOrEmpty(FolderPath) && !IsBusy;

        private bool CanSelectFolder() =>
            !IsBusy;

        private async Task RunProcessAsync()
        {
            IsBusy = true;

            using (var flow = _dataFlowFactory.CreateExport())
            {
                // run through the files and push them into the dataflow
                foreach (var file in _fileSystem.Directory.EnumerateFiles(FolderPath, "*.txt", System.IO.SearchOption.AllDirectories))
                {
                    // push into dataflow
                    await flow.Value.InputBlock.SendAsync(file);
                }

                // complete the data flow
                flow.Value.Complete();
                await flow.Value.Completion;
            }

            IsBusy = false;
        }

        private void SelectFolder()
        {
            if (_folderBrowserDialog.ShowDialog() == true)
            {
                this.FolderPath = _folderBrowserDialog.SelectedPath;
                RunProcessCommand.RaiseCanExecuteChanged();
            }
        }
    }
}
