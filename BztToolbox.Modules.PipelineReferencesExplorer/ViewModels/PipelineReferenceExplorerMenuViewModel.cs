using BztToolbox.Common.BaseClasses;
using BztToolbox.Common.Commands;
using BztToolbox.Modules.PipelineReferencesExplorer.Views;

namespace BztToolbox.Modules.PipelineReferencesExplorer.ViewModels
{
	public class PipelineReferenceExplorerMenuViewModel : ViewModelBase
	{
		#region Commands
		public ShowModuleCommand<PipelineReferenceExplorerView> ShowModuleCmd { get; set; }
		#endregion

		public PipelineReferenceExplorerMenuViewModel() {
			this.ShowModuleCmd = new ShowModuleCommand<PipelineReferenceExplorerView>();
		}
	}
}
