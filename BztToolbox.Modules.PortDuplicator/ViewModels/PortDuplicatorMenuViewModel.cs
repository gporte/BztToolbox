using BztToolbox.Common.BaseClasses;
using BztToolbox.Common.Commands;
using BztToolbox.Modules.PortDuplicator.Views;

namespace BztToolbox.Modules.PortDuplicator.ViewModels
{
	public class PortDuplicatorMenuViewModel : ViewModelBase
	{
		#region Commands
		public ShowModuleCommand<PortDuplicatorView> ShowModuleCmd { get; set; }
		#endregion

		public PortDuplicatorMenuViewModel() {
			this.ShowModuleCmd = new ShowModuleCommand<PortDuplicatorView>();
		}
	}
}
