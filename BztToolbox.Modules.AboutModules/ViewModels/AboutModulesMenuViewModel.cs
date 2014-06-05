using BztToolbox.Common.BaseClasses;
using BztToolbox.Common.Commands;
using BztToolbox.Modules.AboutModules.Views;

namespace BztToolbox.Modules.AboutModules.ViewModels
{
	public class AboutModulesMenuViewModel : ViewModelBase
	{
		#region Commands
		public ShowModuleCommand<AboutModulesView> ShowModuleCmd { get; set; }
		#endregion
		
		public AboutModulesMenuViewModel() {
			this.ShowModuleCmd = new ShowModuleCommand<AboutModulesView>();
		}
	}
}
