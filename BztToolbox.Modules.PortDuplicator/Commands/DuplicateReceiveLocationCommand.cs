using System;
using System.Windows.Input;
using BztToolbox.Common.Utility;
using BztToolbox.Modules.PortDuplicator.Services;
using Microsoft.BizTalk.ExplorerOM;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;

namespace BztToolbox.Modules.PortDuplicator.Commands
{
	public class DuplicateReceiveLocationCommand : ICommand
	{
		private Action _callBack;

		public DuplicateReceiveLocationCommand(Action callBack) {
			this._callBack = callBack;
		}

		public bool CanExecute(object parameter) {
			return parameter != null && parameter is ReceiveLocation;
		}

		public event EventHandler CanExecuteChanged {
			add { CommandManager.RequerySuggested += value; }
			remove { CommandManager.RequerySuggested -= value; }
		}

		public void Execute(object parameter) {
			var rcvLoc = (ReceiveLocation)parameter;
			
			var service = ServiceLocator
				.Current.GetInstance<IUnityContainer>()
				.Resolve<IPortDuplicatorServices>();

			var newName = service.DuplicateReceiveLocation(rcvLoc);
			NotificationHelper.WriteNotification(string.Format("PortDuplicator - L'emplacement de réception {0} a été dupliqué en {1}", rcvLoc.Name, newName));

			// appel du callback pour recharger la liste des ports
			this._callBack();
		}
	}
}
