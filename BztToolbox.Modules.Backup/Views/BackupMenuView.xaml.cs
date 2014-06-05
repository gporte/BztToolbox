using System.Windows.Controls;
using BztToolbox.Modules.Backup.ViewModels;

namespace BztToolbox.Modules.Backup.Views
{
	/// <summary>
	/// Logique d'interaction pour BackupMenuView.xaml
	/// </summary>
	public partial class BackupMenuView : MenuItem
	{
		public BackupMenuView() {
			InitializeComponent();
			this.DataContext = new BackupMenuViewModel();
		}
	}
}
