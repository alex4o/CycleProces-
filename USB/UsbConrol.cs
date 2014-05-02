using LibUsbDotNet;
using PasatSoft.RelayBoard.SDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace USB
{
    public class UsbControl
    {
        static UsbPort port;
        static UsbDevice[] devices;
        static UsbControl()
        {
            port = new UsbPort();
            devices = port.GetDevices();
            
        }

        public static void On(short value)
        {
            port.SetRelayStatus(devices[0], 0, (byte)(value & 255));
        }
        public static void Off()
        {
            port.SetRelayStatus(devices[0], 0, 0x0);
        }
    }
}
