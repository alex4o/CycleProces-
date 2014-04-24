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
		public static readonly DependencyProperty Value = DependencyProperty.Register("EventStartTime", typeof(EventStartTimeType), typeof(ComboBoxTimeItem));
		public EventStartTimeType EventStartTime
		{
			get { return (EventStartTimeType)GetValue(Value); }
			set { SetValue(Value, value); }
		}

	}

    public class ComboBoxPortTypeItem : ComboBoxItem
    {
        public static readonly DependencyProperty Value = DependencyProperty.Register("PortType", typeof(DevicePortType), typeof(ComboBoxTimeItem));
        public DevicePortType PortType
        {
            get { return (DevicePortType)GetValue(Value); }
            set { SetValue(Value, value); }
        }

    }
}
