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
		//Dictionary<string, Action> Events = new Dictionary<string,Action>();


		public Command Shit
		{
			get
			{
				return new Command(() =>
				{
					Loaded.ElementAt(0).Value.Add(new TimePeriodViewModel(new TimePeriodModel("Hello World", new TimeSpan(0, 20, 0))));
				});
			}
		}

		public WeekViewModel()
		{
			Load();
			//TimeText = "12:20";
			files = new ObservableCollection<string>(Directory.GetFiles(@"Save\").Select(item => item.Split('\\').Last().Split('.')[0]));

			DateTime dt = DateTime.Today;

			week = week.Select(res =>
			{
				TimePatternViewModel v = new TimePatternViewModel(files, dt.ToString("dddd"), save[(int)dt.DayOfWeek]);
				Console.WriteLine("{0}", dt.DayOfWeek);
				dt = dt.AddDays(1);
				return v;
			}).ToArray();

			Thread clock = new Thread(() => {
				
			});
			clock.Start();

			PatternUpdatedEvent += WeekViewModel_PatternUpdatedEvent;
		}

		public static void Load()
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
		}

		public static void Save()
		{
			using (StreamWriter sw = new StreamWriter("settings.json"))
			{
				sw.Write(JsonConvert.SerializeObject(save));
				sw.Flush();
				sw.Dispose();
			}

		}

		void WeekViewModel_PatternUpdatedEvent(string Name)
		{
			StaticPatternModel model = new StaticPatternModel();
			using (StreamReader sr = new StreamReader(@"Save\" + Name + ".json"))
			{
				String Object = sr.ReadToEnd();
				model = Newtonsoft.Json.JsonConvert.DeserializeObject<StaticPatternModel>(Object);
			}
			ObservableCollection<TimePeriodViewModel> Pattern = Loaded[Name];
			Pattern.Clear();
			foreach (TimePeriodModel item in model.Patern)
			{
				Pattern.Add(new TimePeriodViewModel(item));
			}
			Console.Write("Pattern Updated: {0}\r", Name);
		}

		public static void changeel()
		{
			Loaded.ElementAt(0).Value.Clear();
		}

		public TimePatternViewModel[] Week
		{
			get
			{
				return week;
			}
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
