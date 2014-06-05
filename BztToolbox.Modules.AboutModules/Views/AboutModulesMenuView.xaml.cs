using System.Windows.Controls;
using BztToolbox.Modules.AboutModules.ViewModels;

namespace BztToolbox.Modules.AboutModules.Views
{
	/// <summary>
	/// Logique d'interaction pour AboutModulesMenuView.xaml
	/// </summary>
	public partial class AboutModulesMenuView : MenuItem
	{
		public AboutModulesMenuView() {
			InitializeComponent();
			this.DataContext = new AboutModulesMenuViewModel();
		}
	}
}
