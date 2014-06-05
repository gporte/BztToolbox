using System;
using System.Windows;
using System.Windows.Input;

namespace BztToolbox.Common.Commands
{
	public class ExitCommand : ICommand
	{
		public bool CanExecute(object parameter) {
			return true;
		}

		public event EventHandler CanExecuteChanged {
			add { CommandManager.RequerySuggested += value; }
			remove { CommandManager.RequerySuggested -= value; }
		}

		public void Execute(object parameter) {
			Application.Current.Shutdown();
		}
	}
}
