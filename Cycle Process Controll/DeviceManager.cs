using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
namespace CycleProcessControl
{
    class DeviceManager
    {

        public static Dictionary<Int32, Device> Devices = new Dictionary<Int32, Device>();

        public DeviceManager()
        {

        }

        public static void AddDevice(Device dev)
        {
            Devices.Add(dev.GetHashCode(), dev);
        }

        void SaveDevices() { 
            
        }

       static public Device NewDevice() {
            Device d = new Device();
            d.name = "New Device";
            d.portType = DevicePortType.USB;
            d.value = 0;
            d.hash = d.GetHashCode();
            Devices.Add(d.hash, d);
            return d;
        }

        static internal void Save()
        {
            IFormatter formater = new BinaryFormatter();
            using (FileStream file = new FileStream("DevicesList.bin", FileMode.OpenOrCreate))
            {
                formater.Serialize(file, Devices);
                file.Flush();
            }
            
        }


        static internal void Load()
        {
            
            IFormatter formater = new BinaryFormatter();
            using (FileStream file = new FileStream("DevicesList.bin", FileMode.OpenOrCreate))
            
            {

                if (file.Length == 0) return;
                Devices = (Dictionary<Int32, Device>)formater.Deserialize(file);
            }
        }
    }


    [Serializable()]
    class Device
    {
        public String name;
        public DevicePortType portType;
        public short value;
        public int hash;

        public override string ToString()
        {
            return this.name;
        }
    }
}
