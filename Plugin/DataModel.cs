using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace DataModel
{
	public class strtime : INotifyPropertyChanged
	{
		string type;
		TimeSpan lenght;
		public string Type
		{
			get
			{
				return type;
			}
			set
			{
				type = value;
				NotifyPropertyChanged("Type");
			}
		}

		public string Lenght
		{
			get
			{
				return lenght.ToString();
			}
			set
			{
				lenght = TimeSpan.Parse(value);
				NotifyPropertyChanged("Lenght");
			}
		}

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

	class BigPaternDataModel
	{

	}
}
