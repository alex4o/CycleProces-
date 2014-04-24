using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CycleProcessControl.Pattern.Model
{
	public struct TimePeriodModel
	{
		public TimePeriodModel(String Name, TimeSpan Period, int EventStartTime, byte EventVal)
		{
			this.Name = Name;
			this.Period = Period;
			this.EventStartTime = EventStartTime;
			this.EventVal = EventVal;
		}
		
		public String Name;
		public TimeSpan Period;
		public int EventStartTime;
		public byte EventVal;
	}
}
