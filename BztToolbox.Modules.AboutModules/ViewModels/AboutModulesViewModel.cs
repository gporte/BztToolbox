using System.Collections.ObjectModel;
using System.ComponentModel;
using BztToolbox.Common.BaseClasses;
using BztToolbox.Modules.AboutModules.Services;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;

namespace BztToolbox.Modules.AboutModules.ViewModels
{
	public class AboutModulesViewModel : ViewModelBase
	{
		private IAboutModulesServices _services;

		#region Items property
		public ObservableCollection<string> Items { get; private set; }
		#endregion

		public AboutModulesViewModel() {
			var container = ServiceLocator.Current.GetInstance<IUnityContainer>();
			
			// Initialisation et enregistrement du service
			container.RegisterInstance<IAboutModulesServices>(
				new AboutModulesServices()
			);

			this._services = ServiceLocator
				.Current.GetInstance<IUnityContainer>()
				.Resolve<IAboutModulesServices>();

			this.Items = new ObservableCollection<string>(this._services.GetLoadedModules());
		}
	}
}
