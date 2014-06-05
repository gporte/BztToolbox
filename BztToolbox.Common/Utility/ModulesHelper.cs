using System.Collections.Generic;

namespace BztToolbox.Common.Utility
{
	public static class ModulesHelper
	{
		public static List<string> LoadedModulesList;

		public static void AddLoadedModuleToList(string moduleName) {
			if (LoadedModulesList == null)
				LoadedModulesList = new List<string>();

			LoadedModulesList.Add(moduleName);
		}
	}
}
