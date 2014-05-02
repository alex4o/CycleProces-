using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
namespace CycleProcessControl.Pattern.ViewModel
{
    class DeviceChooseViewModel : BaseViewModel
    {
        ObservableCollection<DeviceViewModel> devices = new ObservableCollection<DeviceViewModel>();



        private DeviceViewModel current;
        int selected;


        public DeviceChooseViewModel()
        {
            

            foreach (var item in DeviceManager.Devices)
            {
                devices.Add(new DeviceViewModel(item.Value));
            }
        }

        public ObservableCollection<DeviceViewModel> Devices
        {
            get { return devices; }
            set { devices = value; NotifyPropertyChanged("Devices"); }
        }


        public int Selected
        {
            get {
                return selected;
            
            }
            set { 
                selected = value;
                
                NotifyPropertyChanged("Selected"); }
        }


        public DeviceViewModel Current
        {
            get { return current; }
            set {
                if (value == null) return;
                current = value;
                current.Name = value.Name;
                current.PortType = value.PortType;
                current.Value = value.Value;
                NotifyPropertyChanged("Current");
            
            }
        }


        public Command AddCommand
        {
            get
            {
                return new Command(() =>
                {
                    Devices.Add(new DeviceViewModel(DeviceManager.NewDevice()));

                });
            }
        }

        public Command SaveCommand
        {
            get
            {
                return new Command(() =>
                {
                    DeviceManager.Save();
                });
            }
        }

        public Command RemoveCommand
        {
            get
            {
                return new Command(() =>
                {
                    if (Selected < 0) return;
                    Devices.RemoveAt(Selected);
                });
            }
        }
    }

    class DeviceViewModel : BaseViewModel
    {
        Device model;
        

        public DeviceViewModel(Device dev)
        {
            model = dev;
        }

        public DeviceViewModel()
        {
            model = new Device();
        }

        public String Name
        {
            get { 
                return model.name;
            }
            set
            {
                model.name = value;
                NotifyPropertyChanged("Name");
            }
        }

        public short Value
        {
            get {
                return model.value;
            }
            set {
                model.value = value;
                NotifyPropertyChanged("Value");
            }
        }

        public DevicePortType PortType
        {
            get { 
                return model.portType;
            }
            set { 
                model.portType = value; 
                NotifyPropertyChanged("PortType");
            }
        }
    }
}
