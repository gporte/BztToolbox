using System.Windows;
using BztToolbox.Common.Events;
using BztToolbox.ViewModels;
using Microsoft.Practices.Composite.Events;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;

namespace BztToolbox.Views
{
	/// <summary>
	/// Logique d'interaction pour Shell.xaml
	/// </summary>
	public partial class Shell : Window
	{
		public Shell(ShellViewModel vm) {
			InitializeComponent();
			this.DataContext = vm;

			var aggregator = ServiceLocator
				.Current
				.GetInstance<IUnityContainer>()
				.Resolve<IEventAggregator>();

			aggregator.GetEvent<ShowErrorEvent>().Subscribe(this.ShowError);
		}

		private void ShowError(string errorMsg) {
			// TODO Ressource
			MessageBox.Show(errorMsg, "Erreur");
		}
	}
}
