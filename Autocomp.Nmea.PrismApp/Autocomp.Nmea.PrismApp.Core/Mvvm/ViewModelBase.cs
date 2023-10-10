using Prism.Mvvm;
using Prism.Navigation;

namespace Autocomp.Nmea.PrismApp.Core.Mvvm
{
    public abstract class ViewModelBase : BindableBase, IDestructible
    {
        protected ViewModelBase()
        {

        }

        public virtual void Destroy()
        {

        }
    }
}
