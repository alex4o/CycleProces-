using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.IO;
using CycleProcessControl.Pattern.Model;
using System.Threading;

namespace CycleProcessControl.Pattern.ViewModel
{
	class TimePatternViewModel : INotifyPropertyChanged
	{
		//ObservableCollection<string> patterns;
		ObservableCollection<PreviewPeriodViewModel> pattern;
		SaveDataModel savedata;
		WeekViewModel MainViewModel;
		//public string selectedfile;
		public TimePatternViewModel(String Day, SaveDataModel savedata, WeekViewModel week)
		{
			//patterns = p;
			this.Day = Day;
			this.savedata = savedata;
			if (savedata.Name != null && savedata.Name != "") {
				Load(savedata.Name);
			}
			else
			{
				pattern = new ObservableCollection<PreviewPeriodViewModel>();
			}
			MainViewModel = week;

			
		}

		public ObservableCollection<PreviewPeriodViewModel> Pattern
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
					CycleProcessControl.Pattern.View.MainWindow StaticView = new CycleProcessControl.Pattern.View.MainWindow();
					(StaticView.DataContext as CycleProcessControl.Pattern.ViewModel.StaticPatternViewModel).FileOpen(savedata.Name);
					StaticView.Show();
					
				});
			}
		} 
		public ObservableCollection<string> Patterns
		{
			get
			{
				return WeekViewModel.files;
			}

			set
			{
				WeekViewModel.files = value;
				
			}
		}

		private void Load(String Name) {

			if (WeekViewModel.Loaded.ContainsKey(Name))
			{
				Pattern = WeekViewModel.Loaded[Name];
				return;
			}

			StaticPatternModel model = new StaticPatternModel();
			if (File.Exists(@"Save\" + Name + ".json")) {
				
			
			
				using (StreamReader sr = new StreamReader(@"Save\" + Name + ".json"))
				{
					String Object = sr.ReadToEnd();
					model = Newtonsoft.Json.JsonConvert.DeserializeObject<StaticPatternModel>(Object);
				}
			
			}
			if (model == null) {
				Pattern = new ObservableCollection<PreviewPeriodViewModel>();
				WeekViewModel.Loaded.Add(Name, Pattern);
				return;
			}
			TimeSpan StartTime = model.StartTime;
			Pattern = new ObservableCollection<PreviewPeriodViewModel>();
			pattern.Clear();
			
			foreach (TimePeriodModel item in model.Patern)
			{
				model.StartTime = StartTime;
				StartTime += item.Period;

				pattern.Add(new PreviewPeriodViewModel(item, model.StartTime,StartTime));
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
		
	}
}
