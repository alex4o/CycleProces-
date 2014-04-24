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
	class TimePatternViewModel : BaseViewModel
	{
		ObservableCollection<PreviewPeriodViewModel> pattern;
		SaveDataModel savedata;
		WeekViewModel MainViewModel;
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
            Current = 0;
            Check();

            pattern.CollectionChanged += pattern_CollectionChanged;
		}

        void pattern_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Reset) {
                Current = 0;
            }
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

        public int Current
        {
            get; 
            set;
        }
        
        

		public Command Edit{
			get {
				return new Command(() => {
					CycleProcessControl.Pattern.View.StaticPaternView StaticView = new CycleProcessControl.Pattern.View.StaticPaternView();
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

        

        public String Check() {
            TimeSpan CheckTime = DateTime.Now.TimeOfDay;


            if (Current == pattern.Count)
            {
                return "Няма";
            }
            if (pattern[Current].end < CheckTime) {
                Current++;
                return Check();
            }
            if (pattern.First().start > CheckTime) {
                return "Няма";
            }

            switch (pattern[Current].EventStartTime) { 
                case EventStartTimeType.All:

                    break;
                case EventStartTimeType.Start:
                    if ((int)(DateTime.Now.TimeOfDay - pattern[Current].start).TotalSeconds == 30)
                    {
                        //30 seconds from start
                        Console.WriteLine("[{0}] End Short", pattern[Current].Name);
                    }
                    break;
                case EventStartTimeType.End:
                    if ((int)(pattern[Current].end - DateTime.Now.TimeOfDay).TotalSeconds == 30)
                    {
                        //30 seconds to end
                        Console.WriteLine("[{0}] 30 seconds to mars", pattern[Current].Name);
                    }
                    break;
            }

            if ((int)(DateTime.Now.TimeOfDay - pattern[Current].start).TotalSeconds == 30)
            {
                //30 seconds from start
                Console.WriteLine("[{0}] End Short", pattern[Current].Name);
            }

            //Console.WriteLine((int)(model._period.Period - DateTime.Now.TimeOfDay).TotalSeconds);
            if ((int)(pattern[Current].end - DateTime.Now.TimeOfDay).TotalSeconds == 30)
            {
                //30 seconds to end
                Console.WriteLine("[{0}] 30 seconds to mars", pattern[Current].Name);
            }


            return pattern[Current].Name;
        }

		private void Load(String Name) {
            Current = 0;
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
