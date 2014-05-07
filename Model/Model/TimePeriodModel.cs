using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CycleProcessControl.Pattern.Model
{
    [Serializable]
	public class TimePeriodModel
	{
        public TimePeriodModel(String Name, TimeSpan Period, int EventStartTime, int EventVal, int WorkPeriod)
		{
			this.Name = Name;
			this.Period = Period;
			this.EventStartTime = EventStartTime;
            this.DeviceID = EventVal;
            this.WorkPeriod = WorkPeriod;
		}
		
		public String Name;
		public TimeSpan Period;
        public int WorkPeriod;
		public int EventStartTime;
		public int DeviceID;
	}
}
