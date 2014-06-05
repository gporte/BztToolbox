using System.Collections.ObjectModel;
using Microsoft.BizTalk.ExplorerOM;

namespace BztToolbox.Modules.Backup.Services
{
	public interface IBackupServices
	{
		ObservableCollection<Application> GetAllApplications();
		string ExportBinding(string appName, string backupPath);
		string ExportMsi(string appName, string backupPath);
		string ExportMsiWithResourcesFilter(string appName, string backupPath, string resourcesSpecFileName);
		string ExportResourceSpecFile(string appName, string backupPath, out string resourcesFileName);
		void ConfigResourcesSpecFile(string fileName, bool exportBindings, bool exportAssemblies, bool exportWebDirectories);
	}
}
