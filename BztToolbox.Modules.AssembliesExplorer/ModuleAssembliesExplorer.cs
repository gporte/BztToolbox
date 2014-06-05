using BztToolbox.Common.Constantes;
using BztToolbox.Common.Utility;
using BztToolbox.Modules.AssembliesExplorer.Views;
using Microsoft.Practices.Composite.Modularity;
using Microsoft.Practices.Composite.Regions;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;

namespace BztToolbox.Modules.AssembliesExplorer
{
	[Priority(400)]
	public class ModuleAssembliesExplorer : IModule
	{
		#region IModule Membres

		public void Initialize() {
			NotificationHelper.WriteNotification("Initialisation du Module AssembliesExplorer.");

			ModulesHelper.AddLoadedModuleToList("AssembliesExplorer");

			var regionManager = ServiceLocator.Current.GetInstance<IRegionManager>();
			var container = ServiceLocator.Current.GetInstance<IUnityContainer>();

			// Enregistrement du MenuItem
			regionManager.RegisterViewWithRegion(RegionNames.MenuModulesRegion, typeof(AssembliesExplorerMenuView));

			// enregistrement de la vue
			container.RegisterInstance<AssembliesExplorerView>(
				typeof(AssembliesExplorerView).ToString(), 
				new AssembliesExplorerView()
			);

			// Ajout de la vue et desactivation
			regionManager.Regions[RegionNames.ContentRegion].Add(
				container.Resolve<AssembliesExplorerView>(typeof(AssembliesExplorerView).ToString())
			);

			regionManager.Regions[RegionNames.ContentRegion].Deactivate(
				container.Resolve<AssembliesExplorerView>(typeof(AssembliesExplorerView).ToString())
			);
		}

		#endregion
	}
}
