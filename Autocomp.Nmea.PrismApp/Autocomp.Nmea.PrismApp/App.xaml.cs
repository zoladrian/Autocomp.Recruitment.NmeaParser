using Autocomp.Nmea.PrismApp.Modules.ModuleName;
using Autocomp.Nmea.PrismApp.Services;
using Autocomp.Nmea.PrismApp.Services.Interfaces;
using Autocomp.Nmea.PrismApp.Views;
using Prism.Ioc;
using Prism.Modularity;
using System.Windows;

namespace Autocomp.Nmea.PrismApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IMessageService, MessageService>();
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<ModuleNameModule>();
        }
    }
}
