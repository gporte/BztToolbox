using System;
using System.Windows.Input;
using BztToolbox.Common.Constantes;
using BztToolbox.Common.Utility;
using Microsoft.Practices.Composite.Regions;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;

namespace BztToolbox.Common.Commands
{
	public class ShowModuleCommand<T> : ICommand
	{
		#region ICommand Membres

		public bool CanExecute(object parameter) {
			return parameter is string && !string.IsNullOrEmpty(parameter as string);
		}

		public event EventHandler CanExecuteChanged {
			add { CommandManager.RequerySuggested += value; }
			remove { CommandManager.RequerySuggested -= value; }
		}

		public void Execute(object parameter) {
			var regionManager = ServiceLocator.Current.GetInstance<IRegionManager>();
			var container = ServiceLocator.Current.GetInstance<IUnityContainer>();

			regionManager.Regions[RegionNames.ContentRegion].Activate(
				container.Resolve<T>(typeof(T).ToString())
			);

			NotificationHelper.WriteNotification("Affichage du module " + parameter as string);
		}

		#endregion
	}
}
