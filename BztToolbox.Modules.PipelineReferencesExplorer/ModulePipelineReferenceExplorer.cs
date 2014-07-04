using BztToolbox.Common.Constantes;
using BztToolbox.Common.Utility;
using BztToolbox.Modules.PipelineReferencesExplorer.Views;
using Microsoft.Practices.Composite.Modularity;
using Microsoft.Practices.Composite.Regions;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;

namespace BztToolbox.Modules.PipelineReferencesExplorer
{
	[Priority(200)]
	public class ModulePipelineReferenceExplorer : IModule
	{
		#region IModule Membres

		public void Initialize() {
			NotificationHelper.WriteNotification("Initialisation du Module PipelineReferenceExplorer.");

			ModulesHelper.AddLoadedModuleToList("PipelineReferenceExplorer");

			var regionManager = ServiceLocator.Current.GetInstance<IRegionManager>();
			var container = ServiceLocator.Current.GetInstance<IUnityContainer>();

			// Enregistrement du MenuItem
			regionManager.RegisterViewWithRegion(RegionNames.MenuModulesRegion, typeof(PipelineReferenceExplorerMenuView));

			// enregistrement de la vue
			container.RegisterType<PipelineReferenceExplorerView>(typeof(PipelineReferenceExplorerView).ToString());
		}

		#endregion
	}
}
