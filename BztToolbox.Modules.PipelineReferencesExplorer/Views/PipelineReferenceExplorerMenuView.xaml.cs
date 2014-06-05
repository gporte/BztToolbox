using System.Windows.Controls;
using BztToolbox.Modules.PipelineReferencesExplorer.ViewModels;

namespace BztToolbox.Modules.PipelineReferencesExplorer.Views
{
	/// <summary>
	/// Logique d'interaction pour PipelineReferenceExplorerMenuView.xaml
	/// </summary>
	public partial class PipelineReferenceExplorerMenuView : MenuItem
	{
		public PipelineReferenceExplorerMenuView() {
			InitializeComponent();
			this.DataContext = new PipelineReferenceExplorerMenuViewModel();
		}
	}
}
