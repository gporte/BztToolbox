using System.Windows.Controls;
using BztToolbox.Modules.PortDuplicator.ViewModels;

namespace BztToolbox.Modules.PortDuplicator.Views
{
	/// <summary>
	/// Logique d'interaction pour FilterEditorMenuView.xaml
	/// </summary>
	public partial class PortDuplicatorMenuView : MenuItem
	{
		public PortDuplicatorMenuView() {
			InitializeComponent();
			this.DataContext = new PortDuplicatorMenuViewModel();
		}
	}
}
