﻿using System;
using System.Collections.Generic;
using System.Text;
using CycleProcessControl.Pattern.Model;
using System.ComponentModel;
namespace CycleProcessControl.Pattern.ViewModel
{
	class TimePeriodViewModel : BaseViewModel
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

		public EventStartTimeType EventStartTime {
			get
			{
				return (EventStartTimeType)_period.EventStartTime;
			}
			set
			{
				_period.EventStartTime = (int)value;
				NotifyPropertyChanged("EventStartTime");
			}
		}

		public int DeviceID
		{
			get
			{
				return _period.DeviceID;
			}
			set
			{
				_period.DeviceID = value;
                NotifyPropertyChanged("DeviceID");
			}
		}

        public int WorkPeriod
        {
            get
            {
                return _period.WorkPeriod;
            }
            set
            {
                _period.WorkPeriod = value;
                NotifyPropertyChanged("WorkPeriod");
            }
        }
		#endregion
	}

	class PreviewPeriodViewModel : TimePeriodViewModel
    {
        #region Fields
        public TimeSpan start;
		public TimeSpan end;
        #endregion
        
        #region Constructor
        public PreviewPeriodViewModel(TimePeriodModel period, TimeSpan start, TimeSpan end) : base(period)
		{
			this.start = start;
			this.end = end;
		}
        #endregion

        #region Properties
        public String Start
		{
			get
			{
				return start.ToString(@"hh\:mm");
			}
			set
			{
				start = TimeSpan.Parse(value);
				NotifyPropertyChanged("Start");
			}
		}

		public String End
		{
			get
			{
				return end.ToString(@"hh\:mm");
			}
			set
			{
				end = TimeSpan.Parse(value);
				NotifyPropertyChanged("End");
			}
        }
        #endregion
    }
}
