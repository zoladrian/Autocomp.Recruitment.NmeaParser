using Autocomp.Nmea.PrismApp.Core.Mvvm;
using Prism.Regions;

namespace Autocomp.Nmea.PrismApp.Modules.ModuleName.ViewModels
{
    public class ParserViewModel : RegionViewModelBase
    {
        private string _message;
        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }

        public ParserViewModel(IRegionManager regionManager) :
            base(regionManager)
        {
        }

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            //do something
        }
    }
}
