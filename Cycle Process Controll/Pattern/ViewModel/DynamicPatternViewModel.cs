using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace CycleProcessControl.Pattern.ViewModel
{
	class DynamicPatternViewModel : INotifyPropertyChanged
	{
		int repeat = 0;
		
		String Reapeat
		{
			get
			{
				return repeat.ToString();
			}
			set
			{
				repeat = int.Parse(value);
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
