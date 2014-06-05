using System;
using System.Windows.Input;
using BztToolbox.Common.Utility;
using BztToolbox.Modules.FilterEditor.Services;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;

namespace BztToolbox.Modules.FilterEditor.Commands
{
	public class SaveChangesCommand : ICommand
	{
		#region ICommand Membres

		public bool CanExecute(object parameter) {
			return true;
		}

		public event EventHandler CanExecuteChanged {
			add { CommandManager.RequerySuggested += value; }
			remove { CommandManager.RequerySuggested -= value; }
		}

		public void Execute(object parameter) {
			var service = ServiceLocator
				.Current.GetInstance<IUnityContainer>()
				.Resolve<IFilterEditorServices>();

			service.SaveChanges();
			NotificationHelper.WriteNotification("FilterEditor - Modifications sauvegardées.");
		}

		#endregion
	}
}
