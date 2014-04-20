using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Dynamic;
using System.IO;
using CycleProcessControl.Pattern.Model;
using System.Threading;
namespace CycleProcessControl.Pattern.ViewModel
{
	class StaticPatternViweModel : INotifyPropertyChanged
	{
		StaticPatternModel model = new StaticPatternModel();
		ObservableCollection<TimePeriodViewModel> patern = new ObservableCollection<TimePeriodViewModel>();
		TimePeriodViewModel current = new TimePeriodViewModel(new TimePeriodModel("", new TimeSpan(), 0, 0));
		public StaticPatternViweModel()
		{

		}

		public String SaveName
		{
			get;
			set;
		}

		public int SelectedIndex
		{
			get;
			set;
		}

		public String StartTime
		{
			get
			{
				return model.StartTime.ToString(@"hh\:mm");
			}
			set
			{
				model.StartTime = TimeSpan.Parse(value);
				NotifyPropertyChanged("StartTime");
			}
		}

		public Command AddCommand
		{
			get
			{
				return new Command(() =>
				{
					patern.Add(new TimePeriodViewModel(new TimePeriodModel("Period", new TimeSpan(0, 0, 0), 0, 0)));
				});
			}
		}

		public Command UpdateCommand
		{
			get
			{
				return new Command(() =>
				{
					patern[SelectedIndex].Name = current.Name;
					patern[SelectedIndex].Period = current.Period;
					patern[SelectedIndex].EventStartTime = current.EventStartTime;
					patern[SelectedIndex].EventValue = current.EventValue;

				});
			}
		}

		public Command RemoveCommand
		{
			get
			{
				return new Command(() =>
				{
					if (SelectedIndex != -1)
					{
						patern.RemoveAt(SelectedIndex);
						SelectedIndex = 0;
					}
				});
			}
		}

		public Command MoveUpCommand
		{
			get
			{
				return new Command(() =>
				{
					if (SelectedIndex - 1 < 0)
					{
						return;
					}
					patern.Move(SelectedIndex, SelectedIndex - 1);
				});
			}
		}

		public Command MoveDownCommand
		{
			get
			{
				return new Command(() =>
				{
					if (SelectedIndex + 1 > patern.Count - 1)
					{
						return;
					}
					patern.Move(SelectedIndex, SelectedIndex + 1);
				});
			}
		}
		public Command RemoveFile
		{
			get
			{
				return new Command(() =>
				{
					File.Delete(@"Save\" + SaveName + ".json");
					WeekViewModel.PatternUpdate("");

				});
			}
		}

		public Command SaveFile
		{
			get
			{
				return new Command(() =>
				{
					model.Patern.Clear();
					foreach (TimePeriodViewModel item in patern)
					{
						model.Patern.Add(item._period);
					}

					StreamWriter sw = new StreamWriter(@"Save\" + SaveName + ".json");

					sw.Write(Newtonsoft.Json.JsonConvert.SerializeObject(model));
					sw.Flush();
					sw.Dispose();
					WeekViewModel.PatternUpdate(SaveName);

				});
			}
		}



		public void FileOpen(String Name)
		{
			if (Name == null)
			{
				return;
			}
			this.SaveName = Name;

			StreamReader sr = new StreamReader(@"Save\" + SaveName + ".json");
			String Object = sr.ReadToEnd();
			sr.Dispose();
			StaticPatternModel tmodel = Newtonsoft.Json.JsonConvert.DeserializeObject<StaticPatternModel>(Object);
			if (tmodel == null)
			{
				NotifyPropertyChanged("");
				return;
			}
			model = tmodel;


			Patern.Clear();
			foreach (TimePeriodModel item in model.Patern)
			{
				patern.Add(new TimePeriodViewModel(item));
			}
			NotifyPropertyChanged("");
		}

		public TimePeriodViewModel Current
		{
			get
			{
				return current;
			}
			set
			{
                if (value != null)
                {
                    current.Name = value.Name;
                    current.Period = value.Period;
                    current.EventStartTime = value.EventStartTime;
                    current.EventValue = value.EventValue;
                }
			}
		}

		public ObservableCollection<TimePeriodViewModel> Patern
		{
			get
			{
				return patern;
			}
		}




		public event PropertyChangedEventHandler PropertyChanged;
		private void NotifyPropertyChanged(string str)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(str));
			}
		}
	}
}
