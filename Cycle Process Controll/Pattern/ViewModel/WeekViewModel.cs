using CycleProcessControll.Pattern.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading;
using System.IO;
using System.Linq;
namespace CycleProcessControll.Pattern.ViewModel
{
	class WeekViewModel : INotifyPropertyChanged
	{
		TimePatternViewModel[] week = new TimePatternViewModel[7];
		ObservableCollection<string> files;
		public static Dictionary<string, ObservableCollection<TimePeriodViewModel>> Loaded = new Dictionary<string, ObservableCollection<TimePeriodViewModel>>();
		public static SaveDataModel[] save;
		Int32 CurrentHour = 0;
		Timer clock;

		public Command Shit
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

		public WeekViewModel()
		{
			if (!Directory.Exists("Save"))
			{
				Directory.CreateDirectory("Save");
			}

			if (!Directory.Exists("Plugins"))
			{
				Directory.CreateDirectory("Plugins");
			}


			Plugin.PluginManager.Load();
			Load();
			files = new ObservableCollection<string>(Directory.GetFiles(@"Save\").Select(item => item.Split('\\').Last().Split('.')[0]));

			DateTime dt = DateTime.Today;

			week = week.Select(res =>
			{
				TimePatternViewModel v = new TimePatternViewModel(files, dt.ToString("dddd"), save[(int)dt.DayOfWeek], this);dt = dt.AddDays(1);
				return v;
			}).ToArray();
			Thread t = new Thread(() =>
			{
				clock = new Timer(TimerCallback, null, 0, 1000);
			});
			t.Start();
			PatternUpdatedEvent += WeekViewModel_PatternUpdatedEvent;
		}

		public void Load()
		{
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

		void WeekViewModel_PatternUpdatedEvent(string Name)
		{
			StaticPatternModel model = new StaticPatternModel();
			using (StreamReader sr = new StreamReader(@"Save\" + Name + ".json"))
			{
				String Object = sr.ReadToEnd();
				model = Newtonsoft.Json.JsonConvert.DeserializeObject<StaticPatternModel>(Object);
			}
			ObservableCollection<TimePeriodViewModel> Pattern;
			if (Loaded.ContainsKey(Name))
			{
				Pattern = Loaded[Name];
			}
			else
			{
				Pattern = new ObservableCollection<TimePeriodViewModel>();
				Loaded.Add(Name,Pattern);
			}
			Pattern.Clear();
			foreach (TimePeriodModel item in model.Patern)
			{
				model.StartTime += item.Period;
				Pattern.Add(new TimePeriodViewModel(new TimePeriodModel(item.Name, model.StartTime)));
			}
			Console.Write("Pattern Updated: {0}\r", Name);
		}

		void LoadWeek()
		{
			DateTime dt = DateTime.Today.AddDays(1);

			Week = week.Select(res =>
			{
				TimePatternViewModel v = new TimePatternViewModel(files, dt.ToString("dddd"), save[(int)dt.DayOfWeek], this);
				Console.WriteLine("{0}", dt.DayOfWeek);
				dt = dt.AddDays(1);
				return v;
			}).ToArray();
		}

		public static void changeel()
		{
			Loaded.ElementAt(0).Value.Clear();
		}

		void TimerCallback(Object state)
		{
			TimePeriodViewModel model = null;
			TimeText = DateTime.Now.ToString("hh:mm:ss");
			for (int i = CurrentHour; i < Week[0].Pattern.Count; i++)
			{
				if (Week[0].Pattern[i]._period.Period >= DateTime.Now.TimeOfDay)
				{
					model = Week[0].Pattern[i];
					CurrentHour = i;
					break;
				}
			}

			if (model != null)
			{
				PatternName = model.Name;
			}
			else
			{
				PatternName = "";
			}
			NotifyPropertyChanged("PatternName");
			NotifyPropertyChanged("TimeText");
			if (DateTime.Now.TimeOfDay == new TimeSpan(0,0,0))
			{
				LoadWeek();
			}
		}



		public TimePatternViewModel[] Week
		{
			get
			{
				return week;
			}
			set
			{
				week = value;
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

		public delegate void PatternUpdated(String Name);
		public static event PatternUpdated PatternUpdatedEvent;

		public static void PatternUpdate(String name)
		{
			var handler = PatternUpdatedEvent;
			if (handler != null)
				handler(name);
		}

		#region PropertyChanged
		public event PropertyChangedEventHandler PropertyChanged;
		private void NotifyPropertyChanged(string str)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(str));
			}
		}
		#endregion
	}
}
