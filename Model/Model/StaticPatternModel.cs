﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CycleProcessControll.Model{
	public class TimePeriodModel
	{
		public TimePeriodModel(String Name,TimeSpan Period)
		{
			this.Name = Name;
			this.Period = Period;
		}

		public String Name;
		public TimeSpan Period;
	}
}
