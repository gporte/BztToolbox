using System.Windows.Controls;
using BztToolbox.Modules.PipelineReferencesExplorer.ViewModels;
using Microsoft.BizTalk.ExplorerOM;

namespace BztToolbox.Modules.PipelineReferencesExplorer.Views
{
	/// <summary>
	/// Logique d'interaction pour PipelineReferenceExplorerView.xaml
	/// </summary>
	public partial class PipelineReferenceExplorerView : UserControl
	{
		public PipelineReferenceExplorerView() {
			InitializeComponent();
			this.DataContext = new PipelineReferenceExplorerViewModel();
		}

		private void lbxPipelines_SelectionChanged(object sender, SelectionChangedEventArgs e) {
			if (this.lbxPipelines.SelectedItem != null && this.lbxPipelines.SelectedItem is Pipeline) {
				(this.DataContext as PipelineReferenceExplorerViewModel).ExecuteSearchReferencesCommand(this.lbxPipelines.SelectedItem as Pipeline);
			}
		}
	}
}
