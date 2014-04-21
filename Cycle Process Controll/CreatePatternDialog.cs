using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
namespace CycleProcessControl
{
	public partial class CreatePatternDialog : Form
	{
		public CreatePatternDialog()
		{
			InitializeComponent();
		}

		private void SaveButton_Click(object sender, EventArgs e)
		{
			FileStream fs = File.Create("Save\\" + NameBox.Text + ".json");
			fs.Flush();
			fs.Dispose();
			CycleProcessControl.Pattern.View.MainWindow StaticView = new CycleProcessControl.Pattern.View.MainWindow();
			(StaticView.DataContext as CycleProcessControl.Pattern.ViewModel.StaticPatternViewModel).FileOpen(NameBox.Text);
			StaticView.Topmost = true; 
			this.Close();
			StaticView.Show();
			
			
		}

	}
}
