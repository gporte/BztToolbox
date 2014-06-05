using System.Windows.Controls;
using BztToolbox.Modules.FilterEditor.ViewModels;

namespace BztToolbox.Modules.FilterEditor.Views
{
	/// <summary>
	/// Logique d'interaction pour FilterEditorMenuView.xaml
	/// </summary>
	public partial class FilterEditorMenuView : MenuItem
	{
		public FilterEditorMenuView() {
			InitializeComponent();
			this.DataContext = new FilterEditorMenuViewModel();
		}
	}
}
