using System.Windows.Controls;
using BztToolbox.Modules.AboutModules.ViewModels;

namespace BztToolbox.Modules.AboutModules.Views
{
	/// <summary>
	/// Logique d'interaction pour AboutModulesView.xaml
	/// </summary>
	public partial class AboutModulesView : UserControl
	{
		public AboutModulesView() {
			InitializeComponent();
			this.DataContext = new AboutModulesViewModel();
		}
	}
}
