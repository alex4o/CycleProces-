using CycleProcessControl;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace CycleProcessControll
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
	
		public App()
		{
            // Test for directory existance
            if (!Directory.Exists("Save"))
            {
                Directory.CreateDirectory("Save");
            }

            if (!Directory.Exists("Plugins"))
            {
                Directory.CreateDirectory("Plugins");
            }

		}
	}
}
