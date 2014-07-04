using BztToolbox.Common.Constantes;
using BztToolbox.Common.Utility;
using BztToolbox.Modules.Backup.Views;
using Microsoft.Practices.Composite.Modularity;
using Microsoft.Practices.Composite.Regions;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;

namespace BztToolbox.Modules.Backup
{
	[Priority(300)]
	public class ModuleBackup : IModule
	{
		#region IModule Membres

		public void Initialize() {
			NotificationHelper.WriteNotification("Initialisation du Module Backup.");

			ModulesHelper.AddLoadedModuleToList("Backup");

			var regionManager = ServiceLocator.Current.GetInstance<IRegionManager>();
			var container = ServiceLocator.Current.GetInstance<IUnityContainer>();

			// Enregistrement du MenuItem
			regionManager.RegisterViewWithRegion(RegionNames.MenuModulesRegion, typeof(BackupMenuView));

			// enregistrement de la vue
			container.RegisterType<BackupView>(typeof(BackupView).ToString());
		}

		#endregion
	}
}
