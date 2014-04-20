using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CycleProcessControl
{
	public class Command : ICommand
	{
		Action Executable;

		public Command(Action a)
		{
			Executable = a;
		}


		public bool CanExecute(object parameter)
		{
			return true;
		}

		public event EventHandler CanExecuteChanged;

		public void Execute(object parameter)
		{
			Executable.Invoke();
		}
	}
}
