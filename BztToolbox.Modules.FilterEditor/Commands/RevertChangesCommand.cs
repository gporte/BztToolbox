using System;
using System.Windows.Input;
using BztToolbox.Common.Utility;
using BztToolbox.Modules.FilterEditor.Services;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;

namespace BztToolbox.Modules.FilterEditor.Commands
{
	public class RevertChangesCommand : ICommand
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

			service.RevertChanges();
			NotificationHelper.WriteNotification("FilterEditor - Modifications annulées. Sélectionnez un autre port pour rafraîchir l'affichage.");
		}

		#endregion
	}
}
