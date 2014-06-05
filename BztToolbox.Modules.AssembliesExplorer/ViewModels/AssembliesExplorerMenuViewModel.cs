using BztToolbox.Common.BaseClasses;
using BztToolbox.Common.Commands;
using BztToolbox.Modules.AssembliesExplorer.Views;

namespace BztToolbox.Modules.AssembliesExplorer.ViewModels
{
	public class AssembliesExplorerMenuViewModel : ViewModelBase
	{
		#region Commands
		public ShowModuleCommand<AssembliesExplorerView> ShowModuleCmd { get; set; }
		#endregion

		public AssembliesExplorerMenuViewModel() {
			this.ShowModuleCmd = new ShowModuleCommand<AssembliesExplorerView>();
		}
	}
}
