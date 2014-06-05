using System;
using System.Windows;
using System.Windows.Input;
using BztToolbox.Common.Utility;

namespace BztToolbox.Modules.FilterEditor.Commands
{
	public class CopyFilterCommand : ICommand
	{
		#region ICommand Membres

		public bool CanExecute(object parameter) {
			return parameter != null && parameter is string;
		}

		public event EventHandler CanExecuteChanged {
			add { CommandManager.RequerySuggested += value; }
			remove { CommandManager.RequerySuggested -= value; }
		}

		public void Execute(object parameter) {
			Clipboard.SetText(parameter as string);
			NotificationHelper.WriteNotification("FilterEditor - Le filtre a été copié dans le presse-papier.");
		}

		#endregion
	}
}
