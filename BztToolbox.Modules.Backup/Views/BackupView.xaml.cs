using System.Windows.Controls;
using BztToolbox.Modules.Backup.ViewModels;

namespace BztToolbox.Modules.Backup.Views
{
	/// <summary>
	/// Logique d'interaction pour BackupView.xaml
	/// </summary>
	public partial class BackupView : UserControl
	{
		public BackupView() {
			InitializeComponent();
			this.DataContext = new BackupViewModel();
		}
	}
}
