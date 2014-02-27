using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.IO;
using CycleProcessControll.Pattern.Model;
using System.Threading;

namespace CycleProcessControll.Pattern.ViewModel
{
	class TimePatternViewModel : INotifyPropertyChanged
	{
		ObservableCollection<string> patterns;
		ObservableCollection<TimePeriodViewModel> pattern;
		SaveDataModel savedata;
		WeekViewModel MainViewModel;
		//public string selectedfile;
		public TimePatternViewModel(ObservableCollection<string> p, String Day, SaveDataModel savedata, WeekViewModel week)
		{
			patterns = p;
			this.Day = Day;
			this.savedata = savedata;
			if (savedata.Name != null && savedata.Name != "") {
				Load(savedata.Name);
			}
			else
			{
				pattern = new ObservableCollection<TimePeriodViewModel>();
			}
			MainViewModel = week;

			
		}

		public ObservableCollection<TimePeriodViewModel> Pattern
		{
			get
			{
				return pattern;
			}
			set {
				pattern = value;
				NotifyPropertyChanged("Pattern");
			}
		}

		public String Day
		{
			get;
			set;
		}

		public Command Edit{
			get {
				return new Command(() => {
					CycleProcessControll.Pattern.View.MainWindow StaticView = new CycleProcessControll.Pattern.View.MainWindow();
					(StaticView.DataContext as CycleProcessControll.Pattern.ViewModel.StaticPatternViweModel).FileOpen(savedata.Name);
					StaticView.Show();
					
				});
			}
		} 
		public ObservableCollection<string> Patterns
		{
			get
			{
				return patterns;
			}

			set
			{
				patterns = value;
				
			}
		}

		private void Load(String Name) {

			if (WeekViewModel.Loaded.ContainsKey(Name))
			{
				Pattern = WeekViewModel.Loaded[Name];
				return;
			}

			StaticPatternModel model = new StaticPatternModel();
		
			using (StreamReader sr = new StreamReader(@"Save\" + Name + ".json"))
			{
				String Object = sr.ReadToEnd();
				model = Newtonsoft.Json.JsonConvert.DeserializeObject<StaticPatternModel>(Object);
			}
			TimeSpan StartTime = model.StartTime;
			if (pattern == null) Pattern = new ObservableCollection<TimePeriodViewModel>();
			pattern.Clear();
			
			foreach (TimePeriodModel item in model.Patern)
			{
				StartTime += item.Period;

				pattern.Add(new TimePeriodViewModel(new TimePeriodModel(item.Name,StartTime)));
			}
			
			WeekViewModel.Loaded.Add(Name, pattern);
			NotifyPropertyChanged("SelectedFile");
			
		}

		public String SelectedFile
		{
			set
			{
				if (value == null)
				{
					return;
				}
				savedata.Name = value;
				MainViewModel.Save();
				Load(value);		
				//NotifyPropertyChanged("SelectedFile");
			}
			get{
				return savedata.Name;
			}
			
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
