using BztToolbox.Common.Constantes;
using BztToolbox.Common.Utility;
using BztToolbox.Modules.AboutModules.Views;
using Microsoft.Practices.Composite.Modularity;
using Microsoft.Practices.Composite.Regions;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;

namespace BztToolbox.Modules.AboutModules
{
	[Priority(500)]
	public class ModuleAboutModules : IModule
	{
		#region IModule Membres

		public void Initialize() {
			NotificationHelper.WriteNotification("Initialisation du Module AboutModules.");

			ModulesHelper.AddLoadedModuleToList("AboutModules");

			var regionManager = ServiceLocator.Current.GetInstance<IRegionManager>();
			var container = ServiceLocator.Current.GetInstance<IUnityContainer>();

			// Enregistrement du MenuItem
			regionManager.RegisterViewWithRegion(RegionNames.MenuModulesRegion, typeof(AboutModulesMenuView));

			// enregistrement de la vue
			container.RegisterType<AboutModulesView>(typeof(AboutModulesView).ToString());
		}
		#endregion
	}
}
