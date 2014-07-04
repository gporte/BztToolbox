using System;
using System.Windows.Input;
using BztToolbox.Common.Utility;
using BztToolbox.Modules.PortDuplicator.Services;
using Microsoft.BizTalk.ExplorerOM;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;

namespace BztToolbox.Modules.PortDuplicator.Commands
{
	public class DuplicateSendPortCommand : ICommand
	{
		private Action _callBack;
		
		public DuplicateSendPortCommand(Action callBack) {
			this._callBack = callBack;
		}
		
		#region ICommand Membres

		public bool CanExecute(object parameter) {
			return parameter != null && parameter is SendPort;
		}

		public event EventHandler CanExecuteChanged {
			add { CommandManager.RequerySuggested += value; }
			remove { CommandManager.RequerySuggested -= value; }
		}

		public void Execute(object parameter) {
			var sndPort = (SendPort)parameter;
			
			var service = ServiceLocator
				.Current.GetInstance<IUnityContainer>()
				.Resolve<IPortDuplicatorServices>();

			var newName = service.DuplicateSendPort(sndPort);
			NotificationHelper.WriteNotification(string.Format("PortDuplicator - Le port d'envoi {0} a été dupliqué en {1}", sndPort.Name, newName));

			// appel du callback pour recharger la liste des ports
			this._callBack();
		}

		#endregion
	}
}
