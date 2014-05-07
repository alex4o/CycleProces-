using CycleProcessControl.Pattern.Model;
using Newtonsoft.Json;
using System;
using System.Windows;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading;
using System.IO;
using System.Linq;
using LPT;
using CycleProcessControl.Pattern.View;
namespace CycleProcessControl.Pattern.ViewModel
{
	class WeekViewModel : BaseViewModel
	{
		TimePatternViewModel[] week = new TimePatternViewModel[7];
		public static ObservableCollection<string> files;
		public static Dictionary<string, ObservableCollection<PreviewPeriodViewModel>> Loaded = new Dictionary<string, ObservableCollection<PreviewPeriodViewModel>>();
		public static SaveDataModel[] save;
		//static Int32 CurrentHour = 0;
		Timer clock;

		public Command AddPeriod
		{
			get
			{
				return new Command(() =>
				{

					CreatePatternDialog d = new CreatePatternDialog();
					d.ShowDialog();
					//Loaded.ElementAt(0).Value.Add(new TimePeriodViewModel(new TimePeriodModel("Hello World", new TimeSpan(0, 20, 0))));
				});
			}
		}

        public Command Settings
        {
            get
            {
                return new Command(() =>
                {
                    DeviceChoose view = new DeviceChoose();
                    view.Show();

                });
            }
        }

		public WeekViewModel()
		{
           // Plugin.PluginManager.Load();
            DeviceManager.Load();
			Load();
			

            // Get current day
			DateTime dt = DateTime.Today;


            // Load week
			week = week.Select(res =>
			{
				TimePatternViewModel v = new TimePatternViewModel(dt.ToString("dddd"), save[(int)dt.DayOfWeek], this);dt = dt.AddDays(1);
				return v;
			}).ToArray();

            // Start The Clock
			Thread t = new Thread(() =>
			{
				clock = new Timer(TimerCallback, null, 0, 1000);
			});
			t.Start();


            // Event Subscriptions
			PatternRemovedEvent += WeekViewModel_PatternRemovedEvent;
		}

		public void Load()
        {
            files = new ObservableCollection<string>(Directory.GetFiles(@"Save\").Select(item => item.Split('\\').Last().Split('.')[0]));

			if (File.Exists("settings.json"))
			{
				String obj;
				using (StreamReader sr = new StreamReader("settings.json"))
				{
					obj = sr.ReadToEnd();
				}
				save = JsonConvert.DeserializeObject<SaveDataModel[]>(obj);
				if (save == null || save.Length < 7)
				{
					save = new SaveDataModel[7];
					save = save.Select(res => res = new SaveDataModel()).ToArray();
					Save();
				}
			}
			else
			{
				SaveDataModel[] save = new SaveDataModel[7];
				save = save.Select(res => res = new SaveDataModel()).ToArray();
				Save();
			}
		}

		public void Save()
		{
			using (StreamWriter sw = new StreamWriter("settings.json"))
			{
				sw.Write(JsonConvert.SerializeObject(save));
				sw.Flush();
				//sw.Dispose();
			}

			Thread PluginSave = new Thread(() =>
			{
				//Plugin.PluginManager.Plugins.ForEach(item => item.Save());
			});
			PluginSave.Start();
		}


		void WeekViewModel_PatternRemovedEvent(string Name)
		{
			files.Remove(Name);
		}

		void LoadWeek()
		{
			DateTime dt = DateTime.Today;

			week = week.Select(res =>
			{
				TimePatternViewModel v = new TimePatternViewModel(dt.ToString("dddd"), save[(int)dt.DayOfWeek], this);
				//Console.WriteLine("{0}", dt.DayOfWeek);
				dt = dt.AddDays(1);
				return v;
			}).ToArray();
            NotifyPropertyChanged("Week");
		}

		//PreviewPeriodViewModel model = null;
		//TimeSpan Start;
		void TimerCallback(Object state)
		{

            PatternName = week[0].Check();


            TimeText = DateTime.Now.ToString("HH:mm:ss");
            NotifyPropertyChanged("TimeText");
            NotifyPropertyChanged("PatternName");
		//	Console.WriteLine((long)(DateTime.Now.TimeOfDay.Ticks / TimeSpan.TicksPerSecond));
			if ((DateTime.Now.TimeOfDay.Ticks / TimeSpan.TicksPerSecond) == 0)
			{
				LoadWeek();
			}
		}


        #region Properties
        public ObservableCollection<TimePatternViewModel> Week
		{
			get
			{
				return new ObservableCollection<TimePatternViewModel>(week);
			}
            set {
                week = value.ToArray();
                NotifyPropertyChanged("Week");
            }
		}
		public String PatternName
		{
			get;
			set;
		}
        public String TimeText
        {
            get;
            set;
        }
        #endregion

        #region events
        public delegate void PatternUpdated(String Name);
		public static event PatternUpdated PatternUpdatedEvent;

		public delegate void PatternRemoved(String Name);
		public static event PatternRemoved PatternRemovedEvent;

		public static void PatternUpdate(String name)
		{
			var handler = PatternUpdatedEvent;
			if (handler != null)
				handler(name);
		}

		public static void PatternRemove(String name)
		{
			var handler = PatternRemovedEvent;
			if (handler != null)
				handler(name);
		}

        #endregion
       
    }
}
