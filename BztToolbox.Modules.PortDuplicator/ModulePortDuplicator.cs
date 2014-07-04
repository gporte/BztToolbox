using BztToolbox.Common.Constantes;
using BztToolbox.Common.Utility;
using BztToolbox.Modules.PortDuplicator.Views;
using Microsoft.Practices.Composite.Modularity;
using Microsoft.Practices.Composite.Regions;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;

namespace BztToolbox.Modules.PortDuplicator
{
	[Priority(600)]
	public class ModulePortDuplicator : IModule
	{
		#region IModule Membres

		public void Initialize() {
			NotificationHelper.WriteNotification("Initialisation du Module PortDuplicator.");

			ModulesHelper.AddLoadedModuleToList("PortDuplicator");

			var regionManager = ServiceLocator.Current.GetInstance<IRegionManager>();
			var container = ServiceLocator.Current.GetInstance<IUnityContainer>();

			// Enregistrement du MenuItem
			regionManager.RegisterViewWithRegion(RegionNames.MenuModulesRegion, typeof(PortDuplicatorMenuView));

			// enregistrement de la vue
			container.RegisterType<PortDuplicatorView>(typeof(PortDuplicatorView).ToString());
		}

		#endregion
	}
}
