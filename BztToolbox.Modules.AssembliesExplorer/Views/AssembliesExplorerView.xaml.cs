using System.Windows.Controls;
using BztToolbox.Modules.AssembliesExplorer.ViewModels;

namespace BztToolbox.Modules.AssembliesExplorer.Views
{
	/// <summary>
	/// Logique d'interaction pour AssembliesExplorerView.xaml
	/// </summary>
	public partial class AssembliesExplorerView : UserControl
	{
		public AssembliesExplorerView() {
			InitializeComponent();
			this.DataContext = new AssembliesExplorerViewModel();
		}
	}
}
