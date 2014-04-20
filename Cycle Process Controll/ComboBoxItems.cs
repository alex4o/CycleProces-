using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace CycleProcessControl
{
	public class ComboBoxTimeItem : ComboBoxItem
	{
		public static readonly DependencyProperty Value = DependencyProperty.Register("EventStartTime", typeof(Time), typeof(ComboBoxTimeItem));
		public Time EventStartTime
		{
			get { return (Time)GetValue(Value); }
			set { SetValue(Value, value); }
		}

	}
}
