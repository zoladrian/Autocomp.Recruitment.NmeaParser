using Autocomp.Nmea.PrismApp.Core;
using Autocomp.Nmea.PrismApp.Modules.ModuleName.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace Autocomp.Nmea.PrismApp.Modules.ModuleName
{
    public class ParserModule : IModule
    {
        private readonly IRegionManager _regionManager;

        public ParserModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            _regionManager.RequestNavigate(RegionNames.ContentRegion, "ParserView");
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<ParserView>();
        }
    }
}