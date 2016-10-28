using Prism.Logging;
using Prism.Mef;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Registration;
using System.IO.Abstractions;
using System.Windows;

namespace TplPlayground
{
    public class Bootstrapper : MefBootstrapper
    {
        protected override void ConfigureAggregateCatalog()
        {
            base.ConfigureAggregateCatalog();

            // Add this assembly
            this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(Bootstrapper).Assembly));
            this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(Core.RegionNames).Assembly));
            this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(EventViewerModule.EventViewerModule).Assembly));
            this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(CommandFileProcessor.CommandFileProcessorModule).Assembly));
            this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(StadiumOrderProcess.StadiumOrderProcessModule).Assembly));

            // set up the exports for external dependencies
            var builder = new RegistrationBuilder();
            builder.ForType<FileSystem>().Export<IFileSystem>();
            this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(IFileSystem).Assembly, builder));
        }

        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();

            Container.SatisfyImportsOnce(this.Logger);
        }

        protected override ILoggerFacade CreateLogger()
        {
            return new Core.Logging.EventPublishingLogger(base.CreateLogger());
        }

        protected override DependencyObject CreateShell()
        {
            return Container.GetExportedValue<MainWindow>();
        }

        protected override void InitializeShell()
        {
            Application.Current.MainWindow.Show();
        }
    }
}
