using System.Windows.Controls;
using BztToolbox.Modules.FilterEditor.ViewModels;

namespace BztToolbox.Modules.FilterEditor.Views
{
	/// <summary>
	/// Logique d'interaction pour FilterEditorView.xaml
	/// </summary>
	public partial class FilterEditorView : UserControl
	{
		public FilterEditorView() {
			InitializeComponent();
			this.DataContext = new FilterEditorViewModel();
		}
	}
}
