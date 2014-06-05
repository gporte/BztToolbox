using System.Collections.ObjectModel;

namespace BztToolbox.Modules.AboutModules.Services
{
	public interface IAboutModulesServices
	{
		ObservableCollection<string> GetLoadedModules();
	}
}
