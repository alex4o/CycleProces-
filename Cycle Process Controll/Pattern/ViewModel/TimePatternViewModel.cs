using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.IO;
using CycleProcessControll.Model;
using System.Threading;
namespace CycleProcessControll.Pattern.ViewModel
{
	class TimePatternViewModel : INotifyPropertyChanged
	{
		ObservableCollection<string> patterns = new ObservableCollection<string>();
		ObservableCollection<TimePeriodViewModel> pattern = new ObservableCollection<TimePeriodViewModel>();
		public TimePatternViewModel()
		{
			
		}

		public ObservableCollection<TimePeriodViewModel> Pattern {
			get {
				return pattern;
			}
		}

		public ObservableCollection<string> Patterns
		{
			get {
				return new ObservableCollection<string>(GetNames(Directory.GetFiles(@"Save\")));
			}
		}

		private IEnumerable<String> GetNames(String[] Paths)
		{
			foreach (String item in Paths)
			{
				yield return item.Split('\\').Last().Split('.')[0];
			}
		}

		public String SelectedFile
		{
			set
			{
				if (value == null) {
					return;
				}
				AutoResetEvent are = new AutoResetEvent(false);
				StaticPatternModel model = new StaticPatternModel();
				Thread t = new Thread(() =>
				{
					using (StreamReader sr = new StreamReader(@"Save\" + value + ".json"))
					{
						String Object = sr.ReadToEnd();
						model = Newtonsoft.Json.JsonConvert.DeserializeObject<StaticPatternModel>(Object);
					}
					are.Set();
				});
				t.Start();
				pattern.Clear();
				are.WaitOne();
				
				foreach (TimePeriodModel item in model.Patern)
				{
					pattern.Add(new TimePeriodViewModel(item));
				}
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
}
