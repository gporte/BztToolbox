using BztToolbox.Common.Constantes;
using BztToolbox.Common.Utility;
using BztToolbox.Modules.FilterEditor.Views;
using Microsoft.Practices.Composite.Modularity;
using Microsoft.Practices.Composite.Regions;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;

namespace BztToolbox.Modules.FilterEditor
{
	[Priority(100)]
	public class ModuleFilterEditor : IModule
	{
		#region IModule Membres

		public void Initialize() {
			NotificationHelper.WriteNotification("Initialisation du Module FilterEditor.");

			ModulesHelper.AddLoadedModuleToList("FilterEditor");

			var regionManager = ServiceLocator.Current.GetInstance<IRegionManager>();
			var container = ServiceLocator.Current.GetInstance<IUnityContainer>();

			// Enregistrement du MenuItem
			regionManager.RegisterViewWithRegion(RegionNames.MenuModulesRegion, typeof(FilterEditorMenuView));

			// enregistrement de la vue
			container.RegisterInstance<FilterEditorView>(
				typeof(FilterEditorView).ToString(), 
				new FilterEditorView()
			);

			// Ajout de la vue et desactivation
			regionManager.Regions[RegionNames.ContentRegion].Add(
				container.Resolve<FilterEditorView>(typeof(FilterEditorView).ToString())
			);

			regionManager.Regions[RegionNames.ContentRegion].Deactivate(
				container.Resolve<FilterEditorView>(typeof(FilterEditorView).ToString())
			);
		}

		#endregion
	}
}
