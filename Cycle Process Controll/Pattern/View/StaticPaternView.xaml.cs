using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CycleProcessControll.Pattern.View
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		
		public MainWindow()
		{
			InitializeComponent();
			
			

		}
	/*	short count = 0;
		private void Button_Add(object sender, RoutedEventArgs e){
		bpvm.PaternList.Add( new strtime { Type = "Item" + count});
		count += 1;
		}

		private void Button_Save(object sender, RoutedEventArgs e)
		{
			bpvm.SaveItem();
		}

		private void Button_Back(object sender, RoutedEventArgs e)
		{
			 
			 StreamWriter sw = new StreamWriter("savefile.json");
			 String ser = JsonConvert.SerializeObject(bpvm.DataForDB);
			 sw.Write(ser);

			 sw.Dispose();
			 sw.Close();
			 StaticPaternViweModel mv = JsonConvert.DeserializeObject<StaticPaternViweModel>(ser);
			 mv.ToString();
		}

		private void Up(object sender, RoutedEventArgs e)
		{
			int si = bpvm.SIndex;
			if (si < 0)
			{
				return;
			}
			strtime el = bpvm.PaternList[si];
			if (si - 1 < 0)
			{
				return;
			}
			bpvm.PaternList.RemoveAt(si);

			bpvm.PaternList.Insert(si - 1, el);
			fl1.SelectedIndex = si - 1;
		}

		private void Down(object sender, RoutedEventArgs e)
		{
			int si = bpvm.SIndex;
			if (si < 0) {
				return;
			}
			strtime el = bpvm.PaternList[si];
			if (si + 1 > bpvm.PaternList.Count)
			{
				return;
			}
			bpvm.PaternList.RemoveAt(si);

			bpvm.PaternList.Insert(si + 1, el);
			fl1.SelectedIndex = si + 1;
		}

		private void Button_Back(object sender, RoutedEventArgs e)
		{

			StreamWriter sw = new StreamWriter("savefile.json");
			String ser = JsonConvert.SerializeObject(bpvm.DataForDB);
			sw.Write(ser);

			sw.Dispose();
			sw.Close();
			StaticPaternViweModel mv = JsonConvert.DeserializeObject<StaticPaternViweModel>(ser);
			mv.ToString();
			
		}

		private void Open(object sender, RoutedEventArgs e)
		{
			StreamReader sr = new StreamReader("savefile.json");
			String data = sr.ReadToEnd();

		}
	 * */
	}

	
}
