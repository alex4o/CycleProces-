using System;
using System.Collections.Generic;
using System.Text;
using CycleProcessControll.Pattern.Model;
using System.ComponentModel;
namespace CycleProcessControll.Pattern.ViewModel
{
	class TimePeriodViewModel : INotifyPropertyChanged
	{
		#region Fields
		public TimePeriodModel _period;
		#endregion

		#region Constructor
		public TimePeriodViewModel(TimePeriodModel period)
		{
			this._period = period;
		}

		#endregion

		#region Properties
		public String Name {
			get
			{
				return _period.Name;
			}
			set {
				_period.Name = value;
				NotifyPropertyChanged("Name");
			}
		}

		public String Period {
			get {
				return _period.Period.ToString(@"hh\:mm");
			}
			set {
				_period.Period = TimeSpan.Parse(value);
				NotifyPropertyChanged("Period");
			}
		}

		public Time EventStartTime {
			get
			{
				return (Time)_period.EventStartTime;
			}
			set
			{
				_period.EventStartTime = (int)value;
				NotifyPropertyChanged("EventStartTime");
			}
		}

		public byte EventValue
		{
			get
			{
				return _period.EventVal;
			}
			set
			{
				_period.EventVal = value;
				NotifyPropertyChanged("EventValue");
			}
		}
		#endregion

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
