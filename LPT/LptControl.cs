using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace LPT
{
    public static class LPT
    {
		[DllImport("inpout32.dll", EntryPoint = "Out32")]
		private static extern void Out(short addr, short val);

		[DllImport("inpout32.dll", EntryPoint = "Inp32")]
		private static extern short Inp(short addr);

		public static void On(byte value) {
			Out(0x278, value);
			Out(0x378, value);
			Out(0x38C, value);
		}
		public static void Off() {
			Out(0x278, 0);
			Out(0x378, 0);
			Out(0x38C, 0);
		}
    }
}
