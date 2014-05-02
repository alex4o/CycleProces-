using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CycleProcessControl.Pattern.Model
{
	public struct TimePeriodModel
	{
        public TimePeriodModel(String Name, TimeSpan Period, int EventStartTime, int EventVal)
		{
			this.Name = Name;
			this.Period = Period;
			this.EventStartTime = EventStartTime;
            this.DeviceID = EventVal;
		}
		
		public String Name;
		public TimeSpan Period;
		public int EventStartTime;
		public int DeviceID;
	}
}
