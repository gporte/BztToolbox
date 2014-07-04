using System.Windows.Controls;
using BztToolbox.Modules.PortDuplicator.ViewModels;

namespace BztToolbox.Modules.PortDuplicator.Views
{
	/// <summary>
	/// Logique d'interaction pour PortDuplicatorView.xaml
	/// </summary>
	public partial class PortDuplicatorView : UserControl
	{
		public PortDuplicatorView() {
			InitializeComponent();
			this.DataContext = new PortDuplicatorViewModel();
		}
	}
}
