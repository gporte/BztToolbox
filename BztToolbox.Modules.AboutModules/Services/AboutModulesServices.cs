using System.Collections.ObjectModel;
using BztToolbox.Common.Utility;

namespace BztToolbox.Modules.AboutModules.Services
{
	public class AboutModulesServices : IAboutModulesServices
	{
		public AboutModulesServices() {
		}
		
		#region IAboutModulesServices Membres

		public ObservableCollection<string> GetLoadedModules() {
			return new ObservableCollection<string>(ModulesHelper.LoadedModulesList);
		}

		#endregion
	}
}
