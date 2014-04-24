using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
namespace CycleProcessControl.Pattern.ViewModel
{
    class DeviceChooseViewModel : BaseViewModel
    {
        ObservableCollection<DeviceViewModel> Devices = new ObservableCollection<DeviceViewModel>();
        


    }

    class DeviceViewModel : BaseViewModel
    {
        String name;

        public String Name
        {
            get { return name; }
            set {
                name = value;
                NotifyPropertyChanged("Name");
            }
        }
    }
}
