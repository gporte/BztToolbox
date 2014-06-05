using BztToolbox.Common.BaseClasses;
using BztToolbox.Common.Commands;
using BztToolbox.Modules.FilterEditor.Views;

namespace BztToolbox.Modules.FilterEditor.ViewModels
{
	public class FilterEditorMenuViewModel : ViewModelBase
	{
		#region Commands
		public ShowModuleCommand<FilterEditorView> ShowModuleCmd { get; set; }
		#endregion

		public FilterEditorMenuViewModel() {
			this.ShowModuleCmd = new ShowModuleCommand<FilterEditorView>();
		}
	}
}
