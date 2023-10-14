using Prism.Mvvm;

namespace Autocomp.Nmea.PrismApp.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "NmeaParser";

        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public MainWindowViewModel()
        {
        }
    }
}