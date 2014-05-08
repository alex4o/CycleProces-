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
using System.Windows;
namespace CycleProcessControl.Pattern.ViewModel
{
	class StaticPatternViewModel : INotifyPropertyChanged
	{
		StaticPatternModel model = new StaticPatternModel();
		ObservableCollection<TimePeriodViewModel> patern = new ObservableCollection<TimePeriodViewModel>();
		TimePeriodViewModel current = new TimePeriodViewModel(new TimePeriodModel("", new TimeSpan(), 0, 0, 15));
		public StaticPatternViewModel()
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
					patern.Add(new TimePeriodViewModel(new TimePeriodModel("Period", new TimeSpan(0, 0, 0), 0, 0, 15)));
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
					patern[SelectedIndex].DeviceID = current.DeviceID;
                    patern[SelectedIndex].WorkPeriod = current.WorkPeriod;
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
					WeekViewModel.PatternRemove("");

				});
			}
		}

        public Command CopyCommand
        {
            get {
                return new Command(() => {
                    Patern.Add(Patern[SelectedIndex]);
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

                    PatternSave.Save(@"Save\" + SaveName + ".bin", model);


                    ObservableCollection<PreviewPeriodViewModel> Pattern;
                    if (WeekViewModel.Loaded.ContainsKey(SaveName))
                    {
                        Pattern = WeekViewModel.Loaded[SaveName];
                    }
                    else
                    {
                        Pattern = new ObservableCollection<PreviewPeriodViewModel>();
                        WeekViewModel.Loaded.Add(SaveName, Pattern);
                    }

                    TimeSpan EndTime = model.StartTime;
                    TimeSpan SomeTime;
                    Pattern.Clear();
                   
                    foreach (TimePeriodModel item in model.Patern)
                    {

                        SomeTime = EndTime;
                        EndTime += item.Period;
                        Pattern.Add(new PreviewPeriodViewModel(item, SomeTime, EndTime));
                    }
                    Console.WriteLine("Pattern Updated: {0}", SaveName);
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

            StaticPatternModel tmodel = PatternSave.Load(@"Save\" + SaveName + ".bin");
			if (tmodel == null)
			{
				NotifyPropertyChanged("");
				return;
			}
			model = tmodel;


			Patern.Clear();
            Dictionary<TimePeriodModel, TimePeriodViewModel> cach = new Dictionary<TimePeriodModel, TimePeriodViewModel>();
			foreach (TimePeriodModel item in model.Patern)
			{
                if (!cach.ContainsKey(item))
                {
                    cach.Add(item, new TimePeriodViewModel(item));
                }
                patern.Add(cach[item]);
			}
			NotifyPropertyChanged("");
           
		}

        public Dictionary<int, Device> Devices {
            get {
                return DeviceManager.Devices;
            }
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
                    current.DeviceID = value.DeviceID;
                    current.WorkPeriod = value.WorkPeriod;
                    if (value.EventStartTime == EventStartTimeType.All)
                    {
                        SecondVis = Visibility.Collapsed;
                    }
                    else {
                        SecondVis = Visibility.Visible;
                    }
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

        private Visibility vis;
        public Visibility SecondVis {
            get {
                return vis;
            }

            set {
                vis = value;
                NotifyPropertyChanged("SecondVis");
            }
        }

        public EventStartTimeType EventStartTime
        {
            get
            {
                return Current.EventStartTime;

            }
            set
            {
                Current.EventStartTime = value;
                if (value == EventStartTimeType.All)
                {
                    SecondVis = Visibility.Collapsed;
                }
                else
                {
                    SecondVis = Visibility.Visible;
                }
                NotifyPropertyChanged("EventStartTime");
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
