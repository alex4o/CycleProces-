using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CycleProcessControll.Pattern.Model
{
	public struct TimePeriodModel
	{
		public TimePeriodModel(String Name,TimeSpan Period)
		{
			this.Name = Name;
			this.Period = Period;
		}

		public String Name;
		public TimeSpan Period;
		//public int portval = 0;
	}
}
