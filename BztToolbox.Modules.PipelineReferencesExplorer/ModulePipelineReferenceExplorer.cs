using BztToolbox.Common.Constantes;
using BztToolbox.Common.Utility;
using BztToolbox.Modules.PipelineReferencesExplorer.Views;
using Microsoft.Practices.Composite.Modularity;
using Microsoft.Practices.Composite.Regions;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;

namespace BztToolbox.PipelineReferencesExplorer
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
			container.RegisterInstance<PipelineReferenceExplorerView>(
				typeof(PipelineReferenceExplorerView).ToString(), 
				new PipelineReferenceExplorerView()
			);

			// Ajout de la vue et desactivation
			regionManager.Regions[RegionNames.ContentRegion].Add(
				container.Resolve<PipelineReferenceExplorerView>(typeof(PipelineReferenceExplorerView).ToString())
			);

			regionManager.Regions[RegionNames.ContentRegion].Deactivate(
				container.Resolve<PipelineReferenceExplorerView>(typeof(PipelineReferenceExplorerView).ToString())
			);
		}

		#endregion
	}
}
