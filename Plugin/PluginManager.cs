using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Plugin
{
	public class PluginManager
	{

		public static List<IDBManager> Plugins = new List<IDBManager>();

		public static void Load()
		{
			foreach (String folder in Directory.GetDirectories(AppDomain.CurrentDomain.BaseDirectory + @"Plugins"))
			{
				//StreamReader sr = new StreamReader(folder + @"\Plugin.json");
				//String list = sr.ReadToEnd();
				//var Data = JsonConvert.DeserializeObject<dynamic>(list);
				Assembly asm = Assembly.LoadFile(folder + @"\"+folder.Split('\\').Last()+".dll");
			//	Type t = asm.GetType("Connection");

				Type[] types = asm.GetTypes();
				
				IDBManager manager = Activator.CreateInstance(types[0]) as IDBManager;
                Console.WriteLine("{0} Loaded!", asm.GetName().Name);
                
				//manager.Settings = Data.Prop;
				Plugins.Add(manager);
			}
		}
		static PluginManager()
		{
			AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(MyResolveEventHandler);
		}

		private static Assembly MyResolveEventHandler(object sender, ResolveEventArgs args)
		{
			try
			{
				return Assembly.LoadFile(Path.GetDirectoryName(args.RequestingAssembly.Location) + "\\" + args.Name.Split(',')[0] + ".dll");
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);

			}
			return null;
		}
	}
}

