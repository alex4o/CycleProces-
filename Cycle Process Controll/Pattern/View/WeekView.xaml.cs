using CycleProcessControl.Pattern.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CycleProcessControl.Pattern.View
{
	/// <summary>
	/// Interaction logic for Week.xaml
	/// </summary>
	public partial class WeekView : Window
	{
		public WeekView()
		{
            SplashWindow splash = new SplashWindow();

            splash.Show();

			InitializeComponent();

            splash.Close();
           // this.DataContext = new WeekViewModel();
            CompositionTarget.Rendering += OnRendering;
		}

        void OnRendering(object sender, EventArgs e)
        {
            //TT.Text = DateTime.Now.ToString("HH:mm:ss");
            (DataContext as IRefresh).Refresh();
        }
    }
}
