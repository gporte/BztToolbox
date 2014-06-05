using System.Windows.Controls;
using BztToolbox.Modules.AssembliesExplorer.ViewModels;

namespace BztToolbox.Modules.AssembliesExplorer.Views
{
	/// <summary>
	/// Logique d'interaction pour AssembliesExplorerMenuView.xaml
	/// </summary>
	public partial class AssembliesExplorerMenuView : MenuItem
	{
		public AssembliesExplorerMenuView() {
			InitializeComponent();

			this.DataContext = new AssembliesExplorerMenuViewModel();
		}
	}
}
