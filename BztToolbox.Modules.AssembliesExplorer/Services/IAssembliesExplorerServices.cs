using System.Collections.ObjectModel;
using Microsoft.BizTalk.ExplorerOM;

namespace BztToolbox.Modules.AssembliesExplorer.Services
{
	public interface IAssembliesExplorerServices
	{
		ObservableCollection<BtsAssembly> GetAllAssemblies();
	}
}
