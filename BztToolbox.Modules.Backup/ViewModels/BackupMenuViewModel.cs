using BztToolbox.Common.BaseClasses;
using BztToolbox.Common.Commands;
using BztToolbox.Modules.Backup.Views;

namespace BztToolbox.Modules.Backup.ViewModels
{
	public class BackupMenuViewModel : ViewModelBase
	{
		#region Commands
		public ShowModuleCommand<BackupView> ShowModuleCmd { get; set; }
		#endregion

		public BackupMenuViewModel() {
			this.ShowModuleCmd = new ShowModuleCommand<BackupView>();
		}
	}
}
